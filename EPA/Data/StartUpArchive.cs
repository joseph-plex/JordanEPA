using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotLiquid;
using DotLiquid.NamingConventions;

namespace FoM
{


    public static class StartUp
    {
        public static void Initialize()
        {
            Ravka.Logger.Trace("Starting Mappers.Initialize");
            Mappers.Initialize();
            Ravka.Logger.Trace("Starting DotLiquid.Initialize");
            DotLiquid.Initialize();

        }

        public static void WarmUpDatabase(bool justOneCallPerDatabase)
        {
            bool hasValue = false;
            // bool justOneCallPerDatabase = true;


            {
                int siteId = 1;
                var x1 = FoM.Sites.Get.Network(siteId).PrivateSite;
                var x2 = FoM.Sites.Get.DomainInfo(siteId).SiteName;
                var x3 = FoM.Sites.Get.Company(siteId).CompanyName;
            }
            {
                hasValue = FoM.Db.Get().Members.Any();
                hasValue = FoM.Db.Get().MailPersonalParent.Any();

                hasValue = FoM.Db.GetApp().ActionLinks.Any();
                hasValue = FoM.Db.GetApp().MessageDefaults.Any();
            }
            
            
            using (var db = new FoM.Data.AppDb())
            {
                hasValue = db.Bugs.Any();
                if (!justOneCallPerDatabase)
                {
                    hasValue = db.Files.Any();
                    hasValue = db.MessageDefaults.Any();
                }
            }




            using (var db = new FoM.Data.EcommerceDb())
            {
                // db.Coupons.Any();
                hasValue = db.Transactions.Any();
                if (!justOneCallPerDatabase)
                {
                    hasValue = db.Products.Any();
                    hasValue = db.ProductSuggestions.Any();
                    hasValue = db.CustomerAddresses.Any();
                    hasValue = db.Orders.Any();
                }

            }

            using (var db = new FoM.Data.SiteDb())
            {

                hasValue = db.Sites.Any();
                if (!justOneCallPerDatabase)
                {
                    hasValue = db.Adverts.Any();
                    hasValue = db.Colors.Any();
                    hasValue = db.CustomTemplates.Any();
                    hasValue = db.CustomText.Any();
                    hasValue = db.Ecommerce.Any();
                    hasValue = db.FieldOptions.Any();
                    hasValue = db.GlobalNetworks.Any();
                    hasValue = db.LiquidMessageTypeUsages.Any();
                    hasValue = db.Menus.Any();
                    hasValue = db.AdditionalPageAccess.Any();

                    hasValue = db.MessageTemplates.Any();
                    hasValue = db.MetaTags.Any();
                    hasValue = db.Misc.Any();
                    hasValue = db.Networks.Any();
                    hasValue = db.PageElements.Any();
                    hasValue = db.SiteFunctionUsageEntries.Any();
                    hasValue = db.SmtpCredentials.Any();
                    hasValue = db.WebPlugIns.Any();
                }
            }
            using (var db = new FoM.Data.FoMDb())
            {

                hasValue = db.Members.Any();
                if (!justOneCallPerDatabase)
                {
                    hasValue = db.Journals.Any();
                    hasValue = db.GoalMilestones.Any();
                    hasValue = db.AddressBook.Any();
                    hasValue = db.AssessmentParticipants.Any();
                    hasValue = db.AssessmentQuestions.Any();
                    hasValue = db.Authors.Any();
                    hasValue = db.AutoSaves.Any();
                    hasValue = db.CmsModules.Any();
                    hasValue = db.CmsPages.Any();
                    hasValue = db.Comments.Any();
                    hasValue = db.Content.Any();
                    hasValue = db.ContentReviews.Any();
                    hasValue = db.Courses.Any();
                    hasValue = db.EmailAddresses.Any();
                    hasValue = db.Files.Any();
                    hasValue = db.Forums.Any();
                    hasValue = db.Goals.Any();
                    hasValue = db.GroupMembers.Any();
                    hasValue = db.Groups.Any();
                    hasValue = db.Homework.Any();
                    hasValue = db.MailPersonalParent.Any();
                    hasValue = db.MailPersonalRecipients.Any();
                    hasValue = db.MembersAddress.Any();
                    hasValue = db.MembersPersonal.Any();
                    hasValue = db.MembersSecondary.Any();
                    hasValue = db.MembersSmtp.Any();
                    hasValue = db.Polls.Any();
                    hasValue = db.CustomQuestions.Any();
                    hasValue = db.RejectList.Any();
                    hasValue = db.SelfGuided.Any();
                    hasValue = db.Seminars.Any();
                    hasValue = db.Topics.Any();
                }

            }


        }
        public static class DotLiquid
        {
            public static void Initialize()
            {

                //  Template.NamingConvention = new RubyNamingConvention();
                Template.NamingConvention = new CSharpNamingConvention();
                //  Template.RegisterTag<FoM.Dto.Drops.Tags.SignInUrl>("SignInUrl");
                Template.RegisterTag<PrettyMe.PrettyLiquid>("Pretty"); // {% SignInUrl Seminar %}
            }
        }

