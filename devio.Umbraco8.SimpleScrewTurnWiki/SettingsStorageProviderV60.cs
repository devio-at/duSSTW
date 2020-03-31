using System;
using System.Collections.Generic;
using ScrewTurn.Wiki.PluginFramework;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public class SettingsStorageProviderV60 : ISettingsStorageProviderV60
    {
        AclManager _aclManager = new AclManager();

        public ScrewTurn.Wiki.AclEngine.IAclManager AclManager => _aclManager;

        public string CurrentWiki => throw new NotImplementedException();

        public ComponentInformation Information => throw new NotImplementedException();

        public string ConfigHelpHtml => throw new NotImplementedException();

        public bool AddRecentChange(string page, string title, string messageSubject, DateTime dateTime, string user, Change change, string descr)
        {
            throw new NotImplementedException();
        }

        public void BeginBulkUpdate()
        {
            throw new NotImplementedException();
        }

        public bool DeleteOutgoingLinks(string page)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void EndBulkUpdate()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string[]> GetAllOutgoingLinks()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public string GetMetaDataItem(MetaDataItem item, string tag)
        {
            throw new NotImplementedException();
        }

        public string[] GetOutgoingLinks(string page)
        {
            throw new NotImplementedException();
        }

        public string GetPluginConfiguration(string typeName)
        {
            throw new NotImplementedException();
        }

        public bool GetPluginStatus(string typeName)
        {
            throw new NotImplementedException();
        }

        public RecentChange[] GetRecentChanges()
        {
            throw new NotImplementedException();
        }

        public string GetSetting(string name)
        {
            switch(name)
            {
                case "ChangeModerationMode": return "None";
            }
#warning string GetSetting(string name)
            // throw new NotImplementedException();
            return name;
        }

        public void Init(IHostV50 host, string config, string wiki)
        {
#warning void Init(IHostV50 host, string config, string wiki)
            //throw new NotImplementedException();
        }

        public bool IsFirstApplicationStart()
        {
            throw new NotImplementedException();
        }

        public bool SetMetaDataItem(MetaDataItem item, string tag, string content)
        {
            throw new NotImplementedException();
        }

        public bool SetPluginConfiguration(string typeName, string config)
        {
            throw new NotImplementedException();
        }

        public bool SetPluginStatus(string typeName, bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool SetSetting(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetUp(IHostV50 host, string config)
        {
            throw new NotImplementedException();
        }

        public bool StoreOutgoingLinks(string page, string[] outgoingLinks)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOutgoingLinksForRename(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public void ValidateConfig(string config)
        {
            throw new NotImplementedException();
        }
    }
}