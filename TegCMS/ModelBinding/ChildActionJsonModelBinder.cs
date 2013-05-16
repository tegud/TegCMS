using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TegCMS.ModelBinding
{
    public class ChildActionJsonModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if(!controllerContext.IsChildAction)
            {
                return null;
            }

            object deserializedObject;

            try
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).RawValue as JObject;
                deserializedObject = value.ToObject(bindingContext.ModelType);
            }
            catch (Exception)
            {
                return null;
            }

            return deserializedObject;
        }
    }

}