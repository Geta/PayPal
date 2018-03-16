using System.Security.Cryptography;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Logging;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;
using Mediachase.MetaDataPlus;
using Mediachase.MetaDataPlus.Configurator;

namespace Geta.Commerce.Payments.PayPal
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    public class MetadataInitialization : IInitializableModule
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(MetadataInitialization));

        public void Initialize(InitializationEngine context)
        {
            MetaDataContext mdContext = CatalogContext.MetaDataContext;
            var paypalPayment = GetOrCreateMetaClass(mdContext, MetadataConstants.UsersNameSpace,MetadataConstants.PayPalClassName, "PayPal payment",MetadataConstants.PayPalTableName, MetadataConstants.OrderFormPaymentClass);

            var expToken = GetOrCreateMetaField(mdContext, MetadataConstants.PayPalExpToken, "PayPal Exp token", MetaDataType.ShortString);
            JoinField(mdContext, expToken, MetadataConstants.PayPalClassName);

            var ordernumber = GetOrCreateMetaField(mdContext, MetadataConstants.PayPalOrderNumber, "PayPal order number", MetaDataType.ShortString);
            JoinField(mdContext, ordernumber, MetadataConstants.PayPalClassName);
        }

        private MetaClass GetOrCreateMetaClass(MetaDataContext mdContext, string metaNamespace, string className, string friendlyName, string tableName, string parentClass)
        {
            var mc = MetaClass.Load(mdContext,className);
            if (mc == null)
            {
                Logger.Debug(string.Format("Creating MetaClass '{0}' for Paypal payment integration.", className));
                var parent = MetaClass.Load(mdContext, parentClass);
                mc = MetaClass.Create(mdContext, metaNamespace, className, friendlyName, tableName, parent, false, string.Empty);
            }

            return mc;
        }

        private MetaField GetOrCreateMetaField(MetaDataContext mdContext, string fieldName, string friendlyName, MetaDataType metadataType)
        {
            var f = MetaField.Load(mdContext, fieldName);

            if (f == null)
            {
                Logger.Debug(string.Format("Adding meta field '{0}' for Paypal payment integration.", fieldName));
                f = MetaField.Create(mdContext, MetadataConstants.OrderNamespace, fieldName, friendlyName, string.Empty, metadataType, 0, true, false, false, false);
            }

            return f;
        }

        private void JoinField(MetaDataContext mdContext, MetaField field, string metaClassName)
        {
            var cls = MetaClass.Load(mdContext, metaClassName);

            if (MetaFieldIsNotConnected(field, cls))
            {
                cls.AddField(field);
            }
        }

        private static bool MetaFieldIsNotConnected(MetaField field, MetaClass cls)
        {
            return cls != null && !cls.MetaFields.Contains(field);
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}