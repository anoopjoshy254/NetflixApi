using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetflixApi.Modules.Subscription.Interfaces;

namespace NetflixApi.Modules.Subscription.Filters
{
    public class SubscriptionActionFilter : IAsyncActionFilter
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionActionFilter(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userIdClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                var isActive = await _subscriptionService.IsActiveAsync(userId);
                if (!isActive)
                {
                    context.Result = new ObjectResult(new { message = "Active subscription required." })
                    {
                        StatusCode = 403
                    };
                    return;
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
