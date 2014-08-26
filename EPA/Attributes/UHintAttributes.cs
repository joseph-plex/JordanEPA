using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
// using System.Web.Routing;
using System.ComponentModel.DataAnnotations;
namespace EPA.Attributes
{
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            attributes.OfType<MetadataAttribute>().ToList().ForEach(x => x.Process(modelMetadata));
            attributes.OfType<MetadataDataTypeAttribute>().ToList().ForEach(x => x.Process(modelMetadata));
            return modelMetadata;
        }
    }
    public abstract class MetadataAttribute : Attribute
    {
        /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public abstract void Process(ModelMetadata modelMetaData);
    }
    public abstract class MetadataDataTypeAttribute : DataTypeAttribute
    {

        public MetadataDataTypeAttribute(DataType dataType)
            : base(dataType)
        {

        }
        /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public abstract void Process(ModelMetadata modelMetaData);
    }

    /*   public abstract class MetadataDefaultValueAttribute : System.ComponentModel.DefaultValueAttribute
    {
           public MetadataDefaultValueAttribute(DataType dataType)
            : base(dataType)
        {
            
        }
                /// <summary>
        /// Method for processing custom attribute data.
        /// </summary>
        /// <param name="modelMetaData">A ModelMetaData instance.</param>
        public abstract void Process(ModelMetadata modelMetaData);
       } */
}

 
namespace EPA.Attributes.UHints
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.ReturnValue | AttributeTargets.Property, AllowMultiple = true)]
    public class UIHintAttribute : System.ComponentModel.DataAnnotations.UIHintAttribute
    {
        public UIHintAttribute(string value)
            : base(value)
        {

        }
    }

 
    public class FormStyleAttribute : MetadataAttribute
    {
        public Ravka.Globals.Css.FormInputStyle FormInputStyle;
        public FormStyleAttribute(Ravka.Globals.Css.FormInputStyle formInputStyle)
        {
            FormInputStyle = formInputStyle;
        }
        public override void Process(ModelMetadata modelMetaData)
        {
            modelMetaData.AdditionalValues.Add("FormInputStyle", this.FormInputStyle);
        }
    }

  
}
