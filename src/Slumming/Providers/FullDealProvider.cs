using Slumming.Models;
using Slumming.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Slumming.Providers
{
    public class FullDealProvider
    {
        private ISalesmanService salesmanService;
        private IClientService clientService;
        private IDealService dealService;
        private IApartmentService apartmentService;
        public FullDealProvider(ISalesmanService _salesmanService, IClientService _clientService, IDealService _dealService, IApartmentService _apartmentService)
        {
            salesmanService = _salesmanService;
            clientService = _clientService;
            dealService = _dealService;
            apartmentService = _apartmentService;
        }

        public async Task<FullDeal> CreateDeal(FullDeal fullDeal)
        {
            //validate deal
            var validateBlock = new TransformBlock<FullDeal, FullDeal>(async fullDealInner =>
            {
                try
                {
                    var salesman = await salesmanService.Get(fullDeal.Salesman.Id);
                    if (salesman != null)
                    {
                        fullDealInner.Deal.SalesmanId = fullDeal.Salesman.Id;
                    }

                    fullDealInner.IsValid = salesman != null;
                }
                catch (Exception)
                {
                    fullDealInner.IsValid = false;
                }

                return fullDealInner;
            });
            //create/ update client
            var createClientBlock = new TransformBlock<FullDeal, FullDeal>(async fullDealInner =>
          {
              if (fullDealInner.Client.Id == 0)
              {
                  fullDealInner.Client.Id = await clientService.Create(fullDealInner.Client);
              }
              else
              {
                  await clientService.Update(fullDealInner.Client);
              }
              fullDealInner.Apartment.ClientId = fullDealInner.Client.Id;
              return fullDealInner;
          });

            //create/update appartment
            var createAppartmentBlock = new TransformBlock<FullDeal, FullDeal>(async fullDealInner =>
            {
                if (fullDealInner.Apartment.Id == 0)
                {
                    fullDealInner.Apartment.Id = await apartmentService.Create(fullDealInner.Apartment);
                }
                else
                {
                    await apartmentService.Update(fullDealInner.Apartment);
                }
                fullDealInner.Deal.AppartmentId = fullDealInner.Apartment.Id;
                return fullDealInner;

            });

            // register deal
            var registerDealBlock = new TransformBlock<FullDeal, FullDeal>(fullDealInner =>
            {
                fullDealInner.Deal.RegisterId = GetUniqueGuid();
                return fullDealInner;
            });

            //create deal
            var createDealBlock = new TransformBlock<FullDeal, FullDeal>(async fullDealInner =>
            {
                fullDealInner.Deal.Date = DateTime.Now;
                fullDealInner.Deal.Id = await dealService.Create(fullDealInner.Deal);
                return fullDealInner;
            });

            var finalBlock = new ActionBlock<FullDeal>(fullDealInner =>
            {
                Console.WriteLine("Done");
            });

            //invalid path
            var notValidBlock = new TransformBlock<FullDeal, FullDeal>(fullDealInner =>
            {
                fullDealInner.IsValid = false;
                fullDealInner.Reason = "random stuff";
                return fullDealInner;
            });

            var loggingBlock = new ActionBlock<FullDeal>(fullDealInner => Console.WriteLine("error"));

            //linking
            var options = new DataflowLinkOptions { PropagateCompletion = true };
            validateBlock.LinkTo(createClientBlock, options, d => d.IsValid);
            validateBlock.LinkTo(notValidBlock, options, d => !d.IsValid);
            notValidBlock.LinkTo(loggingBlock, options);

            createClientBlock.LinkTo(createAppartmentBlock, options);
            createAppartmentBlock.LinkTo(registerDealBlock, options);
            registerDealBlock.LinkTo(createDealBlock, options);
            createDealBlock.LinkTo(finalBlock, options);

            //post
            validateBlock.Post(fullDeal);
            validateBlock.Complete();

            await createDealBlock.Completion;
            return fullDeal;
        }

        public string GetUniqueGuid(int count = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, count).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
