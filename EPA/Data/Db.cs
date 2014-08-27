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
// using System.Data.Entity.Core;


namespace EPA.Data
{



    public class DbContextBase : DbContext
    {

        public DbContextBase() : base()
        {
        }
        public DbContextBase(DbConnection connection)
            : base(connection, true) 
        {
    }
        /// <summary>
        /// Does not Save
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void SetEntryToModified<T>(T model) where T : CodeFirst.BaseEntities.Entity
        {
            model.PreSaveFixes();
            Entry(model).State = System.Data.EntityState.Modified; // System.Data.Entity.EntityState.Modified;
        }
        /// <summary>
        /// Saves context. Overides base insert with additional control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="disableValidations"></param>
        /// <param name="logErrors"></param>
        /// <returns></returns>
        public Ravka.Outcome InsertOrUpdate<T>(T model, bool disableValidations = false, bool logErrors = false) where T : CodeFirst.BaseEntities.Entity
        {
            if (model == null)
                return new Ravka.Outcome(false, "The model is null (server error)");

            // if (disableValidations)
            //  this.Configuration.ValidateOnSaveEnabled = false;

            model.PreSaveFixes();

            if (model.ID == default(int))
            {

                // New entity
                Set<T>().Add(model);
                //  db.Dinners.AddEntity(dinner);
            }
            else
            {
                // Existing entity
                if (Entry<T>(model).State != System.Data.EntityState.Deleted) //  EntityState.Deleted)
                    Entry<T>(model).State = System.Data.EntityState.Modified;
                //db.Entry(dinner).State = EntityState.Modified;
            }
            var outcome = Save(logErrors, disableValidations);
            if (outcome.HasError == false)
                outcome.EntityId = model.ID;


            //  if (disableValidations)
            //     this.Configuration.ValidateOnSaveEnabled = true;

            return outcome;
        }

