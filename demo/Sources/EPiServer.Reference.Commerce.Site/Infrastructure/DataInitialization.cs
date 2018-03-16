
using System.Linq;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Mediachase.BusinessFoundation.Data;
using Mediachase.BusinessFoundation.Data.Meta.Management;
using Mediachase.Commerce.Orders;
using Mediachase.MetaDataPlus;
using Mediachase.MetaDataPlus.Configurator;
using MetaClass = Mediachase.BusinessFoundation.Data.Meta.Management.MetaClass;
using EPiServer.Reference.Commerce.Site.Infrastructure;
using MetaField = Mediachase.MetaDataPlus.Configurator.MetaField;

namespace EPiServer.Reference.Commerce.Site.Infrastructure
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Data.DataInitialization))]
    [ModuleDependency(typeof(ServiceContainerInitialization))]
    public class DataInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            UpdateCartMetaFields();
        }

        private void UpdateCartMetaFields()
        {
            MetaDataContext mdContext = OrderContext.MetaDataContext;
            var successRedirectUrl = GetOrCreateMetaField(mdContext, MetaDataConstants.OrderNamespace, MetaDataConstants.SuccessRedirectUrl, "Cart success redirect URL", MetaDataType.ShortString, true, false);
            JoinField(mdContext, successRedirectUrl, MetaDataConstants.ShoppingCartClass);

            var failRedirectUrl = GetOrCreateMetaField(mdContext, MetaDataConstants.OrderNamespace, MetaDataConstants.FailRedirectUrl, "Cart failure redirect URL", MetaDataType.ShortString, true, false);
            JoinField(mdContext, failRedirectUrl, MetaDataConstants.ShoppingCartClass);
        }


        public virtual MetaField GetOrCreateMetaField(MetaDataContext mdContext, string metaNamespace, string fieldName, string friendlyName, MetaDataType metadataType, bool allowNulls, bool multiLanguage)
        {
            var f = MetaField.Load(mdContext, fieldName);

            if (f == null)
            {
                f = MetaField.Create(mdContext, metaNamespace, fieldName, friendlyName, string.Empty, metadataType, 0, allowNulls, multiLanguage, false, false);
            }

            return f;
        }

        public virtual void JoinField(MetaDataContext mdContext, MetaField field, string metaClassName)
        {
            var cls = Mediachase.MetaDataPlus.Configurator.MetaClass.Load(mdContext, metaClassName);

            if (IsMetaFieldConnected(field, cls) == false)
            {
                cls.AddField(field);
            }
        }

        public static bool IsMetaFieldConnected(MetaField field, Mediachase.MetaDataPlus.Configurator.MetaClass cls)
        {
            return cls != null && cls.MetaFields.Contains(field);
        }



        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}