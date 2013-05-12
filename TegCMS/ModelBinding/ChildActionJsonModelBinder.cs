using System;
using System.Web.Mvc;
using Newtonsoft.Json;

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
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
                deserializedObject = JsonConvert.DeserializeObject(value, bindingContext.ModelType);
            }
            catch (Exception)
            {
                return null;
            }

            return deserializedObject;
        }
    }

}