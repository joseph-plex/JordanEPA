using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Ravka.Extensions;
 

namespace EPA.Attributes
{

    public class ModelFormAttributes
    {
        //  public string MemberName { get; set; }
        public string Label { get; set; }
        public string Note { get; set; }
        public bool IsRequired { get; set; }

    }

    public static class Get
    {
        public static string PropertyName<T, V>(Expression<Func<T, V>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.Name;
        }
        private static T GetAttribute<T>(ICustomAttributeProvider provider)
                 where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }
        public static bool IsRequired<T, V>(Expression<Func<T, V>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            if (GetAttribute<RequiredAttribute>(memberExpression.Member) != null)
                return true;

            var sl = GetAttribute<StringLengthAttribute>(memberExpression.Member);
            if (sl != null && sl.MinimumLength != 0)
                return true;
            return false;
        }

       


    }

}
