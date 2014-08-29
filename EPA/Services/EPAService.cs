using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plexxis.Helpers.Extensions;

namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EPAService" in both code and config file together.
    public class EPAService : IEPAService
    {

        public EPAService()
        {
            EPA.StartUp.Initialize();
        }
        public void DoWork()
        {
        }

        #region Company
        public Dto.Models.COMPANY CompanyFetch(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }
        public Dto.Models.COMPANY CompanyFetch(int companyId)
        {
            if (companyId == 0)
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).FirstOrDefault();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }

        private int CompanyIdFetch(string key)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).Select(a => a.COMPANY_ID).FirstOrDefault();
            }
        }
        private string CompanyKeyFetch(int companyId)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).Select(a => a.KEY).FirstOrDefault();
            }
        }


        public Dto.Models.COMPANY CompanyCreate(string description)
        {
            return CompanyCreate(new Dto.Models.COMPANY { DESCRIPTION = description });
        }

        public Dto.Models.COMPANY CompanyCreate(Dto.Models.COMPANY company)
        {
            if (string.IsNullOrEmpty(company.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");

            if (string.IsNullOrEmpty(company.KEY))
            {
                company.KEY = IdGenerator.CreateUniqueKeyForCompany();
            }

            company.ROW_VERSION = 1;

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.COMPANY();
                model = Mapper.Map<EPA.Dto.Models.COMPANY, EPA.Models.COMPANY>(company, model);
                db.COMPANIES.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }

        public Dto.Models.COMPANY CompanyModify(Dto.Models.COMPANY company)
        {
            if (company == null)
                throw new Exception("Your model can't be null");
            else if (company.COMPANY_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.Where(a => a.COMPANY_ID == company.COMPANY_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find company to modify based on COMPANY_ID " + company.COMPANY_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY, EPA.Models.COMPANY>(company, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY>(model);
            }
        }


        public void CompanyDelete(int companyId)
        {
            CompanyDelete(CompanyFetch(companyId));
        }
        public void CompanyDelete(Dto.Models.COMPANY company)
        {
            if (company == null)
                throw new Exception("Your model can't be null");
            else if (company.COMPANY_ID == 0 || company.KEY.IsNullOrEmpty())
                throw new Exception("Need a Primary KEY to reference");

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.Where(a => a.COMPANY_ID == company.COMPANY_ID && a.KEY == company.KEY).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid Company");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }

        #endregion
        #region Supplier
        public Dto.Models.SUPPLIER SupplierCreate(string description, string email)
        {

            return SupplierCreate(new Dto.Models.SUPPLIER {  DESCRIPTION = description, EMAIL = email });
        }
        public Dto.Models.SUPPLIER SupplierCreate(Dto.Models.SUPPLIER supplier)
        {

            if (string.IsNullOrEmpty(supplier.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");
            else if (string.IsNullOrEmpty(supplier.EMAIL))
                throw new Exception("EMAIL cannot be empty or null");

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.SUPPLIER();
                model = Mapper.Map<EPA.Dto.Models.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                db.SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.SUPPLIER>(model);
            }
        }
        public Dto.Models.SUPPLIER SupplierModify(Dto.Models.SUPPLIER supplier)
        {
            if (supplier == null)
                throw new Exception("Your model can't be null");
            else if (supplier.SUPPLIER_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");


            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.Where(a => a.SUPPLIER_ID == supplier.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find supplier to modify based on SUPPLIER_ID " + supplier.SUPPLIER_ID);

                model = Mapper.Map<EPA.Dto.Models.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.SUPPLIER>(model);
            }
        }

        public void SupplierDelete(int supplierId)
        {
            SupplierDelete(SupplierFetch(supplierId));
        }
        public void SupplierDelete(Dto.Models.SUPPLIER supplier)
        {
            if (supplier == null)
                throw new Exception("Your model can't be null");
            else if (supplier.SUPPLIER_ID == 0)
                throw new Exception("Need a Primary KEY to reference");

            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.Where(a => a.SUPPLIER_ID == supplier.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid supplier");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }

        public Dto.Models.SUPPLIER SupplierFetch(int? supplierId = null)
        {
            // PROBLEM
            // WHY WOULD IT BE NULL ? SHOULD IT RETURN EVERYTHING?

            int useSupplierId = supplierId.ToInt();
            if (useSupplierId == 0)
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.AsNoTracking().Where(a => a.SUPPLIER_ID == useSupplierId).FirstOrDefault();
                return Mapper.Map<EPA.Dto.Models.SUPPLIER>(model);
            }


        }
        #endregion
        #region Company Supplier
        public Dto.Models.COMPANY_SUPPLIERS CompanySupplierCreate(string companyKey, int supplierId, string description)
        {
            if (String.IsNullOrWhiteSpace(description))
                throw new Exception("Description must not be null or empty");


            using (var db = new EPA.Data.Db())
            {
                var company = CompanyFetch(companyKey);
                if (company == null || company.COMPANY_ID == 0)
                    throw new Exception("Company does not exist");

                var supplier = SupplierFetch(supplierId);
                if (supplier == null || supplier.SUPPLIER_ID == 0)
                    throw new Exception("Supplier does not exist");

                var model = new EPA.Models.COMPANY_SUPPLIERS()
                {
                    COMPANY_ID = company.COMPANY_ID,
                    DESCRIPTION = description,
                    SUPPLIER_ID = supplier.SUPPLIER_ID,
                };

                db.COMPANY_SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(model);
            }
        }

        public Dto.Models.COMPANY_SUPPLIERS CompanySupplierModify(Dto.Models.COMPANY_SUPPLIERS companySupplier)
        {
            if (companySupplier == null)
                throw new Exception("Your model can't be null");
            else if (companySupplier.COMPANY_SUPPLIERS_ID == 0)
                throw new Exception("Need the Primary Key");


            var companyId = companySupplier.COMPANY_ID; // CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("Company does not exist");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_SUPPLIERS.Where(a => a.COMPANY_SUPPLIERS_ID == companySupplier.COMPANY_SUPPLIERS_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find company to modify based on primary key " + companySupplier.COMPANY_SUPPLIERS_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS, EPA.Models.COMPANY_SUPPLIERS>(companySupplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(model);
            }
        }
        public void CompanySupplierDelete(string companyKey, int supplierId)
        {
            if (supplierId == 0)
                throw new Exception("need the supplier Id");

            var companySuppliers = CompanySuppliersFetch(companyKey, supplierId);

            if (companySuppliers == null || companySuppliers.Any())
                throw new Exception("can't find the company supplier");

            foreach (var companySupplier in companySuppliers)
            {
                using (var db = new EPA.Data.Db())
                {
                    var model = db.COMPANY_SUPPLIERS.Where(a => a.COMPANY_SUPPLIERS_ID == companySupplier.COMPANY_SUPPLIERS_ID).FirstOrDefault();
                    db.SetToDeleted(model);
                    db.SaveChanges();
                }
            }
        }

        public Dto.Models.COMPANY_SUPPLIERS[] CompanySuppliersFetch(string companyKey, int? supplierId = null)
        {
            if (string.IsNullOrEmpty(companyKey))
                return null;

            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_SUPPLIERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId);

                var supplierIdInt = supplierId.ToInt();
                if (supplierIdInt != 0)
                    q = q.Where(a => a.SUPPLIER_ID == supplierIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.COMPANY_SUPPLIERS>(a)).ToArray();

            }


        }
        #endregion
        #region Company User
        public Dto.Models.COMPANY_USERS CompanyUserCreate(string companyKey, string descripton, string email)
        {
            return CompanyUserCreate(new Dto.Models.COMPANY_USERS { EMAIL = email, DESCRIPTION = descripton, COMPANY_ID = CompanyIdFetch(companyKey) });
        }
        public Dto.Models.COMPANY_USERS CompanyUserCreate(Dto.Models.COMPANY_USERS companyUser)
        {

            if (String.IsNullOrWhiteSpace(companyUser.DESCRIPTION))
                throw new Exception("Description must have valid value");
            if (String.IsNullOrWhiteSpace(companyUser.EMAIL))
                throw new Exception("Email must have a valid value");

            var companyId = companyUser.COMPANY_ID; // CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.COMPANY_USERS();
                model = Mapper.Map<EPA.Dto.Models.COMPANY_USERS, EPA.Models.COMPANY_USERS>(companyUser, model);
                db.COMPANY_USERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(model);
            }


        }

        public Dto.Models.COMPANY_USERS CompanyUserModify(string companyKey, Dto.Models.COMPANY_USERS companyUser)
        {

            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");

            if (companyUser == null)
                throw new Exception("Your model can't be null");
            else if (companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Need a Primary Key ID to reference");
            else if (String.IsNullOrWhiteSpace(companyUser.EMAIL))
                throw new Exception("Email must have a valid value");
            else if (String.IsNullOrWhiteSpace(companyUser.DESCRIPTION))
                throw new Exception("Description must have valid value");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on ID " + companyUser.COMPANY_USER_ID);

                model = Mapper.Map<EPA.Dto.Models.COMPANY_USERS, EPA.Models.COMPANY_USERS>(companyUser, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(model);
            }
        }

        public void CompanyUserDelete(string companyKey, int companyUserId)
        {

            var companyUsers = CompanyUserFetch(companyKey, companyUserId);

            if (companyUsers == null || companyUsers.Any() == false)
                throw new Exception("Can't find the user");

            var companyUser = companyUsers.FirstOrDefault();

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid Company User");
                db.SetToDeleted(model);
                db.SaveChanges();
            }
        }
        public Dto.Models.COMPANY_USERS[] CompanyUserFetch(string companyKey, int? companyUserId = null)
        {
            
            if (string.IsNullOrEmpty(companyKey))
                return null;

            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId);

                var companyUserIdInt = companyUserId.ToInt();
                if (companyUserIdInt != 0)
                    q = q.Where(a => a.COMPANY_USER_ID == companyUserIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.COMPANY_USERS>(a)).ToArray();

            }
        }
        public bool CompanyUserExists(string companyKey, string email)
        {
            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                return false; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USERS.AsNoTracking()
                    .Where(a => a.COMPANY_ID == companyId)
                    .Where(a => String.Equals(a.EMAIL, email, StringComparison.CurrentCultureIgnoreCase));

                return q.Any();
            }

        }
        #endregion
        #region Company User Suppliers
        public Dto.Models.COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, int supplierId, int companyUserId)
        {

            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                throw new Exception("No company exists with this company key");
            else if (companyUserId == 0)
                throw new Exception("Need the company User Id");



            var companyUser = CompanyUserFetch(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Invalid Company User Information");

            var supplier = SupplierFetch(supplierId);
            if (supplier == null || supplier.SUPPLIER_ID == 0)
                throw new Exception("Invalid Supplier Information");


            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.COMPANY_USER_SUPPLIERS() { COMPANY_USER_ID = companyUserId, SUPPLIER_ID = supplierId };
                db.COMPANY_USER_SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.COMPANY_USER_SUPPLIERS>(model);
            }

        }

        public void CompanyUsersSuppliersDelete(string companyKey, int supplierId, int companyUserId)
        {

            var companyUser = CompanyUserFetch(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("Invalid Company User Information");

            if (companyUser == null)
                throw new Exception("Unauthorized Request");


            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANY_USER_SUPPLIERS.Where(a => a.COMPANY_USER_ID == companyUser.COMPANY_USER_ID && supplierId == a.SUPPLIER_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Invalid COMPANY_USER_SUPPLIER");
                db.SetToDeleted(model);
                db.SaveChanges();
            }



        }

        public bool CompanyUsersSuppliersExists(string companyKey, int supplierId, int companyUserId)
        {

            var companyUser = CompanyUserFetch(companyKey, companyUserId).FirstOrDefault();
            if (companyUser == null || companyUser.COMPANY_USER_ID == 0)
                throw new Exception("No CompanyUserId Key for that user");

            var supplier = SupplierFetch(supplierId);
            if (supplier == null || supplier.SUPPLIER_ID == 0)
                throw new Exception("Invalid Supplier Id");


            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_USER_SUPPLIERS.AsNoTracking()
                    .Where(a => a.SUPPLIER_ID == supplierId)
                    .Where(a => a.COMPANY_USER_ID == companyUserId);
                return q.Any();
            }

        }
        #endregion


        #region PriceList(Material) todo
        public Dto.Models.PRICE_LIST[] PriceListFetch(string companyKey, int? companyUserId = null, int? priceListId = null)
        {
            
  
            /*
            var companyUserFetch = new CompanyUserFetch { Repositories = Repositories };
            var companyUsers = companyUserFetch.Strategy(companyKey, companyUserId);

            var priceList = GetRepository<PRICE_LIST>().RetrieveAll();
            priceList = priceList.Where(p => companyUsers.Any(c => c.COMPANY_USER_ID == p.COMPANY_USER_ID));
            return ((priceListId != null) ? priceList.Where(p => p.PRICE_LIST_ID == priceListId) : priceList).ToArray();
            */


            var companyId = CompanyIdFetch(companyKey);
            if (companyId == 0)
                return null; // throw new Exception("Need a valid company key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST.AsNoTracking().AsQueryable();

                var companyUsers = CompanyUserFetch(companyKey, companyUserId);

                q = q.Where(a => companyUsers.Any(b => b.COMPANY_USER_ID == a.COMPANY_USER_ID));

                int priceListIdInt = priceListId.ToInt();
                if (priceListIdInt != 0)
                    q = q.Where(a => a.PRICE_LIST_ID == priceListIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST>(a)).ToArray();

            }
        }

        public Dto.Models.PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {

           
            // PROBLEM - allowing to search by primary key should overule everything else

            /* OLD FUNCTION
             var priceListFetch = new PriceListFetch { Repositories = Repositories };
            var priceLists = priceListFetch.Strategy(companyKey, companyUserId, priceListId);
           
            var priceListMaterials = GetRepository<PRICE_LIST_MATERIALS>().RetrieveAll();

            priceListMaterials = priceListMaterials.Where(p => priceLists.Any(c => c.PRICE_LIST_ID == p.PRICE_LIST_ID));
            return ((priceListMaterialId != null) ? priceListMaterials.Where(p => p.PRICE_LIST_MATERIAL_ID == priceListMaterialId) : priceListMaterials).ToArray();

             */
            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_MATERIALS.AsNoTracking().AsQueryable();

                var priceLists = PriceListFetch(companyKey, companyUserId, priceListId);
                q = q.Where(a => priceLists.Any(b => b.PRICE_LIST_ID == a.PRICE_LIST_ID));

                int priceListMaterialIdInt = priceListId.ToInt();
                if (priceListMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_LIST_MATERIAL_ID == priceListMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST_MATERIALS>(a)).ToArray();

            }

        }

        public Dto.Models.PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId)
        {

            /*
            PriceListFetch priceListFetch = new PriceListFetch { Repositories = Repositories };
            var priceList = priceListFetch.Strategy(companyKey, null, priceListId).FirstOrDefault();
            if (priceList == null)
                throw new Exception("Price Agreement does not exist for specified company Key");
            return GetRepository<PRICE_LIST_ITEM_TYPES>().RetrieveAll().Where(p => p.PRICE_LIST_ID == priceListId).ToArray();
            */

            var priceLists = PriceListFetch(companyKey, null, priceListId);
            if (priceLists == null || priceLists.Any() == false)
                throw new Exception("Price Agreement does not exist for specified company Key");

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_LIST_ITEM_TYPES.AsNoTracking().Where(p => p.PRICE_LIST_ID == priceListId);
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_LIST_ITEM_TYPES>(a)).ToArray();

            }
        }
        #endregion
        #region PriceAgreement(Material)
        public Dto.Models.PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null)
        {
            /*    var priceAgreements = GetRepository<PRICE_AGREEMENT>().RetrieveAll();
                        var priceListFetch = new PriceListFetch { Repositories = Repositories };
                        var PriceLists = priceListFetch.Strategy(companyKey, companyUserId, priceListId);
                        priceAgreements = priceAgreements.Where(p => PriceLists.Any(c => c.PRICE_LIST_ID == p.PRICE_LIST_ID));
                        return ((priceAgreementId != null) ? priceAgreements.Where(p => p.PRICE_AGREEMENT_ID == priceAgreementId) : priceAgreements).ToArray(); */


            var priceLists = PriceListFetch(companyKey, companyUserId, priceListId);

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT.AsNoTracking().AsQueryable();
                q = q.Where(a => priceLists.Any(b => b.PRICE_LIST_ID == a.PRICE_LIST_ID));
                int priceAgreementIdInt = priceAgreementId.ToInt();
                if (priceAgreementIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_ID == priceAgreementIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT>(a)).ToArray();
            }
        }


        public Dto.Models.PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceAgreementMaterialId = null)
        {
            // ODD BEHAVIOR TO SEARCH BY PRIMARY KEY AND ALSO OPTIONAL PARAMETERS

            // CHANGED priceList TO priceAgreementMaterialId AT THE END

            /* var priceAgreementMaterials = GetRepository<PRICE_AGREEMENT_MATERIALS>().RetrieveAll();
            PriceAgreementFetch priceAgreementFetch = new PriceAgreementFetch { Repositories = Repositories };
            var priceAgreements = priceAgreementFetch.Strategy(companyKey, companyUserId, priceListId, priceAgreementId);
            priceAgreementMaterials = priceAgreementMaterials.Where(p => priceAgreements.Any(c => c.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));
            return ((priceAgreementMaterialId != null) ? priceAgreementMaterials.Where(p => p.PRICE_LIST_MATERIAL_ID == priceAgreementMaterialId) : priceAgreementMaterials).ToArray(); */


            var priceAgreements = PriceAgreementFetch(companyKey, companyUserId, priceListId, priceAgreementId);


            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT_MATERIALS.AsNoTracking().AsQueryable();
                q = q.Where(a => priceAgreements.Any(b => b.PRICE_AGREEMENT_ID == a.PRICE_AGREEMENT_ID));
                int priceAgreementMaterialIdInt = priceAgreementMaterialId.ToInt();
                if (priceAgreementMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == priceAgreementMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS>(a)).ToArray();
            }

        }
        #endregion
        #region PriceAdjustment
        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, int priceAgreementId)
        {
     
            var priceAgreement = PriceAgreementFetch(companyKey, null, null, priceAgreementId).FirstOrDefault();

            if (priceAgreement == null)
                throw new Exception("PriceAgreementId Must be a valid price agreement that is associated with the company");

            using (var db = new EPA.Data.Db())
            {
                var model = new EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS() { PRICE_AGREEMENT_ID = priceAgreementId };
                db.PRICE_AGREEMENT_ADJUSTMENTS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(model);
            }

        }

        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            /* 
             *   var paa = priceAgreementAdjust;
            var paaf = new PriceAgreementAdjustmentFetch { Repositories = Repositories };
            var adjustment = paaf.Strategy(companyKey, null, null, null, paa.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();

            if (adjustment == null)
                throw new Exception("No Price AgreementAdjustment associated with the function");

            GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().Update(paa);
            return GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().Retrieve(p => p.PRICE_AGREEMENT_ADJUSTMENT_ID == paa.PRICE_AGREEMENT_ADJUSTMENT_ID);
               
             * 
             */

            var adjustment = PriceAgreementAdjustmentFetch(companyKey, null, priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();
            if (adjustment == null)
                throw new Exception("No Price AgreementAdjustment associated with the function");


            using (var db = new EPA.Data.Db())
            {
                var model = db.PRICE_AGREEMENT_ADJUSTMENTS.Where(a => a.PRICE_AGREEMENT_ADJUSTMENT_ID == priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on KEY " + priceAgreementAdjust.PRICE_AGREEMENT_ADJUSTMENT_ID);

                model = Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS, EPA.Models.PRICE_AGREEMENT_ADJUSTMENTS>(priceAgreementAdjust, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(model);
            }
        }

        public void PriceAgreementAdjustmentDelete(string companyKey, int priceAgreementAdjustmentId)
        {


            var adjustment = PriceAgreementAdjustmentFetch(companyKey, null, priceAgreementAdjustmentId).FirstOrDefault();
            if (adjustment == null)
                throw new Exception("No Price Agreement Adjustment associated with the function");


            using (var db = new EPA.Data.Db())
            {
                var model = db.PRICE_AGREEMENT_ADJUSTMENTS.Where(a => a.PRICE_AGREEMENT_ADJUSTMENT_ID == priceAgreementAdjustmentId).FirstOrDefault();
                if (model == null)
                    throw new Exception("Can't find object to modify based on KEY " + priceAgreementAdjustmentId);

                  db.SetToDeleted(model);
                db.SaveChanges();
                 
            }

        }

        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, int? priceAgreementId = null, int? priceAgreementAdjustmentId = null)
        {
            // PROBLEM
            // ERROR IN THIS
            /* InvokePreMethodExecutionEvent(MethodInfo.GetCurrentMethod());
            // PROBABLY NOT WHATS INTENDED
            var result = new PriceAgreementAdjustmentFetch { Repositories = repositories }.Strategy(companyKey, priceAgreementId, priceAdustmentId);
            InvokePostMethodExecutionEvent(MethodInfo.GetCurrentMethod());
            return result;
             * 
             *  var priceAgreementFetch = new PriceAgreementFetch { Repositories = Repositories };
            var priceAgreementAdjustments = GetRepository<PRICE_AGREEMENT_ADJUSTMENTS>().RetrieveAll();
            var priceAgreements = priceAgreementFetch.Strategy(companyKey, companyUserId, priceListId, PriceAgreementId);
            priceAgreementAdjustments = priceAgreementAdjustments.Where(p=> priceAgreements.Any(c => c.PRICE_AGREEMENT_ID == p.PRICE_AGREEMENT_ID));
            return ((PriceAdjustmentId != null)?priceAgreementAdjustments.Where(p=>p.PRICE_AGREEMENT_ADJUSTMENT_ID == PriceAdjustmentId):priceAgreementAdjustments).ToArray();
             * 
            */
            var priceAgreements = PriceAgreementFetch(companyKey, null, null, priceAgreementId);

            using (var db = new EPA.Data.Db())
            {
                var q = db.PRICE_AGREEMENT_MATERIALS.AsNoTracking().AsQueryable();
                q = q.Where(a => priceAgreements.Any(b => b.PRICE_AGREEMENT_ID == a.PRICE_AGREEMENT_ID));
                int priceAgreementMaterialIdInt = priceAgreementAdjustmentId.ToInt();
                if (priceAgreementMaterialIdInt != 0)
                {
                    q = q.Where(a => a.PRICE_AGREEMENT_MATERIAL_ID == priceAgreementMaterialIdInt);
                }
                return q.ToList().Select(a => Mapper.Map<EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS>(a)).ToArray();
            }


        }
        #endregion
    }
}
