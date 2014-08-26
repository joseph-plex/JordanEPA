using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ravka.Extensions;
using EPA.Attributes;
using EPA.Attributes.Validators;
using EPA.Attributes.Relationships;
using EPA.Attributes.UHints;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;
using EPA.Models.Interfaces;


namespace EPA.Models.BaseEntities
{

 
    public abstract class Entity : IEntity, System.ComponentModel.DataAnnotations.IValidatableObject 
    {
  
        [Ignore]
        public bool DeepValidate = true;

        [ScaffoldColumn(false)]
        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [DateProperty(false)]
        public DateTime DATE_MADE { get; set; }
 
 
        public Entity()
        {

            DATE_MADE = DateTime.Now;
            //  DateUpdated = DateMade;
        }
        public virtual void AssignDefaults()
        {
            if (DATE_MADE == null || DATE_MADE <= Ravka.Dates.SqlMinDateForChecking)
            {
                DATE_MADE = DateTime.Now;
                
            }

        }
        
        public virtual void PreSaveFixes()
        {
            DATE_MADE = Ravka.Dates.FixForSql(DATE_MADE);
           

        }
        public virtual IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {

            PreSaveFixes();
            return new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            
        }
    }

 


    public abstract class EntityCompany : Entity, ICompany
    {
        public EntityCompany()
            : base()
        {

           // Company = string.Empty;
        }

 
       // [StringRequired(256), TextProperty(true)] // StringProperty(256, true), 
       // public string Company { get; set; }

        public int COMPANY_ID { get; set; }
    }







}
