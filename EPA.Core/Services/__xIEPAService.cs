using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EPA.Dto.Models;

namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEPAService" in both code and config file together.
    [ServiceContract]
    public interface __xIEPAService
    {
        [OperationContract]
        void DoWork();
   

            [OperationContract]
        Dto.Models.WINDEV_CLIENT_VERSION WinDevVersionFetchLatestVersion(string key);

        

        #region Company 

        [OperationContract]
        Dto.Models.COMPANY CompanyFetch(string key);

        [OperationContract]
        Dto.Models.COMPANY CompanyCreate(string description);
        [OperationContract]
        Dto.Models.COMPANY CompanyModify(Dto.Models.COMPANY company);
        [OperationContract]
        void CompanyDelete(Dto.Models.COMPANY company);
        #endregion


        #region Supplier Operations
        [OperationContract]
        SUPPLIER SupplierCreate(string description, string email);
        [OperationContract]
        SUPPLIER SupplierCreate(SUPPLIER supplier);
        [OperationContract]
        SUPPLIER SupplierModify(SUPPLIER supplier);
        [OperationContract]
        void SupplierDelete(Int32 supplierId);
        [OperationContract]
        SUPPLIER SupplierFetch(Int32? supplierId = null);
        #endregion
        #region Company Supplier Operations
        [OperationContract]
        COMPANY_SUPPLIERS CompanySupplierCreate(string  companyKey, Int32 companyId, string description);
        [OperationContract]
        COMPANY_SUPPLIERS CompanySupplierModify(string companyKey, COMPANY_SUPPLIERS companySuppliers);
        [OperationContract]
        void CompanySupplierDelete(string companyKey, Int32 supplierId);
        [OperationContract]
        COMPANY_SUPPLIERS[] CompanySupplierFetch(string companyKey, Int32? supplierId = null);
        #endregion
        #region Company User Operations
        [OperationContract]
        COMPANY_USERS CompanyUserCreate(string companyKey, string companyUser, string email);
        [OperationContract]
        COMPANY_USERS CompanyUserModify(string companyKey, COMPANY_USERS companyUser);
        [OperationContract]
        void CompanyUserDelete(string companyKey, Int32 companyUserId);
        [OperationContract]
        COMPANY_USERS[] CompanyUserFetch(string companyKey, Int32? companyUserId = null);

        Boolean CompanyUserExists(string companyKey, string email);
        #endregion
        #region Company User Suppliers Operations
        [OperationContract]
        COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, Int32 supplierId, Int32 companyUserId);
        [OperationContract]
        void CompanyUsersSuppliersDelete(string companyKey, Int32 supplierId, Int32 companyUserId);
        [OperationContract]
        Boolean CompanyUsersSuppliersExists(string CompanyKey, Int32 supplierId, Int32 companyUserId);
        #endregion



        // TODO - FIGURE OUT WHATS GOING ON HERE
        #region PriceList(Material) Operations
       [OperationContract]
        EPA.Dto.Services.PriceListIUDResponse PriceListIUD(string companyKey, EPA.Dto.Services.PriceListIUDWrapper priceList, EPA.Dto.Services.PriceListMaterialIUDWrapper[] materials);

        [OperationContract]
       EPA.Dto.Services.PriceAgreementIUDResponse PriceAgreementIUD(string companyKey, EPA.Dto.Services.PriceListIUDWrapper PriceAgreement, EPA.Dto.Services.PriceListMaterialIUDWrapper[] materials);
        [OperationContract]
        void PriceListSetFilters(string companyKey, Int32 priceListId, PRICE_LIST_ITEM_TYPES[] iTypeFilters, EPA.Dto.Services.ItemFilter[] itemFilter);
        [OperationContract]
       Dto.Services.ItemFilter[] PriceListGetItemFilters(string companyKey, Int32 priceListId);
        




        [OperationContract]
        PRICE_LIST[] PriceListFetch(string companyKey, Int32? companyUserId = null, Int32? priceListId = null);
        [OperationContract]
        PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, Int32? companyUserId = null, Int32? priceListId = null, Int32? priceListMaterialId = null);

        [OperationContract]
        PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId);
        #endregion
        #region PriceAgreement(Material) Operations
   
        PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, Int32? companyUserId, Int32? priceListId = null, Int32? priceAgreementId = null);
        [OperationContract]
        PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, Int32? companyUserId, Int32? priceListId = null, Int32? priceAgreementId = null, Int32? priceAgreementMaterialId = null);
        #endregion
        #region PriceAdjustment Operations
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, Int32 PriceAgreementId);
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust);
        [OperationContract]
        void PriceAgreementAdjustmentDelete(string companyKey, Int32 priceAgreementAdjustmentId);
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, Int32? priceAgreementId = null, Int32? priceAdustmentId = null);
        #endregion

    }
}
