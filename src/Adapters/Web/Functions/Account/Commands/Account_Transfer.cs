//using AutoMais.Core.Domain.Aggregates.Account.Commands;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Http;

//namespace AutoMais.Integrations.Account.Commands
//{
//    public class Account_Transfer
//    {
//        private readonly ILogger _logger;
//        private readonly IMediator _mediator;

//        public Account_Transfer(ILogger<Account_Transfer> logger, IMediator mediator)
//        {
//            _logger = logger;
//            _mediator = mediator;
//        }

//        [Function(nameof(Account_Transfer))]
//        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account/{id}/transfer")] HttpRequestData request,
//            [FromQuery] string id)
//        {
//            _logger.LogInformation("Account transference requested {InvocationId}", System.Diagnostics.Activity.Current?.Baggage);

//            var transferCommand = await request.ReadFromJsonAsync<AccountChangeRequestedCommand>();
//            transferCommand?.MakeItTransfer(id);

//            var result = await _mediator.Send(transferCommand);

//            if (result.IsSuccess)
//                return new OkObjectResult(result.Value);
//            else
//                //TODO: Implement a better way to track/log the failures and validations
//                return new BadRequestObjectResult(result.Errors);
//        }
//    }
//}