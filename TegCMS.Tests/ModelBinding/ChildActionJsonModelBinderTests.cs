using System.Collections.Specialized;
using System.Web.Mvc;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TegCMS.ModelBinding;
using TegCMS.Pages.Models;

namespace TegCMS.Tests.ModelBinding
{
    [TestFixture]
    public class ChildActionJsonModelBinderTests
    {
        [Test]
        public void NonChildActionRequestReturnsNull()
        {
            var childActionJsonModelBinder = new ChildActionJsonModelBinder();

            var controllerContext = new ControllerContext();

            var modelBindingContext = new ModelBindingContext();

            var result = childActionJsonModelBinder.BindModel(controllerContext, modelBindingContext);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ChildActionRequestWithNonMatchingJObjectStringReturnsNull()
        {
            var childActionJsonModelBinder = new ChildActionJsonModelBinder();

            var controllerContext = new ControllerContext();

            controllerContext.RouteData.DataTokens.Add("ParentActionViewContext", true);
            controllerContext.RouteData.DataTokens.Add("configuration", JObject.FromObject(new { x = 12345 }));

            var modelBindingContext = new ModelBindingContext
            {
                ValueProvider = new RouteDataValueProvider(controllerContext),
                ModelName = "configuration"
            };

            var result = childActionJsonModelBinder.BindModel(controllerContext, modelBindingContext);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ChildActionRequestWithValidHtmlConfigurationStringReturnsHtmlConfiguration()
        {
            var values = new ValueProviderDictionary(null)
                {
                    {"configuration", JObject.FromObject(new {Markdown = "One==="})},
                };

            var childActionJsonModelBinder = new ChildActionJsonModelBinder();

            var controllerContext = new ControllerContext();
            controllerContext.RouteData.DataTokens.Add("ParentActionViewContext", true);

            var modelBindingContext = new ModelBindingContext
            {
                ValueProvider = values, 
                ModelName = "configuration",
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MarkdownConfiguration))
            };

            var result = childActionJsonModelBinder.BindModel(controllerContext, modelBindingContext);

            Assert.That(result, Is.TypeOf<MarkdownConfiguration>());
        }

        [Test]
        public void ChildActionRequestWithInvalidHtmlConfigurationStringReturnsNull()
        {
            var formCollection = new NameValueCollection 
                {
                    { "configuration", "{ \"Markdown\": One===\" }" }
                };

            var childActionJsonModelBinder = new ChildActionJsonModelBinder();

            var controllerContext = new ControllerContext();

            controllerContext.RouteData.DataTokens.Add("ParentActionViewContext", true);
            controllerContext.RouteData.DataTokens.Add("", "");

            var modelBindingContext = new ModelBindingContext
            {
                ValueProvider = new NameValueCollectionValueProvider(formCollection, null),
                ModelName = "configuration",
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MarkdownConfiguration))
            };

            var result = childActionJsonModelBinder.BindModel(controllerContext, modelBindingContext);

            Assert.That(result, Is.Null);
        }
    }
}
