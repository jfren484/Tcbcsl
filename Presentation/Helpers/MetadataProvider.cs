using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Helpers
{
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            var inputTypeAttr = attributes.OfType<InputTypeAttribute>().SingleOrDefault();

            if (inputTypeAttr != null)
            {
                metadata.AdditionalValues.Add("InputType", inputTypeAttr);
            }

            return metadata;
        }
    }
}