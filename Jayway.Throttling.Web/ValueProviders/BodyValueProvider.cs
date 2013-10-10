using System.Collections.Specialized;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;

namespace Jayway.Throttling.Web.ValueProviders
{
    public class BodyValueProvider : IValueProvider
    {
        readonly NameValueCollection _allValues;

        public BodyValueProvider(HttpActionContext actionContext)
        {
            _allValues = actionContext.Request.Content.ReadAsFormDataAsync().Result;
        }

        public bool ContainsPrefix(string prefix)
        {
            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            var rawValue = _allValues.Get(key);

            return new ValueProviderResult(rawValue, rawValue, CultureInfo.CurrentCulture);
        }
    }
}