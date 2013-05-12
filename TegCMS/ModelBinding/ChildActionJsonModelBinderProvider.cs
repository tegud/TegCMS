using System;
using System.Web.Mvc;

namespace TegCMS.ModelBinding
{
    public class ChildActionJsonModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return new ChildActionJsonModelBinder();
        }
    }
}