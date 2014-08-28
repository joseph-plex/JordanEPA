using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EPA.Dto;

namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEPAService" in both code and config file together.
    [ServiceContract]
    public interface IEPAService
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        Dto.COMPANY CompanyFetch(string key);




        #region Company Operations
        [OperationContract]
        Dto.COMPANY CompanyCreate(String description);
        [OperationContract]
        Dto.COMPANY CompanyModify(Dto.COMPANY company);
        [OperationContract]
        void CompanyDelete(Dto.COMPANY company);
        #endregion


        #region Supplier Operations
        [OperationContract]
        SUPPLIER SupplierCreate(String description);
        [OperationContract]
        SUPPLIER SupplierCreate(SUPPLIER description);
        [OperationContract]
        SUPPLIER SupplierModify(SUPPLIER supplier);
        [OperationContract]
        void SupplierDelete(Int32 supplierId);
        [OperationContract]
        SUPPLIER SupplierFetch(Int32? supplierId = null);
        #endregion
        #region Company Supplier Operations
        [OperationContract]
        COMPANY_SUPPLIERS CompanySupplierCreate(String companyKey, Int32 companyId, String description);
        [OperationContract]
        COMPANY_SUPPLIERS CompanySupplierModify(String companyKey, COMPANY_SUPPLIERS companySuppliers);
        [OperationContract]
        void CompanySupplierDelete(String companyKey, Int32 supplierId);
        [OperationContract]
        COMPANY_SUPPLIERS[] CompanySupplierFetch(String companyKey, Int32? supplierId = null);
        #endregion
        #region Company User Operations
        [OperationContract]
        COMPANY_USERS CompanyUserCreate(String companyKey, String companyUser, String email);
        [OperationContract]
        COMPANY_USERS CompanyUserModify(String companyKey, COMPANY_USERS companyUser);
        [OperationContract]
        void CompanyUserDelete(String companyKey, Int32 companyUserId);
        [OperationContract]
        COMPANY_USERS[] CompanyUserFetch(String companyKey, Int32? companyUserId = null);

        Boolean CompanyUserExists(String companyKey, String email);
        #endregion
        #region Company User Suppliers Operations
        [OperationContract]
        COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(String companyKey, Int32 supplierId, Int32 companyUserId);
        [OperationContract]
        void CompanyUsersSuppliersDelete(String companyKey, Int32 supplierId, Int32 companyUserId);
        [OperationContract]
        Boolean CompanyUsersSuppliersExists(String CompanyKey, Int32 supplierId, Int32 companyUserId);
        #endregion



        // TODO - FIGURE OUT WHATS GOING ON HERE
        #region PriceList(Material) Operations
      /*  [OperationContract]
        PriceListIUDResponse PriceListIUD(String companyKey, PriceListIUDWrapper priceList, PriceListMaterialIUDWrapper[] materials);

        [OperationContract]
        PriceAgreementIUDResponse PriceAgreementIUD(String companyKey, PriceListIUDWrapper PriceAgreement, PriceListMaterialIUDWrapper[] materials);
        [OperationContract]
        void PriceListSetFilters(String companyKey, Int32 priceListId, PRICE_LIST_ITEM_TYPES[] iTypeFilters, ItemFilter[] itemFilter);
        [OperationContract]
        ItemFilter[] PriceListGetItemFilters(String companyKey, Int32 priceListId);
        */
        [OperationContract]
        PRICE_LIST[] PriceListFetch(String companyKey, Int32? companyUserId = null, Int32? priceListId = null);
        [OperationContract]
        PRICE_LIST_MATERIALS[] PriceListMaterialFetch(String companyKey, Int32? companyUserId = null, Int32? priceListId = null, Int32? priceListMaterialId = null);

        [OperationContract]
        PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId);
        #endregion
        #region PriceAgreement(Material) Operations
   
        PRICE_AGREEMENT[] PriceAgreementFetch(String companyKey, Int32? companyUserId, Int32? priceListId = null, Int32? priceAgreementId = null);
        [OperationContract]
        PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(String companyKey, Int32? companyUserId, Int32? priceListId = null, Int32? priceAgreementId = null, Int32? priceList = null);
        #endregion
        #region PriceAdjustment Operations
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(String companyKey, Int32 PriceAgreementId);
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust);
        [OperationContract]
        void PriceAgreementAdjustmentDelete(String companyKey, Int32 priceAgreementAdjustmentId);
        [OperationContract]
        PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(String companyKey, Int32? priceAgreementId = null, Int32? priceAdustmentId = null);
        #endregion

    }
}
