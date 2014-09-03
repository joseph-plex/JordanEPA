using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using EPA.Consumer.Service;

namespace EPA.Consumer
{
    public class Client : ClientBase, IEPAService
    {
        public Client(string uri)
            : base(uri)
        {
        }

        #region Company
        public COMPANY CompanyFetch(string key)
        {
            return channel.CompanyFetch(key);
        }
        public COMPANY CompanyFetchById(int companyId)
        {
            return channel.CompanyFetchById(companyId);
        }

        public COMPANY CompanyCreate(string description)
        {
            return channel.CompanyCreate(description);
        }

        public COMPANY CompanyCreateByModel(COMPANY company)
        {
            return channel.CompanyCreateByModel(company);
        }

        public COMPANY CompanyModify(COMPANY company)
        {
            return channel.CompanyModify(company);
        }

        public void CompanyDeleteById(int companyId)
        {
            channel.CompanyDeleteById(companyId);
        }
        public void CompanyDelete(COMPANY company)
        {
            channel.CompanyDelete(company);
        }

        #endregion

        #region WinDevVersion

        public WINDEV_CLIENT_VERSION WinDevVersionFetchLatestVersion()
        {
            return channel.WinDevVersionFetchLatestVersion();
        }

        #endregion

        #region Supplier
        public SUPPLIER SupplierCreate(string description, string email)
        {
            return channel.SupplierCreate(description, email);
        }
        public SUPPLIER SupplierCreateByModel(SUPPLIER supplier)
        {
            return channel.SupplierCreateByModel(supplier);
        }


        public SUPPLIER SupplierModify(SUPPLIER supplier)
        {
            return channel.SupplierModify(supplier);
        }

        public void SupplierDeleteById(int supplierId)
        {
            channel.SupplierDeleteById(supplierId);
        }
        public void SupplierDelete(SUPPLIER supplier)
        {
            channel.SupplierDelete(supplier);
        }

        public SUPPLIER[] SupplierFetch(int? supplierId = null)
        {
            return channel.SupplierFetch(supplierId);
        }
        #endregion

        #region Company Supplier
        public COMPANY_SUPPLIERS CompanySupplierCreate(string companyKey, int supplierId, string description)
        {
            return channel.CompanySupplierCreate(companyKey, supplierId, description);

        }
        public COMPANY_SUPPLIERS CompanySupplierModify(string companyKey, COMPANY_SUPPLIERS companySuppliers)
        {
            return channel.CompanySupplierModify(companyKey, companySuppliers);
        }

        /*  public COMPANY_SUPPLIERS CompanySupplierModify(COMPANY_SUPPLIERS companySupplier)
          {
              return channel.CompanySupplierModify(companySupplier);

          } */
        public void CompanySupplierDelete(string companyKey, int supplierId)
        {
            channel.CompanySupplierDelete(companyKey, supplierId);

        }

        public COMPANY_SUPPLIERS[] CompanySupplierFetch(string companyKey, int? supplierId = null)
        {
            return channel.CompanySupplierFetch(companyKey, supplierId);
        }
        public PRICE_AGREEMENT[] SupplierPriceAgreementFetch(string email)
        {
            return channel.SupplierPriceAgreementFetch(email);
        }

        public COMPANY[] SupplierCompanyFetch(string email)
        {
            return channel.SupplierCompanyFetch(email);
        }

        public SUPPLIER[] SupplierFetchByEmail(string email = null)
        {
            return channel.SupplierFetchByEmail(email);
        }
        #endregion

        #region Company User
        public COMPANY_USERS CompanyUserCreate(string companyKey, string descripton, string email)
        {
            return channel.CompanyUserCreate(companyKey, descripton, email);
        }
        public COMPANY_USERS CompanyUserCreateByModel(COMPANY_USERS companyUser)
        {
            return channel.CompanyUserCreateByModel(companyUser);
        }

        public COMPANY_USERS CompanyUserModify(string companyKey, COMPANY_USERS companyUser)
        {

            return channel.CompanyUserModify(companyKey, companyUser);
        }

        public void CompanyUserDelete(string companyKey, int companyUserId)
        {
            channel.CompanyUserDelete(companyKey, companyUserId);

        }
        public COMPANY_USERS[] CompanyUserFetch(string companyKey, int? companyUserId = null)
        {
            return channel.CompanyUserFetch(companyKey, companyUserId);
        }
        public bool CompanyUserExists(string companyKey, string email)
        {
            return channel.CompanyUserExists(companyKey, email);
        }
        #endregion

        #region Company User Suppliers
        public COMPANY_USER_SUPPLIERS CompanyUsersSuppliersCreate(string companyKey, int supplierId, int companyUserId)
        {
            return channel.CompanyUsersSuppliersCreate(companyKey, supplierId, companyUserId);

        }

