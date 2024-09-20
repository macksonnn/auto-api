using Core.Application.Ticket.Queries;
using Core.Domain.Aggregates.Ticket;
using Core.Domain.Aggregates.Ticket.Commands;
using Core.Domain.Aggregates.Ticket.Events;
using System.Threading;

namespace AutoMais.Ticket.Api.Controllers
{
    //public class TicketEndpoints : IEndpointDefinition
    //{
    //    //TODO: Find a way to register a global endpoint filter to manipulare the result
    //    //TODO: view https://khalidabuhakmeh.com/global-endpoint-filters-with-aspnet-core-minimal-apis
    //    public void RegisterEndpoints(WebApplication app)
    //    {
    //        app.MapPost("/api/ticket", async ([FromBody] CreateTicketCommand command, IMediator mediator) =>
    //        {
    //            return await mediator.Send(command);
    //        });

    //        app.MapGet("/api/ticket", async ([FromBody] CreateTicketCommand command, IMediator mediator) =>
    //        {
    //            return await mediator.Send(command);
    //        });
    //    }
    //}


    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<Result<TicketAgg>> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            var query = new TicketGetOne(id);
            return _mediator.Send(query, cancellationToken);
        }


        [HttpPost()]
        public Task<Result<TicketCreated>> Create([FromBody] CreateTicketCommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}
