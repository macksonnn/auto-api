//using Azure.Messaging.ServiceBus;
//using AutoMais.Core.Domain.Aggregates.Account.Events;
//using Newtonsoft.Json;
//using AutoMais.Core.Application.Adapters.Stream;
//using Core.Common.Extensions;

//namespace AutoMais.Integrations.Account.Events
//{

//    /// <summary>
//    /// When the BlockScheduled event happens in the ServiceBus, dispatch the BlockAccount command
//    /// </summary>
//    public class AccountBlockScheduled_BlockAccount
//    {
//        private readonly ILogger _logger;
//        private IMediator _mediator;

//        public AccountBlockScheduled_BlockAccount(ILogger<AccountBlockScheduled_BlockAccount> logger, IMediator mediator)
//        {
//            _logger = logger;
//            _mediator = mediator;
//        }

//        [Function(nameof(AccountBlockScheduled_BlockAccount))]
//        public async Task Run(
//            [ServiceBusTrigger(TOPICS.ADUserBlockScheduled, SUBSCRIPTIONS.ExecuteAccountBlock, Connection = "ServiceBusConnectionString")]
//            ServiceBusReceivedMessage message,
//            ServiceBusMessageActions messageActions)
//        {
//            _logger.LogInformation("Account block requested {InvocationId} - {MessageId}", System.Diagnostics.Activity.Current?.Baggage, message.MessageId);

//            var accountBlockScheduled = JsonConvert.DeserializeObject<AccountBlockScheduled>(message.Body.ToString());

//            var blockCommand = accountBlockScheduled?.ThrowIfNull().ToCommand();

//            var result = await _mediator.Send(blockCommand);

//            if (result.IsFailed)
//            {
//                //TODO: Improve the way of logging the erros to avoid serializing the FluentResult object
//                await messageActions.DeadLetterMessageAsync(message, deadLetterReason: "Block account failed");
//            }
//        }
//    }
//}
