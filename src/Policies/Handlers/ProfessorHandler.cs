using edu_for_you.Models;
using edu_for_you.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace edu_for_you.Policies.Handlers;

public class ProfessorHandler : AuthorizationHandler<ProfessorRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfessorRequirement requirement)
    {
        if (context.User.FindFirst(Usuario.ClaimType.PerfilProfessorId) != null)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
