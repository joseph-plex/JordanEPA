using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Plexxis.Helpers.Extensions;
using System.Data.Common;
// using Effort.DataLoaders;
using System.Data.EntityClient;

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
}

namespace EPA.Models
{



    public partial class DbFirstEntities : DbContext
    {

        public DbFirstEntities(DbConnection connection)
            : base(connection, true)
        {
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
                        EPA.Logger.Warn(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + " " +
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
                EPA.Logger.Warn("From Db " + errorMessage);
            }
            return Outcome;
        }




    }
}
