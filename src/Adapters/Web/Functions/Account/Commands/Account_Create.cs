//using AutoMais.Core.Domain.Aggregates.Account.Commands;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Http;

//namespace OpAutoMaistimus.Integrations.Account.Commands
//{
//    public class Account_Create
//    {
//        private readonly ILogger _logger;
//        private readonly IMediator _mediator;

//        public Account_Create(ILogger<Account_Create> logger, IMediator mediator)
//        {
//            _logger = logger;
//            _mediator = mediator;
//        }

//        [Function(nameof(Account_Create))]
//        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account/create")] HttpRequestData request)
//        {
//            var changeCommand = await request.ReadFromJsonAsync<AccountChangeRequestedCommand>();

//            changeCommand?.MakeItCreate();

//            var result = await _mediator.Send(changeCommand);

//            if (result.IsSuccess)
//                return new OkObjectResult(result.Value);
//            else
//                //TODO: Implement a better way to track/log the failures and validations
//                return new BadRequestObjectResult(result.Errors);
//        }
//    }
//}