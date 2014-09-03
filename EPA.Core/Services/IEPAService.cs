using System;
using System.ServiceModel;
namespace EPA.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEPAService" in both code and config file together.
    [ServiceContract]
    public interface IEPAService
    {
        [OperationContract]
        EPA.Dto.Models.COMPANY CompanyCreateByModel(Dto.Models.COMPANY company);
        [OperationContract]
        EPA.Dto.Models.COMPANY CompanyCreate(string description);
        [OperationContract]
        void CompanyDelete(EPA.Dto.Models.COMPANY company);
        [OperationContract]
        void CompanyDeleteById(int companyId);
        [OperationContract]
        EPA.Dto.Models.COMPANY CompanyFetchById(int companyId);
        [OperationContract]
        EPA.Dto.Models.COMPANY CompanyFetch(string key);
        [OperationContract]
        EPA.Dto.Models.COMPANY CompanyModify(EPA.Dto.Models.COMPANY company);
        [OperationContract]
        EPA.Dto.Models.COMPANY_SUPPLIERS CompanySupplierCreate(string companyKey, int supplierId, string description);
        [OperationContract]
        void CompanySupplierDelete(string companyKey, int supplierId);
        [OperationContract]
        EPA.Dto.Models.COMPANY_SUPPLIERS[] CompanySupplierFetch(string companyKey, int? supplierId = null);
        [OperationContract]
        EPA.Dto.Models.COMPANY_SUPPLIERS CompanySupplierModify(string companyKey, EPA.Dto.Models.COMPANY_SUPPLIERS companySuppliers);
        [OperationContract]
        EPA.Dto.Models.COMPANY_USERS CompanyUserCreateByModel(EPA.Dto.Models.COMPANY_USERS companyUser);
        [OperationContract]
        EPA.Dto.Models.COMPANY_USERS CompanyUserCreate(string companyKey, string descripton, string email);
        [OperationContract]
        void CompanyUserDelete(string companyKey, int companyUserId);
           [OperationContract]
        bool CompanyUserExists(string companyKey, string email);
        [OperationContract]
        EPA.Dto.Models.COMPANY_USERS[] CompanyUserFetch(string companyKey, int? companyUserId = null);
        [OperationContract]
        EPA.Dto.Models.COMPANY_USERS CompanyUserModify(string companyKey, EPA.Dto.Models.COMPANY_USERS companyUser);
        [OperationContract]
        EPA.Dto.Models.COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, int supplierId, int companyUserId);
        [OperationContract]
        void CompanyUsersSuppliersDelete(string companyKey, int supplierId, int companyUserId);
        [OperationContract]
        bool CompanyUsersSuppliersExists(string companyKey, int supplierId, int companyUserId);
        [OperationContract]
        void DoWork();
        [OperationContract]
        string GenerateAdjustmentReferenceNumber(string companyKey, int companyUserId);
        [OperationContract]
        string GenerateAgreementReferenceNumber(string companyKey, int companyUserId);
        [OperationContract]
        string GeneratePriceListReferenceNumber(string companyKey, int companyUserId);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, int priceAgreementId);
        [OperationContract]
        void PriceAgreementAdjustmentDelete(string companyKey, int priceAgreementAdjustmentId);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, int? priceAgreementId = null, int? priceAgreementAdjustmentId = null);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, EPA.Dto.Models.PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null);
          [OperationContract]
        EPA.Dto.Services.PriceAgreementIUDResponse PriceAgreementIUD(string companyKey, EPA.Dto.Services.PriceAgreementIUDWrapper priceAgreement, EPA.Dto.Services.PriceAgreementMaterialIUDWrapper[] materials);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceAgreementMaterialId = null);
        [OperationContract]
        EPA.Dto.Models.PRICE_LIST[] PriceListFetch(string companyKey, int? companyUserId = null, int? priceListId = null);
               [OperationContract]
        EPA.Dto.Services.ItemFilter[] PriceListGetItemFilters(string companyKey, int priceListId);
        [OperationContract]
        EPA.Dto.Models.PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId);
          [OperationContract]
        EPA.Dto.Services.PriceListIUDResponse PriceListIUD(string companyKey, EPA.Dto.Services.PriceListIUDWrapper priceList, EPA.Dto.Services.PriceListMaterialIUDWrapper[] materials);
        [OperationContract]
        EPA.Dto.Models.PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null);
        [OperationContract]
        void PriceListSetFilters(string companyKey, int priceListId, EPA.Dto.Models.PRICE_LIST_ITEM_TYPES[] iTypeFilters, EPA.Dto.Services.ItemFilter[] itemFilter);
        [OperationContract]
        EPA.Dto.Models.COMPANY[] SupplierCompanyFetch(string email);
        [OperationContract]
        EPA.Dto.Models.SUPPLIER SupplierCreateByModel(EPA.Dto.Models.SUPPLIER supplier);
        [OperationContract]
        EPA.Dto.Models.SUPPLIER SupplierCreate(string description, string email);
        [OperationContract]
        void SupplierDelete(EPA.Dto.Models.SUPPLIER supplier);
        [OperationContract]
        void SupplierDeleteById(int supplierId);
        [OperationContract]
        EPA.Dto.Models.SUPPLIER[] SupplierFetch(int? supplierId = null);
        [OperationContract]
        EPA.Dto.Models.SUPPLIER[] SupplierFetchByEmail(string email = null);
        [OperationContract]
        EPA.Dto.Models.SUPPLIER SupplierModify(EPA.Dto.Models.SUPPLIER supplier);
        [OperationContract]
        EPA.Dto.Models.PRICE_AGREEMENT[] SupplierPriceAgreementFetch(string email);
        [OperationContract]
        EPA.Dto.Models.WINDEV_CLIENT_VERSION WinDevVersionFetchLatestVersion();
    }
}