        public void CompanyUsersSuppliersDelete(string companyKey, int supplierId, int companyUserId)
        {
            channel.CompanyUsersSuppliersDelete(companyKey, supplierId, companyUserId);
        }

        public bool CompanyUsersSuppliersExists(string companyKey, int supplierId, int companyUserId)
        {
            return channel.CompanyUsersSuppliersExists(companyKey, supplierId, companyUserId);
        }
        #endregion

        #region PriceList(Material)
        public PRICE_LIST[] PriceListFetch(string companyKey, int? companyUserId = null, int? priceListId = null)
        {

            return channel.PriceListFetch(companyKey, companyUserId, priceListId);

        }

        public PRICE_LIST_MATERIALS[] PriceListMaterialFetch(string companyKey, int? companyUserId = null, int? priceListId = null, int? priceListMaterialId = null)
        {
            return channel.PriceListMaterialFetch(companyKey, companyUserId, priceListId, priceListMaterialId);

        }

        public PRICE_LIST_ITEM_TYPES[] PriceListGetItemTypeFilters(string companyKey, int priceListId)
        {
            return channel.PriceListGetItemTypeFilters(companyKey, priceListId);


        }
        public void PriceListSetFilters(string companyKey, int priceListId, PRICE_LIST_ITEM_TYPES[] iTypeFilters, ItemFilter[] itemFilter)
        {
            channel.PriceListSetFilters(companyKey, priceListId, iTypeFilters, itemFilter);

        }

        public ItemFilter[] PriceListGetItemFilters(string companyKey, int priceListId)
        {
            return channel.PriceListGetItemFilters(companyKey, priceListId);

        }
        #endregion

        #region PriceAgreement(Material)
        public PRICE_AGREEMENT[] PriceAgreementFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null)
        {
            return channel.PriceAgreementFetch(companyKey, companyUserId, priceListId, priceAgreementId);
        }


        public PRICE_AGREEMENT_MATERIALS[] PriceAgreementMaterialFetch(string companyKey, int? companyUserId, int? priceListId = null, int? priceAgreementId = null, int? priceAgreementMaterialId = null)
        {
            return channel.PriceAgreementMaterialFetch(companyKey, companyUserId, priceListId, priceAgreementId, priceAgreementMaterialId);


        }
        #endregion

        #region PriceAdjustment
        public PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentCreate(string companyKey, int priceAgreementId)
        {
            return channel.PriceAgreementAdjustmentCreate(companyKey, priceAgreementId);


        }

        public PRICE_AGREEMENT_ADJUSTMENTS PriceAgreementAdjustmentModify(string companyKey, PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            return channel.PriceAgreementAdjustmentModify(companyKey, priceAgreementAdjust);

        }

        public void PriceAgreementAdjustmentDelete(string companyKey, int priceAgreementAdjustmentId)
        {

            channel.PriceAgreementAdjustmentDelete(companyKey, priceAgreementAdjustmentId);


        }

        public PRICE_AGREEMENT_ADJUSTMENTS[] PriceAgreementAdjustmentFetch(string companyKey, int? priceAgreementId = null, int? priceAgreementAdjustmentId = null)
        {
            return channel.PriceAgreementAdjustmentFetch(companyKey, priceAgreementId, priceAgreementAdjustmentId);


        }
        #endregion

        #region IUD

        public PriceListIUDResponse PriceListIUD(string companyKey, PriceListIUDWrapper priceList,
            PriceListMaterialIUDWrapper[] materials)
        {
            return channel.PriceListIUD(companyKey, priceList, materials);

        }

        public PriceAgreementIUDResponse PriceAgreementIUD(string companyKey, PriceAgreementIUDWrapper priceAgreement, PriceAgreementMaterialIUDWrapper[] materials)
        {
            return channel.PriceAgreementIUD(companyKey, priceAgreement, materials);

        }
        #endregion

        #region Generate

        public string GenerateAdjustmentReferenceNumber(string companyKey, int companyUserId)
        {
            return channel.GenerateAdjustmentReferenceNumber(companyKey, companyUserId);

        }

        public string GenerateAgreementReferenceNumber(string companyKey, int companyUserId)
        {
            return channel.GenerateAgreementReferenceNumber(companyKey, companyUserId);

        }

        public string GeneratePriceListReferenceNumber(string companyKey, int companyUserId)
        {
            return channel.GeneratePriceListReferenceNumber(companyKey, companyUserId);
        }
        #endregion















        public Task<COMPANY> CompanyCreateAsync(string description)
        {
            throw new System.NotImplementedException();
        }

        public Task CompanyDeleteAsync(COMPANY company)
        {
            throw new System.NotImplementedException();
        }

