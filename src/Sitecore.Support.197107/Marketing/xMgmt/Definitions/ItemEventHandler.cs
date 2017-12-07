namespace Sitecore.Support.Marketing.xMgmt.Definitions
{
    using Sitecore.Abstractions;
    using Sitecore.Data.Events;
    using Sitecore.Data.Items;
    using Sitecore.Events;
    using Sitecore.Marketing.Definitions.MarketingAssets.Data.ItemDb;
    using Sitecore.Marketing.xMgmt.Extensions;
    using System;
    using System.Reflection;

    internal class ItemEventHandler : Sitecore.Marketing.xMgmt.Definitions.ItemEventHandler
    {
        private BaseTemplateManager _templateManager;

        public ItemEventHandler()
        {
            _templateManager = GetBaseTemplateManager();
        }

        public void SupportOnItemSaving([NotNull] object sender, [CanBeNull] EventArgs args)
        {
            #region Added code. The bug fix
            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null || _templateManager.GetTemplate(item)
                        .InheritsFrom(Sitecore.Marketing.Definitions.MarketingAssets.WellKnownIdentifiers.TemplateIDs.MediaClassificationTemplateId.ToID())
                                && !item.IsMarketingAsset())
            {
                return;
            }
            #endregion

            OnItemSaving(sender, args);
        }

        public void SupportOnItemCreating(object sender, EventArgs args)
        {
            #region Added code. The bug fix
            var creatingArgs = Event.ExtractParameter(args, 0) as ItemCreatingEventArgs;
            if (creatingArgs == null || _templateManager.GetTemplate(creatingArgs.TemplateId, creatingArgs.Parent.Database)
                        .InheritsFrom(Sitecore.Marketing.Definitions.MarketingAssets.WellKnownIdentifiers.TemplateIDs.MediaClassificationTemplateId.ToID()))
            {
                return;
            }
            #endregion

            OnItemCreating(sender, args);
        }

        #region Supplementary code

        private BaseTemplateManager GetBaseTemplateManager()
        {
            Assembly assembly = Assembly.Load("Sitecore.Marketing.xMgmt");
            Type type = assembly.GetType("Sitecore.Marketing.xMgmt.Definitions.ItemEventHandler");
            Sitecore.Marketing.xMgmt.Definitions.ItemEventHandler obj = (Sitecore.Marketing.xMgmt.Definitions.ItemEventHandler)Activator.CreateInstance(type);
            FieldInfo info = obj.GetType().GetField("_templateManager", BindingFlags.NonPublic | BindingFlags.Instance);

            return (BaseTemplateManager)info.GetValue(this);
        }

        #endregion
    }
}