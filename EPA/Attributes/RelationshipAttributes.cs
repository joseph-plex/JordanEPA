using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
// using Ravka.Extensions;

namespace EPA.Attributes.Relationships
{
    #region Tables

    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : System.ComponentModel.DataAnnotations.Schema.TableAttribute
    {
        public TableAttribute(string value)
            : base(value)
        {
        }
        public TableAttribute(string value, string DiscriminatorValue, string DiscriminatorColumn = "", string DiscriminatorType = "")
            : base(value)
        {

            // throw new NotImplementedException();
        }

    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class ComplexTypeAttribute : System.ComponentModel.DataAnnotations.Schema.ComplexTypeAttribute
    {
        public ComplexTypeAttribute()
            : base()
        {
            // throw new NotImplementedException();
        }

    }




    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class NestedComplexTypeAttribute : Attribute // Castle.ActiveRecord.NestedAttribute
    {
        public NestedComplexTypeAttribute(bool required)
            : base()
        {
            // if possible, if required than it should overide the elements
            // probably not possible
        }

    }
    #endregion


    #region Relationships
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class ForeignKeyAttribute : System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute // PropertyAttribute
    {
        public ForeignKeyAttribute(string nameofObject, bool required)
            : base(nameofObject)
        {

            //  NotNull = required;
        }

    }



    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class BelongsToForeignKeyAttribute : System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute // InversePropertyAttribute 
    {

        public BelongsToForeignKeyAttribute(string foreignKeyName, bool required, bool HasRealForeignKey = true, bool lazy = true)
            : base(foreignKeyName)
        {
            /*  if (required)
                  Cascade = CascadeEnum.All;
              else
                  Cascade = CascadeEnum.SaveUpdate;


              NotNull = required;
              Lazy = lazy ? FetchWhen.OnInvoke : FetchWhen.Immediate;
              */
            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class OneToOneAttribute : Attribute // Castle.ActiveRecord.OneToOneAttribute
    {

        public OneToOneAttribute(bool lazy = true)
            : base()
        {


            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }

    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HasManyBothSidesAttribute : Attribute // Castle.ActiveRecord.HasManyAttribute
    {

        public HasManyBothSidesAttribute(Type mapType, bool requireAtleastOneEntry = false, bool lazy = true)
            : base() // base(mapType)
        {
            /*
            Inverse = true;
            Lazy = lazy;

            base.Cascade = ManyRelationCascadeEnum.AllDeleteOrphan; // AllDeleteOrphan;

            // if (HasRealForeignKey)
            // throw new NotImplementedException();
            */

        }

    }




    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HasManySimplesAttribute : Attribute // Castle.ActiveRecord.HasManyAttribute
    {

        public HasManySimplesAttribute(Type mapType, bool orderIsImportant, string MyEntityColumnName, string WhateverThisIsColumnName, string table, bool requireAtleastOneEntry = false, bool lazy = true)
            : base() // base(mapType, RelationType.Guess)
        {
            /*
            base.Cascade = ManyRelationCascadeEnum.All; // AllDeleteOrphan;
            base.Index = "DisplayOrder";
            base.RelationType = Castle.ActiveRecord.RelationType.List;
            // launch temp orderIsImportant
            //  RelationType = Castle.ActiveRecord.RelationType.List;
            base.ColumnKey = MyEntityColumnName;
            //  base.ColumnRef = WhateverThisIsColumnName;
            base.Table = table;
            base.ElementType = mapType;
            base.Element = WhateverThisIsColumnName;
            Inverse = true;
            Lazy = lazy;
            */
            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }

    }

    /*
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HasManyJustThisSidesAttribute :  Castle.ActiveRecord.HasManyAttribute
    {

        public HasManyJustThisSidesAttribute(Type mapType, string columnKey, string table, bool requireAtleastOneEntry = false, bool lazy = true)
            : base(mapType, columnKey, table)
        {

            Lazy = lazy;

            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }

    }
    */
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HasManyToManyBothSidesAttribute : Attribute // Castle.ActiveRecord.HasAndBelongsToManyAttribute
    {

        public HasManyToManyBothSidesAttribute(Type mapType, string MyEntityColumnName, string WhateverThisIsColumnName, string table, bool requireAtleastOneEntry = false, bool lazy = true)
            : base() // base(mapType, RelationType.Guess)
        {
            /*   base.ColumnKey = MyEntityColumnName;
               base.ColumnRef = WhateverThisIsColumnName;
               base.Table = table;

               Lazy = lazy;
               */
            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }

    }
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.ReturnValue | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HasManyToManyJustThisSideAttribute : Attribute // Castle.ActiveRecord.HasAndBelongsToManyAttribute
    {

        public HasManyToManyJustThisSideAttribute(Type mapType, bool OrderIsImportant, string MyEntityColumnName, string WhateverThisIsColumnName, string table, bool requireAtleastOneEntry = false, bool lazy = true)
            : base() // mapType, RelationType.Guess)
        {
            /*
            // do something based on lazy to load up everything (like topics)
            // LAUNCH TEMP base.Fetch = FetchEnum.Unspecified;
            base.ColumnKey = MyEntityColumnName;
            base.ColumnRef = WhateverThisIsColumnName;
            base.Table = table;

            //if (OrderIsImportant)
            //  base.RelationType = Castle.ActiveRecord.RelationType.List;

            Lazy = lazy;
            */
            // if (HasRealForeignKey)
            // throw new NotImplementedException();
        }

    }
    #endregion

}
