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
        public Dto.COMPANY CompanyFetch(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).FirstOrDefault();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }
        public int CompanyIdFetch(string key)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.KEY == key).Select(a => a.COMPANY_ID).FirstOrDefault();
            }
        }
        public string CompanyKeyFetch(int companyId)
        {
            using (var db = new EPA.Data.Db())
            {
                return db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).Select(a => a.KEY).FirstOrDefault();
            }
        }
        public Dto.COMPANY CompanyFetch(int companyId)
        {
            if (companyId == 0)
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.COMPANIES.AsNoTracking().Where(a => a.COMPANY_ID == companyId).FirstOrDefault();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }

        public Dto.COMPANY CompanyCreate(string description)
        {
            return CompanyCreate(new Dto.COMPANY { DESCRIPTION = description });
        }

        public Dto.COMPANY CompanyCreate(Dto.COMPANY company)
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
                model = Mapper.Map<EPA.Dto.COMPANY, EPA.Models.COMPANY>(company, model);
                db.COMPANIES.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }

        public Dto.COMPANY CompanyModify(Dto.COMPANY company)
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

                model = Mapper.Map<EPA.Dto.COMPANY, EPA.Models.COMPANY>(company, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.COMPANY>(model);
            }
        }


        public void CompanyDelete(int companyId)
        {
            CompanyDelete(CompanyFetch(companyId));
        }
        public void CompanyDelete(Dto.COMPANY company)
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
        public Dto.SUPPLIER SupplierCreate(string description, string email)
        {
            return SupplierCreate(new Dto.SUPPLIER { DESCRIPTION = description, EMAIL = email })
        }
        public Dto.SUPPLIER SupplierCreate(Dto.SUPPLIER supplier)
        {

            if (string.IsNullOrEmpty(supplier.DESCRIPTION))
                throw new Exception("Description cannot be empty or null");
            else if (string.IsNullOrEmpty(supplier.EMAIL))
                throw new Exception("EMAIL cannot be empty or null");

            using (var db = new EPA.Data.Db())
            {

                var model = new EPA.Models.SUPPLIER();
                model = Mapper.Map<EPA.Dto.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                db.SUPPLIERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.SUPPLIER>(model);
            }
        }
        public Dto.SUPPLIER SupplierModify(Dto.SUPPLIER supplier)
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

                model = Mapper.Map<EPA.Dto.SUPPLIER, EPA.Models.SUPPLIER>(supplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.SUPPLIER>(model);
            }
        }

        public void SupplierDelete(int supplierId)
        {
            SupplierDelete(SupplierFetch(supplierId));
        }
        public void SupplierDelete(Dto.SUPPLIER supplier)
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
        public Dto.SUPPLIER SupplierFetch(int? supplierId = null)
        {
            int useSupplierId = supplierId.ToInt();
            if (useSupplierId == 0)
                return null;

            using (var db = new EPA.Data.Db())
            {
                var model = db.SUPPLIERS.AsNoTracking().Where(a => a.SUPPLIER_ID == useSupplierId).FirstOrDefault();
                return Mapper.Map<EPA.Dto.SUPPLIER>(model);
            }


        }
        #endregion
        #region Company Supplier
        public Dto.COMPANY_SUPPLIERS CompanySupplierCreate(string companyKey, int supplierId, string description)
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
                return Mapper.Map<EPA.Dto.COMPANY_SUPPLIERS>(model);
            }
        }

        public Dto.COMPANY_SUPPLIERS CompanySupplierModify(Dto.COMPANY_SUPPLIERS companySupplier)
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

                model = Mapper.Map<EPA.Dto.COMPANY_SUPPLIERS, EPA.Models.COMPANY_SUPPLIERS>(companySupplier, model);
                db.SetToModified(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.COMPANY_SUPPLIERS>(model);
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

        public Dto.COMPANY_SUPPLIERS[] CompanySuppliersFetch(string companyKey, int? supplierId = null)
        {
            if (string.IsNullOrEmpty(companyKey))
                return null;


            using (var db = new EPA.Data.Db())
            {
                var q = db.COMPANY_SUPPLIERS.AsNoTracking()
                    .Where(a => a.COMPANy.KEY == companyKey);

                var supplierIdInt = supplierId.ToInt();
                if (supplierIdInt != 0)
                    q = q.Where(a => a.SUPPLIER_ID == supplierIdInt);

                return q.ToList().Select(a => Mapper.Map<EPA.Dto.COMPANY_SUPPLIERS>(a)).ToArray();

            }


        }
        #endregion
        #region Company User
        public Dto.COMPANY_USERS CompanyUserCreate(string companyKey, string descripton, string email)
        {
            return CompanyUserCreate(new Dto.COMPANY_USERS { EMAIL = email, DESCRIPTION = descripton, COMPANY_ID = CompanyIdFetch(companyKey) });
        }
            public Dto.COMPANY_USERS CompanyUserCreate( Dto.COMPANY_USERS companyUser)
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
                model = Mapper.Map<EPA.Dto.COMPANY_USERS, EPA.Models.COMPANY_USERS>(companyUser, model);
                db.COMPANY_USERS.Add(model);
                db.SaveChanges();
                return Mapper.Map<EPA.Dto.COMPANY_USERS>(model);
            }

       
        }

        public Dto.COMPANY_USERS CompanyUserModify(string companyKey, Dto.COMPANY_USERS companyUser)
        {
            throw new NotImplementedException();
        }

        public void CompanyUserDelete(string companyKey, int companyUserId)
        {
            throw new NotImplementedException();
        }

        public Dto.COMPANY_USERS[] CompanyUserFetch(string companyKey, int? companyUserId = null)
        {
            throw new NotImplementedException();
        }

        public bool CompanyUserExists(string companyKey, string email)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Company User Suppliers
        public Dto.COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, int supplierId, int companyUserId)
        {
            throw new NotImplementedException();
        }

        public void CompanyUsersSuppliersDelete(string companyKey, int supplierId, int companyUserId)
        {
            throw new NotImplementedException();
        }

        public bool CompanyUsersSuppliersExists(string CompanyKey, int supplierId, int companyUserId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region PriceList(Material) todo
        public Dto.PRICE_LIST[] PriceListFetch(string companyKey, int? companyUserId = null, int? priceListId = null)
        {
            throw new NotImplementedException();
        }

        public Dto.PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {
            throw new NotImplementedException();
        }

        public Dto.PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region PriceAgreement(Material)
        public Dto.PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null)
        {
            throw new NotImplementedException();
        }

        public Dto.PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceList = null)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region PriceAdjustment
        public Dto.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, int PriceAgreementId)
        {
            throw new NotImplementedException();
        }

        public Dto.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, Dto.PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            throw new NotImplementedException();
        }

        public void PriceAgreementAdjustmentDelete(string companyKey, int priceAgreementAdjustmentId)
        {
            throw new NotImplementedException();
        }

        public Dto.PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, int? priceAgreementId = null, int? priceAdustmentId = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