        /// <summary>
        /// Overides Save, converts exceptions to Outcome
        /// </summary>
        /// <param name="importantLogging"></param>
        /// <param name="disableValidations"></param>
        /// <returns></returns>
        public Ravka.Outcome Save(bool importantLogging = false, bool disableValidations = false)
        {

            var Outcome = new Ravka.Outcome();
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
    public class EPADb : DbContextBase
    {
        public EPADb()
            : base()
        {
           /*  DbConnection con = new Devart.Data.Oracle.OracleConnection(
          "Data Source=ora1020;User Id=scott;Password=tiger;");
      con.StateChange += new StateChangeEventHandler(Connection_StateChange);
      ---------------------------------------------------------------*/
        }
        public EPADb(DbConnection connection) : base(connection) 
        {
    }
        static EPADb()
        {

            //--------------------------------------------------------------
            // You can choose one of database initialization
            // strategies or turn off initialization:
            //--------------------------------------------------------------
            //System.Data.Entity.Database.SetInitializer<EPADb>(new Initialization.EPADbDropCreateDatabaseAlways());
            System.Data.Entity.Database.SetInitializer<EPADb>(new Initialization.EPADbCreateDatabaseIfNotExists());
            //System.Data.Entity.Database.SetInitializer<EPADb>(new Initialization.EPADbDropCreateDatabaseIfModelChanges());
            //System.Data.Entity.Database.SetInitializer<EPADb>(null);
        }


        public DbSet<CodeFirst.Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            /*-------------------------------------------------------------
    ColumnTypeCasingConvention should be removed for dotConnect for Oracle.
    This option is obligatory only for SqlClient.
    Turning off ColumnTypeCasingConvention isn't necessary
    for  dotConnect for MySQL, PostgreSQL, and SQLite.
    -------------------------------------------------------------*/
            /*

            modelBuilder.Conventions
              .Remove<System.Data.Entity.ModelConfiguration.Conventions
                .ColumnTypeCasingConvention>();
            */

            /*-------------------------------------------------------------
            If you don't want to create and use EdmMetadata table
            for monitoring the correspondence
            between the current model and table structure
            created in a database, then turn off IncludeMetadataConvention:
            -------------------------------------------------------------*/

            modelBuilder.Conventions
              .Remove<System.Data.Entity.Infrastructure.IncludeMetadataConvention>();

            /*-------------------------------------------------------------
            In the sample above we have defined autoincrement columns in the primary key
            and non-nullable columns using DataAnnotation attributes.
            Similarly, the same can be done with Fluent mapping
            -------------------------------------------------------------*/

            //modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            //modelBuilder.Entity<Product>().Property(p => p.ProductID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Product>().Property(p => p.ProductName)
            //    .IsRequired()
            //    .HasMaxLength(50);
            //modelBuilder.Entity<ProductCategory>().HasKey(p => p.CategoryID);
            //modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryName)
            //    .IsRequired()
            //    .HasMaxLength(20);
            //modelBuilder.Entity<Product>().ToTable("Product", "TEST");
            //modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory", "TEST");

            //-------------------------------------------------------------//


            /*

                //  modelBuilder.Entity<Models.Sites.AssessmentSettings>().Property(a => a.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                modelBuilder.Entity<Models.Sites.Colors>().Property(a => a.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
      
                // modelBuilder.Entity<Models.Sites.Site>().HasOptional(u => u.AssessmentSettings).WithRequired().WillCascadeOnDelete();
                modelBuilder.Entity<Models.Sites.Site>().HasOptional(u => u.Colors).WithRequired().WillCascadeOnDelete();

                // GOOD!
                modelBuilder.Entity<Models.Members.ProfileQuestionAnswer>().HasMany(i => i.AnswersSelected)
                         .WithMany() // (c => c.Associations.HomeworkAssociations)
                         .Map(mc =>
                         {
                             mc.MapLeftKey("ProfileQuestionId");
                             mc.MapRightKey("AnswerChoiceId");
                             mc.ToTable("ProfileQuestionAnswerSelected");
                         });

                modelBuilder.Entity<Models.Admin.FeedbackQuestionAnswer>().HasMany(i => i.AnswersSelected)
                 .WithMany() // (c => c.Associations.HomeworkAssociations)
                 .Map(mc =>
                 {
                     mc.MapLeftKey("FeedbackQuestionId");
                     mc.MapRightKey("AnswerChoiceId");
                     mc.ToTable("FeedbackQuestionAnswerSelected");
                 });


                modelBuilder.Entity<Models.Members.ProfileQuestionAnswer>()
        .HasRequired(a => a.Question).WithMany() // b => b.UserAnswers)
           .HasForeignKey(c => c.QuestionId).WillCascadeOnDelete(false);
                var properties = new[]
            {
                modelBuilder.Entity<Models.Groups.GroupCoachingCall>().Property(a => a.Session),
                modelBuilder.Entity<Models.Groups.GroupEntity>().Property(a => a.CurrentSession),
          
            };
                properties.ToList().ForEach(property =>
                {
                    property.HasPrecision(10, 2);
                    // property.Precision = 10;
                    // property.Scale = 2;
                });

                modelBuilder.Entity<Models.Admin.FeedbackQuestionAnswer>()
    .HasRequired(a => a.Question).WithMany() // b => b.UserAnswers)
    .HasForeignKey(c => c.QuestionId).WillCascadeOnDelete(false);

                modelBuilder.Entity<Models.Ecommerce.ProductSuggestion>().HasRequired(a => a.SuggestProduct).WithMany()
          .HasForeignKey(b => b.SuggestProductId).WillCascadeOnDelete(false);



                modelBuilder.Entity<Models.ImportExport.EmailListAddress>().HasRequired(a => a.EmailAddress).WithMany()
          .HasForeignKey(b => b.EmailAddressId).WillCascadeOnDelete(false);
                */
            base.OnModelCreating(modelBuilder);
            return;

        }
    }

    public class Initialization
    {

        public class EPADbDropCreateDatabaseAlways : DropCreateDatabaseAlways<EPADb>
        {

            protected override void Seed(EPADb context)
            {

                EPADbSeeder.Seed(context);
            }
        }

        public class EPADbDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<EPADb>
        {

            protected override void Seed(EPADb context)
            {

                EPADbSeeder.Seed(context);
            }
        }

        public class EPADbCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<EPADb>
        {

            protected override void Seed(EPADb context)
            {

                EPADbSeeder.Seed(context);
            }
        }

        public static class EPADbSeeder
        {

            public static void Seed(EPADb context)
            {
                context.Companies.Add(new CodeFirst.Company
                {
                    CODE = "hello",
                    DESCRIPTION = "DESCRIPTION",
                    KEY = "KEY",
                    EMAIL = "TEST@TEST.COM",
                    ROW_VERSION = 1,

                });

              

            }
        }

    }
}