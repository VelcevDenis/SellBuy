using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;
using SellBuy.Repositories;

namespace SellBuy.Services.Helpers
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params UserRole[] roles) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        UsersRepository _clientsRepository;
        public UserRole[] Roles { get; }
        User User;

        public AuthorizeFilter(UserRole[] roles, UsersRepository clientsRepository)
        {
            Roles = roles;
            _clientsRepository = clientsRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (!Roles.Any())
                return;

            var userId = context.HttpContext.User.Claims.Where(x => x.Type == "id").Select(y => y.Value).FirstOrDefault();
            if (userId != null)
            {
                User = Task.Run(async () => await _clientsRepository.GetById(int.Parse(userId))).WaitAndUnwrapException();

                var isAuthorize = IsAuthorize(
                        context.Filters
                            .Where(w => w.GetType() == typeof(AuthorizeFilter))
                            .Cast<AuthorizeFilter>()
                            .ToList());

                if (!isAuthorize)
                    context.Result = new ForbidResult();
            }
            else
            {
                context.Result = new ForbidResult();
                return;
            }
        }

        private bool IsAuthorize(IEnumerable<AuthorizeFilter> filters)
             => filters.Any(filter => IsAuthorize(filter.Roles));

        private bool IsAuthorize(UserRole[] roles)
            => !roles.Any() || roles.Any(role => role == User.Role) || User.Role == UserRole.Admin;               
    }
}
