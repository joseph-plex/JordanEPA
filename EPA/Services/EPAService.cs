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
        public static EPAService GetService
        {
            get
            {
                return new EPAService();
            }
        }
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
            return EPA.Behaviors.CompanyFetch.Execute(key);
        }
        public Dto.Models.COMPANY CompanyFetch(int companyId)
        {
            return EPA.Behaviors.CompanyFetch.Execute(companyId);
        }

        public Dto.Models.COMPANY CompanyCreate(string description)
        {
            return EPA.Behaviors.CompanyCreate.Execute(description);
        }

        public static Dto.Models.COMPANY CompanyCreate(Dto.Models.COMPANY company)
        {
            return EPA.Behaviors.CompanyCreate.Execute(company);
        }

        public Dto.Models.COMPANY CompanyModify(Dto.Models.COMPANY company)
        {
            return EPA.Behaviors.CompanyModify.Execute(company);
        }

        public void CompanyDelete(int companyId)
        {
            EPA.Behaviors.CompanyDelete.Execute(companyId);
        }
        public void CompanyDelete(Dto.Models.COMPANY company)
        {
            EPA.Behaviors.CompanyDelete.Execute(company);
        }

        #endregion
        
        #region WinDevVersion

        public Dto.Models.WINDEV_CLIENT_VERSION WinDevVersionFetchLatestVersion()
        {
            return EPA.Behaviors.WinDevVersionFetchLatestVersion.Execute();
        }

        #endregion

        #region Supplier
        public Dto.Models.SUPPLIER SupplierCreate(string description, string email)
        {
            return EPA.Behaviors.SupplierCreate.Execute(description, email);
        }
        public Dto.Models.SUPPLIER SupplierCreate(Dto.Models.SUPPLIER supplier)
        {
            return EPA.Behaviors.SupplierCreate.Execute(supplier);
        }


        public Dto.Models.SUPPLIER SupplierModify(Dto.Models.SUPPLIER supplier)
        {
            return EPA.Behaviors.SupplierModify.Execute(supplier);
        }

        public void SupplierDelete(int supplierId)
        {
            EPA.Behaviors.SupplierDelete.Execute(supplierId);
        }
        public void SupplierDelete(Dto.Models.SUPPLIER supplier)
        {
            EPA.Behaviors.SupplierDelete.Execute(supplier);
        }

        public Dto.Models.SUPPLIER[] SupplierFetch(int? supplierId = null)
        {
            return EPA.Behaviors.SupplierFetch.Execute(supplierId);
        }
        #endregion

        #region Company Supplier
        public Dto.Models.COMPANY_SUPPLIERS CompanySupplierCreate(string companyKey, int supplierId, string description)
        {
            return EPA.Behaviors.CompanySupplierCreate.Execute(companyKey, supplierId, description);

        }
        public Dto.Models.COMPANY_SUPPLIERS CompanySupplierModify(string companyKey, Dto.Models.COMPANY_SUPPLIERS companySuppliers)
        {
            return EPA.Behaviors.CompanySupplierModify.Execute(companyKey, companySuppliers);
        }

        private Dto.Models.COMPANY_SUPPLIERS CompanySupplierModify(Dto.Models.COMPANY_SUPPLIERS companySupplier)
        {
            return EPA.Behaviors.CompanySupplierModify.Execute(companySupplier);

        }
        public void CompanySupplierDelete(string companyKey, int supplierId)
        {
            EPA.Behaviors.CompanySupplierDelete.Execute(companyKey, supplierId);

        }

        public Dto.Models.COMPANY_SUPPLIERS[] CompanySupplierFetch(string companyKey, int? supplierId = null)
        {
            return EPA.Behaviors.CompanySupplierFetch.Execute(companyKey, supplierId);
        }
        public EPA.Dto.Models.PRICE_AGREEMENT[] SupplierPriceAgreementFetch(string email)
        {
            return EPA.Behaviors.SupplierPriceAgreementFetch.Execute(email);
        }

        public EPA.Dto.Models.COMPANY[] SupplierCompanyFetch(string email)
        {
            return EPA.Behaviors.SupplierCompanyFetch.Execute(email);
        }

        public EPA.Dto.Models.SUPPLIER[] SupplierFetchByEmail(string email = null)
        {
            return EPA.Behaviors.SupplierFetchByEmail.Execute(email);
        }
        #endregion
        
        #region Company User
        public Dto.Models.COMPANY_USERS CompanyUserCreate(string companyKey, string descripton, string email)
        {
            return EPA.Behaviors.CompanyUserCreate.Execute(companyKey, descripton, email);
        }
        public Dto.Models.COMPANY_USERS CompanyUserCreate(Dto.Models.COMPANY_USERS companyUser)
        {
            return EPA.Behaviors.CompanyUserCreate.Execute(companyUser);
        }

        public Dto.Models.COMPANY_USERS CompanyUserModify(string companyKey, Dto.Models.COMPANY_USERS companyUser)
        {

            return EPA.Behaviors.CompanyUserModify.Execute(companyKey, companyUser);
        }

        public void CompanyUserDelete(string companyKey, int companyUserId)
        {
            EPA.Behaviors.CompanyUserDelete.Execute(companyKey, companyUserId);

        }
        public Dto.Models.COMPANY_USERS[] CompanyUserFetch(string companyKey, int? companyUserId = null)
        {
            return EPA.Behaviors.CompanyUserFetch.Execute(companyKey, companyUserId);
        }
        public bool CompanyUserExists(string companyKey, string email)
        {
            return EPA.Behaviors.CompanyUserExists.Execute(companyKey, email);
        }
        #endregion

        #region Company User Suppliers
        public Dto.Models.COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, int supplierId, int companyUserId)
        {
            return EPA.Behaviors.CompanyUsersSuppliersCreate.Execute(companyKey, supplierId, companyUserId);

        }

        public void CompanyUsersSuppliersDelete(string companyKey, int supplierId, int companyUserId)
        {
            EPA.Behaviors.CompanyUsersSuppliersDelete.Execute(companyKey, supplierId, companyUserId);
        }

        public bool CompanyUsersSuppliersExists(string companyKey, int supplierId, int companyUserId)
        {
            return EPA.Behaviors.CompanyUsersSuppliersExists.Execute(companyKey, supplierId, companyUserId);
        }
        #endregion
        
        #region PriceList(Material)  
        public Dto.Models.PRICE_LIST[] PriceListFetch(string companyKey, int? companyUserId = null, int? priceListId = null)
        {

            return EPA.Behaviors.PriceListFetch.Execute(companyKey, companyUserId, priceListId);

        }

        public Dto.Models.PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {
            return EPA.Behaviors.PriceListMaterialFetch.Execute(companyKey, companyUserId, priceListId, priceListMaterialId);

        }

        public Dto.Models.PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId)
        {
            return EPA.Behaviors.PriceListGetItemTypeFilters.Execute(companyKey, priceListId);


        }
        public void PriceListSetFilters(string companyKey, int priceListId, Dto.Models.PRICE_LIST_ITEM_TYPES[] iTypeFilters, EPA.Dto.Services.ItemFilter[] itemFilter)
        {
            EPA.Behaviors.PriceListSetFilters.Execute(companyKey, priceListId, iTypeFilters, itemFilter);

        }

        public Dto.Services.ItemFilter[] PriceListGetItemFilters(string companyKey, int priceListId)
        {
            return EPA.Behaviors.PriceListGetItemFilters.Execute(companyKey, priceListId);

        }
        #endregion

        #region PriceAgreement(Material)
        public Dto.Models.PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null)
        {
            return EPA.Behaviors.PriceAgreementFetch.Execute(companyKey, companyUserId, priceListId, priceAgreementId);
        }


        public Dto.Models.PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceAgreementMaterialId = null)
        {
            return EPA.Behaviors.PriceAgreementMaterialFetch.Execute(companyKey, companyUserId, priceListId, priceAgreementId, priceAgreementMaterialId);


        }
        #endregion

        #region PriceAdjustment
        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, int priceAgreementId)
        {
            return EPA.Behaviors.PriceAgreementAdjustmentCreate.Execute(companyKey, priceAgreementId);


        }

        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            return EPA.Behaviors.PriceAgreementAdjustmentModify.Execute(companyKey, priceAgreementAdjust);

        }

        public void PriceAgreementAdjustmentDelete(string companyKey, int priceAgreementAdjustmentId)
        {

            EPA.Behaviors.PriceAgreementAdjustmentDelete.Execute(companyKey, priceAgreementAdjustmentId);


        }

        public Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, int? priceAgreementId = null, int? priceAgreementAdjustmentId = null)
        {
            return EPA.Behaviors.PriceAgreementAdjustmentFetch.Execute(companyKey, priceAgreementId, priceAgreementAdjustmentId);


        }
        #endregion
        
        #region IUD

        public EPA.Dto.Services.PriceListIUDResponse PriceListIUD(string companyKey, EPA.Dto.Services.PriceListIUDWrapper priceList,
            EPA.Dto.Services.PriceListMaterialIUDWrapper[] materials)
        {
            return EPA.Behaviors.PriceListIUD.Execute(companyKey, priceList);

        }

        public EPA.Dto.Services.PriceAgreementIUDResponse PriceAgreementIUD(string companyKey, EPA.Dto.Services.PriceAgreementIUDWrapper priceAgreement, EPA.Dto.Services.PriceAgreementMaterialIUDWrapper[] materials)
        {
            return EPA.Behaviors.PriceAgreementIUD.Execute(companyKey, priceAgreement, materials);

        }
            #endregion
        
        #region Generate

        public string GenerateAdjustmentReferenceNumber(string companyKey, int companyUserId)
        {
            return EPA.Behaviors.GenerateAdjustmentReferenceNumber.Execute(companyKey, companyUserId);

        }

        public string GenerateAgreementReferenceNumber(string companyKey, int companyUserId)
        {
            return EPA.Behaviors.GenerateAgreementReferenceNumber.Execute(companyKey, companyUserId);

        }

        public string GeneratePriceListReferenceNumber(string companyKey, int companyUserId)
        {
            return EPA.Behaviors.GeneratePriceListReferenceNumber.Execute(companyKey, companyUserId);
        }
        #endregion
    }
}
