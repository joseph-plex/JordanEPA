using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Ravka.Extensions;
using System.Web.Mvc;

namespace EPA
{
    public partial class Constants
    {
        public const int MaxVarcharLength = 4000;
        public const bool SemiThreadedBoards = false;
    }

}
namespace EPA.Attributes
{




 

    
        [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OrderAttribute : Ravka.Attributes.OrderAttribute //  : Attribute 
    {
         
            public OrderAttribute(int orderValue)
                : base(orderValue)
        {
         
            }
    }

    [Serializable]
        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class HiddenEnumAttribute : Ravka.Attributes.HiddenEnumAttribute // Attribute
        {
      
            public HiddenEnumAttribute() : base()
            {
          
            }
            public HiddenEnumAttribute(bool isHidden)
                : base(isHidden)
            {
            }
        }


    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.ReturnValue | AttributeTargets.Property, AllowMultiple = true)]
    public class ScaffoldColumnAttribute : System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute
    {
        public ScaffoldColumnAttribute(bool value)
            : base(value)
        { }
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HtmlPropertyAttribute : MetadataDataTypeAttribute //System.ComponentModel.DataAnnotations.DataTypeAttribute // .ColumnAttribute // StringPropertyAttribute
    {
     //   private EPA.Globals.MessageType UseMessageTypeTemplate { get; set; }
     //   private int UseTemplateConstantTemplate { get; set; }

        public HtmlPropertyAttribute(bool required, string defaultValue = "")
            : base(DataType.Html) // 0, required)
        {
            // base.ColumnType = "StringClob";
            //   base.TypeName = "Text";
       //     UseMessageTypeTemplate = useMessageTypeTemplate;
      //      UseTemplateConstantTemplate = useTemplateConstantTemplate;

        }
      
        public override void Process(ModelMetadata modelMetaData)
        {
            /*   modelMetaData.AdditionalValues.Add("FormInputStyle", Ravka.Globals.Css.FormInputStyle.Html);
             if (UseTemplateConstantTemplate != 0)
                 modelMetaData.AdditionalValues.Add("TemplateConstant", UseTemplateConstantTemplate);
            if (UseMessageTypeTemplate != EPA.Globals.MessageType.Undefined)
                 modelMetaData.AdditionalValues.Add("MessageType", UseMessageTypeTemplate);

             // modelMetaData.AdditionalValues.Add("AutoCompleteUrlData", this.RouteValueDictionary);  */
         
            if (modelMetaData.TemplateHint == null)
                modelMetaData.TemplateHint = "Html"; 
          
        }
       
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class TextPropertyAttribute : MetadataDataTypeAttribute // System.ComponentModel.DataAnnotations.DataTypeAttribute // StringPropertyAttribute
    {
  
        public TextPropertyAttribute(bool required, int rows = 2)
            : base(DataType.MultilineText) //   : base(0, required)
        {
          
        }

        public override void Process(ModelMetadata modelMetaData)
        {
         
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class UrlAttribute :  MetadataDataTypeAttribute // System.ComponentModel.DataAnnotations.DataTypeAttribute // .DataTypeAttribute // PropertyAttribute
    {
        public UrlAttribute(bool required) // , string defaultValue = ""
            : base(DataType.Url)
        {
            
        }
    
        public override void Process(ModelMetadata modelMetaData)
        {
              modelMetaData.TemplateHint = "Url";
        }  
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class StringPropertyAttribute : MetadataDataTypeAttribute // System.ComponentModel.DataAnnotations.DataTypeAttribute // .DataTypeAttribute // PropertyAttribute
    {
        public StringPropertyAttribute(int maxLength, bool required, bool isLongText = false) // , string defaultValue = ""
            : base(DataType.Text)
        {
      
        }

        private bool IsLongText { get; set; }
        public override void Process(ModelMetadata modelMetaData)
        {
   
            modelMetaData.TemplateHint = "String";
        } 
    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class EmailPropertyAttribute : StringLengthAttribute  // DataAnnotationsExtensions.EmailAttribute // System.ComponentModel. Attribute // Castle.Components.Validator.ValidateEmailAttribute
    {
        // THIS IS NOT NEEDED, DONE IN DATA TYPE ATTRIBUTE
        private bool Required;

        public EmailPropertyAttribute(bool required)
            : base(64)
        {

            Required = required;
        }
        /* 
         protected override ValidationResult IsValid(object value, ValidationContext validationContext)
         {
             if (EPA.Config.ValidateModel)
                 return base.IsValid(value, validationContext);
             else
                 return ValidationResult.Success;
         }
      */
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class EnumPropertyAttribute :  MetadataAttribute // UIHintAttribute // Attribute
    {
        /*
        public bool AddBlank { get; set; }
        public int IgnoreTheseEnumsAsInt { get; set; }
        public int OnlyTheseEnumsAsInt { get; set; }
        public bool AllowFlags { get; set; }
        public bool RadioButtonList { get; set; }
        public bool AllowMultiple { get; set; }
        public int SelectAllFlagAsInt { get; set; } 
        */
        public EnumPropertyAttribute(bool required = false, int defaultValue = 0,
            bool addBlankInDropDownList = true, 
            int ignoreTheseEnumsAsInt = 0,
            int onlyTheseEnumsAsInt = 0,
            bool allowFlags = false,
            bool radioButtonList = false,
            bool allowMultiple = false,
            int selectAllFlagAsInt = 0)
            : base() // "EnumsDropDown")
        {
 
        }
      public override void Process(ModelMetadata modelMetaData)
        {
 
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class PhoneNumberPropertyAttribute : System.ComponentModel.DataAnnotations.DataTypeAttribute
    {
        public PhoneNumberPropertyAttribute(bool required = false)
            : base(DataType.PhoneNumber)
        {
            /*  NotNull = true;

              if (!required)
              {
                  Default = "0";
              }
              */
        }
    }




    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class IntPropertyAttribute : UIHintAttribute // Attribute // System.ComponentModel.DataAnnotations.DataTypeAttribute // System.ComponentModel.DataAnnotations.ColumnAttribute // PropertyAttribute
    {
        public IntPropertyAttribute(bool required = false, int defaultValue = 0, string columnNameOveride = "")
            : base("Integer")
        {
            // note, columnNameOveride is not implemented
            // NotNull = true;

            /*
            if (!string.IsNullOrEmpty(columnNameOveride))
                base. = columnNameOveride;

            if (!required)
            {
                Default = defaultValue.ToString();
            }
            */
        }

    }



    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class DoublePropertyAttribute : UIHintAttribute //PropertyAttribute
    {
        public DoublePropertyAttribute(bool required = false, double defaultValue = 0)
            : base("Double")
        {
            /*  NotNull = true;
            if (!required)
                Default = defaultValue.ToString(); */
        }
    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class DecimalPropertyAttribute : UIHintAttribute //PropertyAttribute
    {
        public byte Precision { get; set; }
        public byte Scale { get; set; }


        public DecimalPropertyAttribute(bool required = false, double defaultValue = 0, byte totalNumberOfDigits = 10, byte decimalPlaces = 2)
            : base("Decimal")
        {

            Precision = totalNumberOfDigits;
            Scale = decimalPlaces;

            /*  NotNull = true;
            if (!required)
                Default = defaultValue.ToString(); */
        }
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class NullIntPropertyAttribute : Attribute // PropertyAttribute
    {
        public NullIntPropertyAttribute(bool required = false)
            : base()
        {
            /*  if (required)
                NotNull = true;
          
            if (!required)
            {
                if (defaultValue != null)
                    Default = ((int)defaultValue).ToString();
            }
            */
        }
    }
        [Serializable]
    public enum DateDefault
    {
        MinDate,
        MaxDate,
        Undefined
    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class DatePropertyAttribute : Attributes.UHints.UIHintAttribute
        // System.ComponentModel.DefaultValueAttribute // PropertyAttribute
    {
        public DatePropertyAttribute(bool required = false, DateDefault defaultValue = DateDefault.MinDate) // = true DateTime? defaultValue = null)
            : base("Date")
        {
            /*   NotNull = true;

               if (!required)
               {
                   if (defaultValue == DateDefault.MaxDate)
                       Default = Ravka.Dates.SqlDateText(Ravka.Dates.SqlMinDate);
                   else
                       Default = Ravka.Dates.SqlDateText(Ravka.Dates.SqlMaxDate);

               }
               */

        }

        public DatePropertyAttribute(bool required, string defaultFormattedDateForSql) // = true DateTime? defaultValue = null)
            : base(defaultFormattedDateForSql)
        {
            /*    NotNull = true;

                if (!required && String.IsNullOrEmpty(defaultFormattedDateForSql) == false)
                {
                    Default = defaultFormattedDateForSql;

                }
                */
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class DateTimePropertyAttribute : Attributes.UHints.UIHintAttribute
    // System.ComponentModel.DefaultValueAttribute // PropertyAttribute
    {
        public DateTimePropertyAttribute(bool required = false, DateDefault defaultValue = DateDefault.MinDate) // = true DateTime? defaultValue = null)
            : base("DateTime")
        {
 

        }

        public DateTimePropertyAttribute(bool required, string defaultFormattedDateForSql) // = true DateTime? defaultValue = null)
            : base("DateTime") // base(defaultFormattedDateForSql)
        {
  
        }
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class TimePropertyAttribute : Attributes.UHints.UIHintAttribute
    // System.ComponentModel.DefaultValueAttribute // PropertyAttribute
    {
        public TimePropertyAttribute(bool required = false, DateDefault defaultValue = DateDefault.MinDate) // = true DateTime? defaultValue = null)
            : base("Time")
        {

        }

        public TimePropertyAttribute(bool required, string defaultFormattedDateForSql) // = true DateTime? defaultValue = null)
            : base("Time") // base(defaultFormattedDateForSql)
        {
     
        }
    }
  

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class BoolPropertyAttribute : MetadataAttribute  
    {
        public BoolPropertyAttribute(bool required = false, bool defaultValue = false)
            : base() // defaultValue
        {
            /*    NotNull = true;

                if (!required) // && defaultValue != null)
                {
                    Default = Ravka.Numbers.ToInt((bool)defaultValue).ToString();
                }
                */
        }


        public override void Process(ModelMetadata modelMetaData)
        {
            modelMetaData.AdditionalValues.Add("FormInputStyle", Ravka.Globals.Css.FormInputStyle.CheckBox);
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class IgnoreAttribute : System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute
    {
        public IgnoreAttribute()
            : base()
        {


        }
    }

  

}
