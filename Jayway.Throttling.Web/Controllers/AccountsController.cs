using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Jayway.Throttling.Web.ModelBinders;

namespace Jayway.Throttling.Web.Controllers
{
    public class AccountsController : ApiController
    {
        readonly IThrottlingService _throttlingService;

        public AccountsController() : this(new AzureCacheThrottlingContext())
        {
        }

        public AccountsController(IThrottlingContext throttlingContext)
        {
            _throttlingService = throttlingContext.GetThrottlingService();
        }

        [HttpPost("accounts/{account}/demo")]
        public HttpResponseMessage Demo(string account)
        {
            bool allow = _throttlingService.Allow(account, 1, () => new Interval(60, 10));
            return allow
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateResponse(HttpStatusCode.PaymentRequired, "THROTTLED");
        }

        [HttpPost("single")]
        public HttpResponseMessage Single(
            [ModelBinder(typeof (SingleAccountModelBinder))] SingleAccountInputModel singleAccountInput)
        {
            bool allow =
                new ThrottledRequest(_throttlingService, singleAccountInput.Cost, singleAccountInput.IntervalInSeconds,
                    singleAccountInput.CreditsPerIntervalValue).Perform(singleAccountInput.Account);
            return allow
                ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateResponse(HttpStatusCode.PaymentRequired, "THROTTLED");
        }

        [HttpPost("multi")]
        public dynamic Multi(string account, int calls, int accounts, int cost, int intervalInSeconds,
            long creditsPerIntervalValue)
        {
            var r = new ThrottledRequest(_throttlingService, cost, intervalInSeconds, creditsPerIntervalValue);
            var throttledCount = 0;
            long startTime = DateTime.Now.Millisecond; //System.currentTimeMillis();
            for (var indx = 0; indx < calls; indx++)
            {
                var randomAccount = "a" + new Random().Next()*accounts;
                if (!r.Perform(randomAccount))
                {
                    throttledCount++;
                }
            }
            var time = DateTime.Now.Millisecond - startTime;
            return new {calls, time, throttledCount};
        }
    }


    //TODO create filter
}