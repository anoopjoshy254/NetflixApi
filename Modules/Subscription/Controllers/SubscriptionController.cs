using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Subscription.Interfaces;

namespace NetflixApi.Modules.Subscription.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("plans")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _subscriptionService.GetActivePlansAsync();
            return Ok(plans);
        }

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<IActionResult> Subscribe([FromBody] int planId)
        {
            var userId = GetUserId();
            var success = await _subscriptionService.SubscribeAsync(userId, planId);
            if (!success) return BadRequest("Unable to subscribe. You may already have an active subscription.");
            return Ok(new { message = "Subscribed successfully" });
        }

        [HttpPost("upgrade")]
        [Authorize]
        public async Task<IActionResult> Upgrade([FromBody] int newPlanId)
        {
            var userId = GetUserId();
            var success = await _subscriptionService.UpgradeAsync(userId, newPlanId);
            if (!success) return BadRequest("Unable to upgrade subscription.");
            return Ok(new { message = "Upgraded successfully" });
        }

        [HttpPost("downgrade")]
        [Authorize]
        public async Task<IActionResult> Downgrade([FromBody] int newPlanId)
        {
            var userId = GetUserId();
            var success = await _subscriptionService.DowngradeAsync(userId, newPlanId);
            if (!success) return BadRequest("Unable to downgrade subscription.");
            return Ok(new { message = "Downgraded successfully" });
        }

        [HttpPost("cancel")]
        [Authorize]
        public async Task<IActionResult> Cancel()
        {
            var userId = GetUserId();
            var success = await _subscriptionService.CancelAsync(userId);
            if (!success) return BadRequest("Unable to cancel. No active subscription found.");
            return Ok(new { message = "Subscription cancelled successfully" });
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMySubscription()
        {
            var userId = GetUserId();
            var sub = await _subscriptionService.GetMySubscriptionAsync(userId);
            if (sub == null) return NotFound("No active subscription found.");
            return Ok(sub);
        }

        [HttpGet("status")]
        [Authorize]
        public async Task<IActionResult> GetStatus()
        {
            var userId = GetUserId();
            var status = await _subscriptionService.GetStatusAsync(userId);
            return Ok(status);
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }
    }
}
