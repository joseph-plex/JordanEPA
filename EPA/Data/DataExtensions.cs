using System.Data.Entity;
using System;
using EPA.Models;
using System.Linq;
using System.Data.Common;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using Ravka.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
/* using System.Data.Linq;
using System.Data.Linq.Mapping; */
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// using System.Data.Objects;
using System.Reflection;
using System.Data.Entity.ModelConfiguration.Configuration;
namespace EPA.Extensions
{
    public static class  Data
    {
        public static void SetEntryToModified<T>(this DbContext dbContext, T model) where T : CodeFirst.BaseEntities.Entity
        {
            model.PreSaveFixes();
            dbContext.Entry(model).State = System.Data.EntityState.Modified; // System.Data.Entity.EntityState.Modified;
        }
        public static Ravka.Outcome InsertOrUpdate<T>(this DbContext dbContext, T model, bool disableValidations = false, bool logErrors = false) where T : CodeFirst.BaseEntities.Entity
        {
            if (model == null)
                return new Ravka.Outcome(false, "The model is null (server error)");

            // if (disableValidations)
            //  this.Configuration.ValidateOnSaveEnabled = false;

            model.PreSaveFixes();

            if (model.ID == default(int))
            {

                // New entity
                dbContext.Set<T>().Add(model);
                //  db.Dinners.AddEntity(dinner);
            }
            else
            {
                // Existing entity
                if (dbContext.Entry<T>(model).State != System.Data.EntityState.Deleted) //  EntityState.Deleted)
                    dbContext.Entry<T>(model).State = System.Data.EntityState.Modified;
                //db.Entry(dinner).State = EntityState.Modified;
            }
            var outcome = Save(dbContext, logErrors, disableValidations);
            if (outcome.HasError == false)
                outcome.EntityId = model.ID;


            //  if (disableValidations)
            //     this.Configuration.ValidateOnSaveEnabled = true;

            return outcome;
        }

        public static Ravka.Outcome Save(this DbContext dbContext, bool importantLogging = false, bool disableValidations = false)
        {

            var Outcome = new Ravka.Outcome();
            string errorMessage = string.Empty;

            if (disableValidations)
                dbContext.Configuration.ValidateOnSaveEnabled = false;

            try
            {
                dbContext.SaveChanges();
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
            catch (System.Data.UpdateException ex)
            {

                if (ex.InnerException != null)
                    errorMessage = ex.InnerException.ToString();
                else
                    errorMessage = ex.ToString();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.InnerException != null)
                    errorMessage = ex.InnerException.ToString();
                else
                    errorMessage = ex.ToString();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    errorMessage = ex.InnerException.ToString();
                else
                    errorMessage = ex.ToString();


            }

            if (disableValidations)
                dbContext.Configuration.ValidateOnSaveEnabled = true;

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