        public static class Mappers
        {
            public static void Initialize()
            {
                AutoMapper.Mapper.CreateMap<int?, int>().ConvertUsing<NullIntToIntConverter>();
                AutoMapper.Mapper.CreateMap<int, int?>().ConvertUsing<IntToNullIntConverter>();
                /*
                AutoMapper.Mapper.CreateMap<FoM.Models.Common.PhoneNumberCXT, FoM.Dto.Common.PhoneNumber>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.PhoneNumber, FoM.Models.Common.PhoneNumberCXT>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Common.PhoneNumberEntity, FoM.Dto.Common.PhoneNumber>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.PhoneNumber, FoM.Models.Common.PhoneNumberEntity>();
                */
                AutoMapper.Mapper.CreateMap<FoM.Models.Common.PhoneNumberCXT, FoM.Models.Members.PhoneNumber>();
                AutoMapper.Mapper.CreateMap<FoM.Models.Members.PhoneNumber, FoM.Models.Common.PhoneNumberCXT>();


                // authors

                AutoMapper.Mapper.CreateMap<FoM.Models.BaseEntites.EntityAuthor, FoM.Dto.Promote.Author>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Promote.Author, FoM.Models.BaseEntites.EntityAuthor>();

                AutoMapper.Mapper.CreateMap<FoM.Models.BaseEntites.EntityAuthor, FoM.Dto.Promote.Author>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Promote.Author, FoM.Models.BaseEntites.EntityAuthor>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Promote.NewsAuthor, FoM.Dto.Promote.Author>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Promote.Author, FoM.Models.Promote.NewsAuthor>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Admin.FeedbackAuthor, FoM.Dto.Promote.Author>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Promote.Author, FoM.Models.Admin.FeedbackAuthor>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Promote.TestimonialAuthor, FoM.Dto.Promote.Author>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Promote.Author, FoM.Models.Promote.TestimonialAuthor>();


                //   AutoMapper.Mapper.CreateMap<FoM.Models.Promote.NewsAuthor, FoM.Models.BaseEntites.EntityAuthor>();
                //   AutoMapper.Mapper.CreateMap<FoM.Models.BaseEntites.EntityAuthor, FoM.Models.Promote.NewsAuthor>();



                AutoMapper.Mapper.CreateMap<FoM.Models.Common.AddressCXT, FoM.Models.Members.Address>();
                AutoMapper.Mapper.CreateMap<FoM.Models.Members.Address, FoM.Models.Common.AddressCXT>();



                AutoMapper.Mapper.CreateMap<FoM.Models.Common.LocationCXT, FoM.Dto.Common.Location>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.Location, FoM.Models.Common.LocationCXT>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Common.AddressCXT, FoM.Dto.Common.Location>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.Location, FoM.Models.Common.AddressCXT>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Promote.SeminarAddressCXT, FoM.Dto.Common.Location>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.Location, FoM.Models.Promote.SeminarAddressCXT>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Common.LocationEntity, FoM.Dto.Common.Location>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Common.Location, FoM.Models.Common.LocationEntity>();

                // AutoMapper.Mapper.CreateMap<FoM.Models.Sites.CompanyCXT, FoM.Dto.Drops.Sites.Company>();
                //  AutoMapper.Mapper.CreateMap<FoM.Dto.Drops.Sites.Company, FoM.Models.Sites.CompanyCXT>();
                AutoMapper.Mapper.CreateMap<FoM.Models.Sites.Network, FoM.Dto.Drops.Sites.Network>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Drops.Sites.Network, FoM.Models.Sites.Network>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Promote.AssessmentReviewSettingsCXT, FoM.Dto.Sites.AssessmentSettingsWithText>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Sites.AssessmentSettingsWithText, FoM.Models.Promote.AssessmentReviewSettingsCXT>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Promote.AssessmentReviewSettingsCXT, FoM.Dto.Sites.AssessmentSettingsWithText>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Sites.AssessmentSettingsWithText, FoM.Models.Promote.AssessmentReviewSettingsCXT>();

                // AutoMapper.Mapper.CreateMap<FoM.Models.Sites.AssessmentSettings, FoM.Dto.Sites.AssessmentSettingsWithText>();
                // AutoMapper.Mapper.CreateMap<FoM.Dto.Sites.AssessmentSettingsWithText, FoM.Models.Sites.AssessmentSettings>();

                AutoMapper.Mapper.CreateMap<FoM.Models.Sites.ContentReviewSettings, FoM.Dto.Sites.ContentReviewWithText>();
                AutoMapper.Mapper.CreateMap<FoM.Dto.Sites.ContentReviewWithText, FoM.Models.Sites.ContentReviewSettings>();


                AutoMapper.Mapper.CreateMap<FoM.Models.Content.GoalMilestone, FoM.Models.Content.TemporaryGoalMilestone>();
                AutoMapper.Mapper.CreateMap<FoM.Models.Content.TemporaryGoalMilestone, FoM.Models.Content.GoalMilestone>();



                //  AutoMapper.Mapper.CreateMap<FoM.Dto.Drops.Sites.Network, FoM.Models.Sites.Network>().ConvertUsing;

            }

            public class NullIntToIntConverter : AutoMapper.TypeConverter<int?, int>
            {
                protected override int ConvertCore(int? source)
                {
                    return Ravka.Numbers.ToInt(source);
                }
            }

            public class IntToNullIntConverter : AutoMapper.TypeConverter<int, int?>
            {

                protected override int? ConvertCore(int source)
                {
                    if (source == 0)
                        return (int?)null;
                    else
                        return source;
                }
            }
        }
    }
}