using System.Collections.Specialized;
using System.Web.Mvc;
using NUnit.Framework;
using TegCMS.ModelBinding;
using TegCMS.Pages.Models;

namespace TegCMS.Tests.ModelBinding
{
    [TestFixture]
    public class ChildActionJsonModelBinderProviderTests
    {
        [Test]
        public void GetBinderReturnsChildActionJsonModelBinder()
        {
            Assert.That(new ChildActionJsonModelBinderProvider().GetBinder(typeof(object)),
                        Is.TypeOf<ChildActionJsonModelBinder>());
        }
    }

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
        public void ChildActionRequestWithBlankStringReturnsNull()
        {
            var childActionJsonModelBinder = new ChildActionJsonModelBinder();

            var controllerContext = new ControllerContext();

            controllerContext.RouteData.DataTokens.Add("ParentActionViewContext", true);
            controllerContext.RouteData.DataTokens.Add("configuration", string.Empty);

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
            var formCollection = new NameValueCollection 
                {
                    { "configuration", "{ \"Markdown\": \"One===\" }" }
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
