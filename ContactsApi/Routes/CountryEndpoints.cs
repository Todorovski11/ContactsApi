using ContactsApi.Application.Commands.Countries;
using ContactsApi.Application.Queries.Countries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.ContactsApi.Routes
{
    public static class CountryEndpoints
    {
        public static void RegisterCountryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/countries");

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCountriesQuery());
                return Results.Ok(result);
            });

            group.MapGet("/{id:int}", async (IMediator mediator, int id) =>
            {
                var result = await mediator.Send(new GetCountryByIdQuery(id));
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateCountryCommand command) =>
            {
                var countryId = await mediator.Send(command);
                return Results.Created($"/countries/{countryId}", new { Id = countryId });
            });

            group.MapPut("/{id:int}", async (IMediator mediator, int id, [FromBody] UpdateCountryCommand command) =>
            {
                if (id != command.CountryId) return Results.BadRequest("ID mismatch");

                var success = await mediator.Send(command);
                return success ? Results.Ok("Country updated successfully") : Results.NotFound();
            });

            group.MapDelete("/{id:int}", [Authorize] async (IMediator mediator, int id) =>
            {
                var success = await mediator.Send(new DeleteCountryCommand(id));
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapGet("/{id:int}/company-stats", async (IMediator mediator, int id) =>
            {
                var result = await mediator.Send(new GetCompanyStatisticsQuery(id));
                return Results.Ok(result);
            });
        }
    }
}
