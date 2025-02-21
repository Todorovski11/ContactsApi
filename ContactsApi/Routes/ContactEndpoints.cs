using ContactsApi.Application.Commands.Contacts;
using ContactsApi.Application.Queries.Contacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.ContactsApi.Routes
{
    public static class ContactEndpoints
    {
        public static void RegisterContactEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/contacts");

            group.MapGet("/", async (IMediator mediator, int? countryId, int? companyId, int skip = 0, int take = 10) =>
            {
                var result = await mediator.Send(new GetAllContactsQuery(countryId, companyId, skip, take));
                return Results.Ok(result);
            });

            group.MapGet("/{id:int}", async (IMediator mediator, int id) =>
            {
                var result = await mediator.Send(new GetContactByIdQuery(id));
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateContactCommand command) =>
            {
                var contactId = await mediator.Send(command);
                return Results.Created($"/contacts/{contactId}", new { Id = contactId });
            });

            group.MapPut("/{id:int}", async (IMediator mediator, int id, [FromBody] UpdateContactCommand command) =>
            {
                if (id != command.ContactId) return Results.BadRequest("ID mismatch");

                var success = await mediator.Send(command);
                return success ? Results.Ok("Contact updated successfully") : Results.NotFound();
            });

            group.MapDelete("/{id:int}", [Authorize] async (IMediator mediator, int id) =>
            {
                var success = await mediator.Send(new DeleteContactCommand(id));
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
