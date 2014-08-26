using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Ravka.Extensions;

namespace EPA.Attributes.Validators
{

    public static class Common
    {
        public static string ValidationContextInfo(ValidationContext validationContext)
        {
            if (validationContext != null)
            {
                return " <!-- " + validationContext.ObjectInstance + " - " + validationContext.DisplayName + " - " + validationContext.ObjectType + " -->";
            }
            else
            {
                return string.Empty;
            }
        }
    }
    #region Validators

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequireForeignKeyAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // int id = Ravka.Numbers.ToInt(value);
            // if (id == 0)
            //  return new ValidationResult("This value is required.");

            return ValidationResult.Success;
        }

    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequireBelongsToForeignKeyAttribute : Attribute
    {

    }



    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class ValidateEmailAttribute : ValidationAttribute // DataTypeAttribute EmailProperty
    {
        private bool Required = false;


        public ValidateEmailAttribute(bool required = false)
            : base() // DataType.EmailAddress)
        {
            Required = required;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!EPA.Config.ValidateModel)
                return ValidationResult.Success;

            string valueAsString = Ravka.Strings.ToCleanString(value);
            if (valueAsString.Length == 0)
            {

                if (Required)
                    return new ValidationResult("Please enter the required Email Address" + Common.ValidationContextInfo(validationContext));
                else
                    return ValidationResult.Success;
            }

            if (Ravka.Validate.ValidEmail(valueAsString))
                return ValidationResult.Success;

            return new ValidationResult("Please enter a valid Email Address" + Common.ValidationContextInfo(validationContext));

        }

    }


 
      [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
      
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class NotRequiredAttribute : Attribute
    {

        public NotRequiredAttribute()
            : base()
        {

        }
 
    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class StringNotRequiredAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        public StringNotRequiredAttribute(int maxLength)
            : base(maxLength)
        {

        }

    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class RegularExpressionAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute
    {
        public RegularExpressionAttribute(string pattern)
            : base(pattern)
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (!EPA.Config.ValidateModel)
                return ValidationResult.Success;

            return base.IsValid(value, validationContext);


        }
        public override bool IsValid(object value)
        {
            if (!EPA.Config.ValidateModel)
                return true;

            return base.IsValid(value);
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class EnumRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
         
            if (!EPA.Config.ValidateModel)
                return ValidationResult.Success;

            string name = validationContext.DisplayName.ToCleanString();
            string vString = value.ToCleanString();
            if (vString.Length == 0)
                return new ValidationResult(name + " - Please select a value"); //) Sorry, you do not have the required member level.");

            if (vString == "Undefined" || vString == "0")
                return new ValidationResult(name + " - Please choose a value"); // LAUNCH TEMP ) Sorry, you do not have the required member level.");    
            /*
            int vInt = value.ToInt();
            if (vInt == 0)
                return new ValidationResult("Please choose a value"); //) Sorry, you do not have the required member level.");    
            */

            return ValidationResult.Success;

            return base.IsValid(value, validationContext);
        }
    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class StringRequiredAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        /*
        public StringNotRequiredAttribute()
            : base(0)
        {
            base.MaximumLength = EPA
        } */
        public StringRequiredAttribute(int maxLength, bool required = true)
            : base(maxLength)
        {

            if (required)
                base.MinimumLength = 1;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!EPA.Config.ValidateModel)
                return ValidationResult.Success;

            string s = value.ToCleanString();

            string name = validationContext.DisplayName.ToCleanString();
            if (base.MinimumLength != 0 && s.Length == 0)
                return new ValidationResult(name + " - Please enter a value"); //) Sorry, you do not have the required member level.");

           // if (base.MaximumLength != 0 && s.Length > base.MaximumLength)
            //     value = Ravka.Strings.Left(value, base.MaximumLength);

            return base.IsValid(value, validationContext);
        }
    }

    #endregion
}
