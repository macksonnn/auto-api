//using Core.Common.Extensions;

//namespace AutoMais.Integrations.Account.Commands
//{
//    public class Account_Block
//    {
//        private readonly ILogger _logger;
//        private readonly IMediator _mediator;

//        public Account_Block(ILogger<Account_Block> logger, IMediator mediator)
//        {
//            _logger = logger;
//            _mediator = mediator;
//        }

//        [Function(nameof(Account_Block))]
//        public async Task<IActionResult> RunAsync(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account/{id}/block")] HttpRequestData request,
//            [FromQuery] string id)
//        {
//            var blockAccountCommand = await request.ReadFromJsonAsync<AccountChangeRequestedCommand>().ThrowIfNull();

//            _logger.LogInformation("Block scheduled to {ScheduledDate}", blockAccountCommand.BlockingDate);

//            blockAccountCommand?.MakeItBlock(id);

//            var result = await _mediator.Send(blockAccountCommand);

//            if (result.IsSuccess)
//                return new OkObjectResult(result.Value);
//            else
//                //TODO: Implement a better way to track/log the failures and validations
//                return new BadRequestObjectResult(result.Errors);
//        }
//    }
//}