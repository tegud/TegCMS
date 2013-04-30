using System.Web;

namespace TegCMS.Utilities
{
    public interface IMvcRequestFactory
    {
        MvcRequest Build(HttpRequestBase httpRequest);
    }

    public class MvcRequestFactory : IMvcRequestFactory
    {
        public MvcRequest Build(HttpRequestBase httpRequest)
        {
            return new MvcRequest(httpRequest);
        }
    }

    public class MvcRequest
    {
        private readonly HttpRequestBase _httpRequest;

        public MvcRequest(HttpRequestBase httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public string GetUrlHostName()
        {
            return _httpRequest.Url.Host;
        }
    }
}