        public Task CompanyDeleteByIdAsync(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY> CompanyFetchByIdAsync(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY> CompanyFetchAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY> CompanyModifyAsync(COMPANY company)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_SUPPLIERS> CompanySupplierCreateAsync(string companyKey, int supplierId, string description)
        {
            throw new System.NotImplementedException();
        }

        public Task CompanySupplierDeleteAsync(string companyKey, int supplierId)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_SUPPLIERS[]> CompanySupplierFetchAsync(string companyKey, int? supplierId)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_SUPPLIERS> CompanySupplierModifyAsync(string companyKey, COMPANY_SUPPLIERS companySuppliers)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_USERS> CompanyUserCreateByModelAsync(COMPANY_USERS companyUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_USERS> CompanyUserCreateAsync(string companyKey, string descripton, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task CompanyUserDeleteAsync(string companyKey, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CompanyUserExistsAsync(string companyKey, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_USERS[]> CompanyUserFetchAsync(string companyKey, int? companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_USERS> CompanyUserModifyAsync(string companyKey, COMPANY_USERS companyUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY_USER_SUPPLIERS> CompanyUsersSuppliersCreateAsync(string companyKey, int supplierId, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task CompanyUsersSuppliersDeleteAsync(string companyKey, int supplierId, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CompanyUsersSuppliersExistsAsync(string companyKey, int supplierId, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public void DoWork()
        {
            throw new System.NotImplementedException();
        }

        public Task DoWorkAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GenerateAdjustmentReferenceNumberAsync(string companyKey, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GenerateAgreementReferenceNumberAsync(string companyKey, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GeneratePriceListReferenceNumberAsync(string companyKey, int companyUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT_ADJUSTMENTS> PriceAgreementAdjustmentCreateAsync(string companyKey, int priceAgreementId)
        {
            throw new System.NotImplementedException();
        }

        public Task PriceAgreementAdjustmentDeleteAsync(string companyKey, int priceAgreementAdjustmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT_ADJUSTMENTS[]> PriceAgreementAdjustmentFetchAsync(string companyKey, int? priceAgreementId, int? priceAgreementAdjustmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT_ADJUSTMENTS> PriceAgreementAdjustmentModifyAsync(string companyKey, PRICE_AGREEMENT_ADJUSTMENTS priceAgreementAdjust)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT[]> PriceAgreementFetchAsync(string companyKey, int? companyUserId, int? priceListId, int? priceAgreementId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PriceAgreementIUDResponse> PriceAgreementIUDAsync(string companyKey, PriceAgreementIUDWrapper priceAgreement, PriceAgreementMaterialIUDWrapper[] materials)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT_MATERIALS[]> PriceAgreementMaterialFetchAsync(string companyKey, int? companyUserId, int? priceListId, int? priceAgreementId, int? priceAgreementMaterialId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_LIST[]> PriceListFetchAsync(string companyKey, int? companyUserId, int? priceListId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_LIST_ITEM_TYPES[]> PriceListGetItemTypeFiltersAsync(string companyKey, int priceListId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PriceListIUDResponse> PriceListIUDAsync(string companyKey, PriceListIUDWrapper priceList, PriceListMaterialIUDWrapper[] materials)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_LIST_MATERIALS[]> PriceListMaterialFetchAsync(string companyKey, int? companyUserId, int? priceListId, int? priceListMaterialId)
        {
            throw new System.NotImplementedException();
        }

        public Task PriceListSetFiltersAsync(string companyKey, int priceListId, PRICE_LIST_ITEM_TYPES[] iTypeFilters, ItemFilter[] itemFilter)
        {
            throw new System.NotImplementedException();
        }

        public Task<COMPANY[]> SupplierCompanyFetchAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<SUPPLIER> SupplierCreateByModelAsync(SUPPLIER supplier)
        {
            throw new System.NotImplementedException();
        }

        public Task<SUPPLIER> SupplierCreateAsync(string description, string email)
        {
            throw new System.NotImplementedException();
        }

        public Task SupplierDeleteAsync(SUPPLIER supplier)
        {
            throw new System.NotImplementedException();
        }

        public Task SupplierDeleteByIdAsync(int supplierId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SUPPLIER[]> SupplierFetchAsync(int? supplierId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SUPPLIER[]> SupplierFetchByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<SUPPLIER> SupplierModifyAsync(SUPPLIER supplier)
        {
            throw new System.NotImplementedException();
        }

        public Task<PRICE_AGREEMENT[]> SupplierPriceAgreementFetchAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<WINDEV_CLIENT_VERSION> WinDevVersionFetchLatestVersionAsync()
        {
            throw new System.NotImplementedException();
        }


        public Task<COMPANY> CompanyCreateByModelAsync(COMPANY company)
        {
            throw new System.NotImplementedException();
        }

        public Task<ItemFilter[]> PriceListGetItemFiltersAsync(string companyKey, int priceListId)
        {
            throw new System.NotImplementedException();
        }
    }

}
