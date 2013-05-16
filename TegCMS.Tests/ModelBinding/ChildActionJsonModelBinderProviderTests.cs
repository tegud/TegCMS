using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TegCMS.ModelBinding;

namespace TegCMS.Tests.ModelBinding
{
    [TestFixture]
    public class ChildActionJsonModelBinderProviderTests
    {
        [Test]
        public void GetBinderReturnsChildActionJsonModelBinder()
        {
            Assert.That(new ChildActionJsonModelBinderProvider().GetBinder(typeof(JObject)),
                        Is.TypeOf<ChildActionJsonModelBinder>());
        }
    }
}