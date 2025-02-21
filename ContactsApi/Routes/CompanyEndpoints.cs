using ContactsApi.Application.Commands.Companies;
using ContactsApi.Application.Commands.Contacts;
using ContactsApi.Application.Queries.Companies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.ContactsApi.Routes
{
    public static class CompanyEndpoints
    {
        public static void RegisterCompanyEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/companies");

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCompaniesQuery());
                return Results.Ok(result);
            });

            group.MapGet("/{id:int}", [Authorize] async (IMediator mediator, int id) =>
            {
                var result = await mediator.Send(new GetCompanyByIdQuery(id));
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateCompanyCommand command) =>
            {
                var companyId = await mediator.Send(command);
                return Results.Created($"/companies/{companyId}", new { Id = companyId });
            });

            group.MapPut("/{id:int}", async (IMediator mediator, int id, [FromBody] UpdateCompanyCommand command) =>
            {
                if (id != command.CompanyId) return Results.BadRequest("ID mismatch");

                var success = await mediator.Send(command);
                return success ? Results.Ok("Company updated successfully") : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (IMediator mediator, int id) =>
            {
                var success = await mediator.Send(new DeleteCompanyCommand(id));
                return success ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
