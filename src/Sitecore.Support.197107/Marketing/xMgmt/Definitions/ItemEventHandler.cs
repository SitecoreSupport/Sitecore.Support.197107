namespace Sitecore.Support.Marketing.xMgmt.Definitions
{
    using Sitecore.Abstractions;
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
            OnItemSaving(sender, args);
        }

        public void SupportOnItemCreating(object sender, EventArgs args)
        {
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