using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace Jayway.Throttling.Web.ModelBinders
{
    public class SingleAccountModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var account = bindingContext.ValueProvider.GetValue("account").AttemptedValue;
            var cost = int.Parse(bindingContext.ValueProvider.GetValue("cost").AttemptedValue);
            var intervalInSeconds = int.Parse(bindingContext.ValueProvider.GetValue("intervalInSeconds").AttemptedValue);
            var creditsPerIntervalValue = int.Parse(bindingContext.ValueProvider.GetValue("creditsPerInterval").AttemptedValue);

            var result = new SingleAccountInputModel(account, cost, intervalInSeconds, creditsPerIntervalValue);

            bindingContext.Model = result;

            return true;
        }
    }
}