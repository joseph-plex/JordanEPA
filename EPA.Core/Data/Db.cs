using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Plexxis.Helpers.Extensions;
using System.Data.Common;
// using Effort.DataLoaders;
using System.Data.EntityClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace EPA.Data
{
    public partial class Db : Models.DbFirstEntities
    {
        public Db()
            : base()
        {

        }
    }
    public partial class MockDb : Models.DbFirstEntities
    {
        public MockDb()
            : base()
        {

        }
     /*   public MockDb()
            : base(MockConnectionWithData())
        {

        }

        private static DbConnection MockConnectionWithData()
        {
            IDataLoader loader = new EntityDataLoader("name=DbFirstEntities");

           // return Effort.EntityConnectionFactory.CreateTransient("name=DbFirstEntities");
            return Effort.EntityConnectionFactory.CreateTransient("name=DbFirstEntities", loader);
           //  return Effort.DbConnectionFactory.CreateTransient(loader);

        } */
    }

    public enum Sequence
    {
        COMPANY_ID,
       // C_SUPPLIER_ID,
        COMPANY_SUPPLIERS_ID,
        // C_USER_ID,
        COMPANY_USER_SUPPLIERS_ID,
        COMPANY_USER_ID,
      //  PA_ADJUSTMENTS_ID,
       // PA_ID,
       // PAI_ID,
        PRICE_AGREEMENT_ID,
        PRICE_AGREEMENT_MATERIAL_ID,
        PRICE_AGREEMENT_ADJUSTMENT_ID,
        PRICE_LIST_ID,
        PRICE_LIST_MATERIAL_ID,
        PRICE_LIST_ITEM_TYPES_ID,
        SUPPLIER_ID,
        WINDEV_CLIENT_VERSION_ID
    };
}

namespace EPA.Models
{



    public partial class DbFirstEntities : DbContext
    {

        public DbFirstEntities(DbConnection connection)
            : base(connection, true)
        {
        }
        public T AssignPrimaryKey<T, TProperty>(T entity, Expression<Func<TProperty>> expr, EPA.Data.Sequence sequence)
        {
            return AssignPrimaryKey<T, TProperty>(entity, expr, sequence.ToString().ToLower());
        }
        public T AssignPrimaryKey<T, TProperty>(T entity, Expression<Func<TProperty>> expr, string sequenceName)
        {
            string classSeperator = ".";
            var ex = ((MemberExpression)expr.Body);
            string keyColumnName = Plexxis.Helpers.Strings.TextAfterThis(Plexxis.Helpers.Strings.TextAfterThis(ex.ToString(), ")."), ".");
            if (classSeperator != ".")
                keyColumnName = keyColumnName.Replace(".", classSeperator);

            var nextVal = this.Database.SqlQuery<int>("select " + sequenceName + ".nextval from dual").First();

            PropertyInfo propertyInfo = entity.GetType().GetProperty(keyColumnName);
            propertyInfo.SetValue(entity, Convert.ChangeType(nextVal, propertyInfo.PropertyType), null);

           
            return entity;

        }

        public void SetToModified<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = System.Data.EntityState.Modified;
        }
        public void SetToDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = System.Data.EntityState.Deleted;
        }
        /// <summary>
        /// Overides Save, converts exceptions to Outcome
        /// </summary>
        /// <param name="importantLogging"></param>
        /// <param name="disableValidations"></param>
        /// <returns></returns>
        public Plexxis.Helpers.Outcome Save(bool importantLogging = false, bool disableValidations = false)
        {
         
            var Outcome = new Plexxis.Helpers.Outcome();
            string errorMessage = string.Empty;

            if (disableValidations)
                this.Configuration.ValidateOnSaveEnabled = false;

            try
            {
                SaveChanges();
                Outcome.AddSuccess(""); // , id);

            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {

                    // todo : add entity name
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        Outcome.AddError(validationError.ErrorMessage);
                       Plexxis.Helpers.Logger.Warn(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + " " +
                            dbEx.StackTrace + " " + dbEx.Source);
                    }
                }

            }
              catch (   System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                        errorMessage = ex.InnerException.InnerException.Message.ToString();
                    else
                        errorMessage = ex.InnerException.ToString();
                }

                else
                    errorMessage = ex.ToString();
            }
            catch (System.Data.UpdateException ex)
            {

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                        errorMessage = ex.InnerException.InnerException.Message.ToString();
                    else
                        errorMessage = ex.InnerException.ToString();
                }
                else
                    errorMessage = ex.ToString();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                        errorMessage = ex.InnerException.InnerException.Message.ToString();
                    else
                        errorMessage = ex.InnerException.ToString();
                }

                else
                    errorMessage = ex.ToString();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                        errorMessage = ex.InnerException.InnerException.Message.ToString();
                    else
                        errorMessage = ex.InnerException.ToString();
                }
                else
                    errorMessage = ex.ToString();


            }

            if (disableValidations)
                this.Configuration.ValidateOnSaveEnabled = true;

            if (!errorMessage.IsNullOrEmpty())
            {
                errorMessage = errorMessage.Replace("An error occurred while updating the entries. See the inner exception for details.", "");
                Outcome.AddError(errorMessage);
                Plexxis.Helpers.Logger.Warn("From Db " + errorMessage);
            }
            return Outcome;
        }




    }
}
