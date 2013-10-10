using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace Jayway.Throttling.Web.ValueProviders
{
    public class BodyValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(HttpActionContext actionContext)
        {
            return new BodyValueProvider(actionContext);
        }
    }
}