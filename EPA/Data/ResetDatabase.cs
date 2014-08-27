using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;

namespace FoM.Data
{
   public class Startup {

       /*   public static void KillAndCreateDatabase()
      {
          try
          {
              FoM.Config.IsInitializingDatabase = true;
              using (var db = new FoM.Data.FoMDb()) // ProductContext())
              {
                    
                  db.Database.Delete();
                  db.Database.CreateIfNotExists();
                //  Console.ReadKey();
              }

              using (var db = new FoM.Data.SiteDb()) // ProductContext())
              {
                  db.Database.Delete();
                  db.Database.CreateIfNotExists();
                  //  Console.ReadKey();
              }
             using (var db = new FoM.Data.AppDb()) // ProductContext())
              {
                 // db.Database.Delete();
                  db.Database.CreateIfNotExists();
                  //  Console.ReadKey();
              }
             using (var db = new FoM.Data.EcommerceDb()) // ProductContext())
             {
                 // db.Database.Delete();
                 db.Database.CreateIfNotExists();
                 //  Console.ReadKey();
             }
              Console.WriteLine("Run Categories Sql Now then press enter..");
              FoM.Config.IsInitializingDatabase = false;

          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.ToString());
              throw;
          }

      }
   
      public static void TestPrivacy()
      {

          int id = 23611;

          var old = new Logic.objects.Journal();
          old.Get_Journal(id);
          if (!Common.Members.Exist(old.Member_ID))
          {
              Ravka.Logger.Warn("Could not find journal owner for " + id);
              Logic.common.Journals.dt.UpdateV4Status(id, false, true);
              return;
          }

          using (var db = new FoM.Data.FoMDb())
          {

              var p = new FoM.Models.Content.PrivacyEntity()
                  {
                      AllowFacebookAccess = old.Allow_Facebook_Access,
                      AllowPublicComments = old.Allow_Public_Comments,
                      Allowed = Retro.Enums.Privacy(old.Privacy_Viewing)


                  };

              var j = new FoM.Models.Content.Journal()
              {

                  Availability = Retro.Enums.Availability(old.Availability_Status),
                  ContentType = FoM.Globals.ContentType.Journal,
                  CreatorId = old.Member_ID,

                  // GroupId = (old.Course_ID != 0 ? Groups.RealGroupId(old.Course_ID, FoM.Globals.GroupType.Course) :
                  // (old.Self_Guided_Course_ID != 0 ? Groups.RealGroupId(old.Self_Guided_Course_ID, FoM.Globals.GroupType.SelfGuided) : null)),
                  HomeworkId = (old.Homework_ID != 0 ? Retro.Materials.RealHomeworkId(old.Homework_ID) : null),
                  DateMade = Ravka.Dates.FixForSql(old.DateMade),
                  Description = "hello, this is a test " + Retro.BBCode.UpgradeText(old.Description_Unformatted, true),
                  Session = old.Week,
                  // GroupType = old.Course_ID != 0 ? FoM.Globals.GroupType.Course : old.Self_Guided_Course_ID != 0 ? FoM.Globals.GroupType.SelfGuided : FoM.Globals.GroupType.Undefined,
                  // ID ASSIGNED Id = old.Journal_ID, // ID ASSIGNED
                  Version3Id = old.Journal_ID,
                  SubscriptionsCount = 0,

                  Title = old.Title,
                  PostProcessed = true,




              };
              var groupType = FoM.Globals.GroupType.Undefined;
              j.GroupId = Retro.Groups.RealGroupId(old.Course_ID, old.Self_Guided_Course_ID, out groupType);
              j.GroupType = groupType;

              old.Allow_Member_1 = 1;
              old.Allow_Member_2 = 52;


              if (old.Allow_Member_1 != 0 && FoM.Common.Members.Exist(old.Allow_Member_1))
              {
                  p.AdditionalMembers.Add(new Models.Content.PrivacyAdditionalMember { MemberId = old.Allow_Member_1 });
              }

           //   if (old.Allow_Member_2 != 0 && FoM.Common.Members.Exist(old.Allow_Member_2))
           //       p.AdditionalMembers.Add(FoM.Common.Members.Get(old.Allow_Member_2, false));

              j.Privacy = p;

              try
              {
                  db.Journals.AddEntity(j);
                  db.SaveChanges();
              }
              catch (DbEntityValidationException dbEx)
              {
                  foreach (var validationErrors in dbEx.EntityValidationErrors)
                  {
                      foreach (var validationError in validationErrors.ValidationErrors)
                      {
                          Ravka.Logger.Warn(string.Format("Journal " + j.Version3Id + " - Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                      }
                  }
                  throw;
              }
              catch (Exception)
              {
                    
                  throw;
              }
            
          }
      }


      public static void AddSiteData()
      {
          FoM.Resources.Startup.CreateDatabaseConstantDefaults();
          FoM.Retro.Sites.CreateSites(); // .CreateDatabaseMessageDefaults();
          FoM.Retro.Sites.CreateMessageDefaults();
          FoM.Retro.Sites.CreateAdverts();
      }


      public static void AddDataSet1()
      {
          FoM.Retro.Materials.CreatePolls();
       //   FoM.Retro.Materials.CreateFiles(); // LAUNCH TEMP TODO
            
          int resultCount = 1;
          do
          {
              FoM.Retro.Members.CreateMembers(500, out resultCount);
          } while (resultCount > 0);
            
          // GROUPS

          resultCount = 1;
          do
          {
              FoM.Retro.Groups.CreateCourses(100, out resultCount);
          } while (resultCount > 0);
            
          resultCount = 1;
            
          do
          {
              FoM.Retro.Groups.CreateSelfGuided(1000, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;

          FoM.Retro.Groups.CreateTraining(1000, out resultCount);

          // PAGES
          FoM.Retro.Pages.CreateCms();
          FoM.Retro.Sites.FixSiteAfterPages();
                    
          // CONTENT

          resultCount = 1;
          do
          {
              FoM.Retro.Content.CreateJournals(100, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;
          do
          {
              FoM.Retro.Content.CreateBoards(100, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;
          do
          {
              FoM.Retro.Content.CreateGoals(100, out resultCount);
          } while (resultCount > 0);

          // INTERACT

          FoM.Retro.Interactions.CreateAddressBooks();
          FoM.Retro.Interactions.CreateFavorites();
          FoM.Retro.Interactions.CreateRejectList();

          // MAIL

          resultCount = 1;
          do
          {
              FoM.Retro.Interactions.CreatePersonalMail(500, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;

          do
          {
              FoM.Retro.Interactions.CreateGroupMail(500, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;

          // ADMIN
          do
          {
              FoM.Retro.Admin.CreateCoachRelationships(500, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;

          // emails
          do
          {
              FoM.Retro.Emails.CreateEmails(500, out resultCount);
          } while (resultCount > 0);

          resultCount = 1;

          do
          {
              FoM.Retro.Emails.CreateEmailLists(500);
          } while (resultCount > 0);

          resultCount = 1;

          // PROMO

          FoM.Retro.Promote.CreateAllNews();
          resultCount = 1;

          do
          {
              FoM.Retro.Promote.CreateAssessments(500, out resultCount);
          } while (resultCount > 0);
            
          FoM.Retro.Promote.CreateSeminars();
          FoM.Retro.Promote.CreateTestimonials();

            

      }

      static void DataTest()
      {


          try
          {
              using (var db = new FoM.Data.FoMDb()) // ProductContext())
              {
                  db.Database.Delete();
                  db.Database.CreateIfNotExists();
                  db.Members.Add(new FoM.Models.Member { Username = "test" });
                  db.Members.Add(new FoM.Models.Member { Username = "test2" });
                  db.Members.Add(new FoM.Models.Member { Username = "test3" });


                  // db.Sites.Add(new FoM.Models.Sites.Site { DomainInfo = new FoM.Models.Sites.DomainCXT { SiteName = "TestSite", WebSiteName = "My Test Site" } });
                  db.SaveChanges();
                  var m = db.Members.Single(a => a.Username == "test");
                  var s = new FoM.Models.Sites.Site();
                  s.DomainInfo.SiteName = "TestSiteName";
                  s.DomainInfo.WebSiteName = "Test WebSite";
                  s.Company.AdminId = m.Id;
                 // db.Sites.AddEntity(s);
                  db.SaveChanges();

                  // var newsite = db.Sites.First();
                  m.SiteId = s.Id;

                  var f = new FoM.Models.Material.File(); // { CreatorId = m.Id, FileExtension = "jpg", FileName = "testfile", Title = "Test Title" };
                  db.Files.AddEntity(f);
                  db.SaveChanges();
                  // db.Files.Add(new FoM.Models.Material.File { CreatorId = m.Id, FileExtension = "jpg", FileName = "testfile", Title = "Test Title" });

                  var j = new FoM.Models.Content.Journal { CreatorId = m.Id, Description = "Description", Title = "Title" };

                  //   if (j.Privacy.AdditionalMembers == null)
                  //    j.Privacy.AdditionalMembers = new List<FoM.Models.Content.PrivacyAdditionalMembers>();

                  // j.Privacy.AdditionalMembers.Add(new FoM.Models.Content.PrivacyAdditionalMembers { MemberId = m.Id });
                  j.Files = new List<FoM.Models.Material.File>();
                  j.Files.Add(f);

                  // j.IncludedFiles.Add(new FoM.Models.BaseEntites.ContentFiles { File = f });
                  // j.IncludedFiles = new List<FoM.Models.BaseEntites.ContentFiles> { new FoM.Models.BaseEntites.ContentFiles { File = f } };

                  db.Journals.AddEntity(j);

                  //  });

                  db.SaveChanges();


                  var assessment = new FoM.Models.Promote.Assessment
                  {
                      CreatorId = m.Id,
                      SiteId = m.SiteId,
                      HeadReviewerId = m.Id,
                      IntroMessage = "IntoTexty",
                      NewEntryNotification = new FoM.Models.Common.NotificationCXT(true),
                      Title = "My Original Assessment",
                      Questions = new List<FoM.Models.Promote.AssessmentQuestion>()
                  };
                  assessment.Questions.Add(new FoM.Models.Promote.AssessmentQuestion
                  {
                      PageNumber = 1,
                      Availability = FoM.Globals.Availability.Unavailable,
                      QuestionType = FoM.Globals.QuestionType.AgreeDisagree,
                      QuestionText = "Question 1",
                      Manditory = true
                  });

                  var q2 = (new FoM.Models.Promote.AssessmentQuestion
                  {
                      PageNumber = 2,
                      Availability = FoM.Globals.Availability.Available,
                      QuestionType = FoM.Globals.QuestionType.Important,
                      AnswerChoices = new List<FoM.Models.Promote.AssessmentAnswerChoice>(),
                      QuestionText = "Question 2",
                      Manditory = false
                  });

                  q2.AnswerChoices.Add(new FoM.Models.Promote.AssessmentAnswerChoice { AnswerText = "Choice 1", DisplayOrder = 1 });
                  q2.AnswerChoices.Add(new FoM.Models.Promote.AssessmentAnswerChoice { AnswerText = "Choice 2", DisplayOrder = 2 });
                  assessment.Questions.Add(q2);

                  db.Assessments.AddEntity(assessment);
                  db.SaveChanges();
              }
              FoM.Data.Cache.Reset();
            //  var newAssesId = FoM.Data.Cache.Db.Assessments.First().Id;
            //  FoM.Common.Promote.Assessment.CloneAssessment(newAssesId, new Ravka.Outcome());

          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.ToString());
              throw;
          }



      }
     */
    }
}
