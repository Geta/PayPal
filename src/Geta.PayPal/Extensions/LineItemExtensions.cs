using EPiServer.Commerce.Order;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Catalog.Managers;
using Mediachase.Commerce.Catalog.Objects;
using Mediachase.Commerce.Website.Helpers;

namespace Geta.PayPal.Extensions
{
    public static class ILineItemExtensions
    {
         /// <summary>
        /// Get display name with current language
        /// </summary>
        /// <param name="item">The line item of oder</param>
        /// <param name="maxSize">The number of character to get display name</param>
        /// <returns>Display name with current language</returns>
        public static string GetDisplayNameOfCurrentLanguage(this ILineItem item, int maxSize)
        {
            Entry entry = CatalogContext.Current.GetCatalogEntry(item.Code, new CatalogEntryResponseGroup(CatalogEntryResponseGroup.ResponseGroup.CatalogEntryFull));
            // if the entry is null (product is deleted), return item display name
            var displayName = (entry != null) ? StoreHelper.GetEntryDisplayName(entry).StripPreviewText(maxSize <= 0 ? 100 : maxSize) : item.DisplayName.StripPreviewText(maxSize <= 0 ? 100 : maxSize);

            return displayName;
        }

    }
}