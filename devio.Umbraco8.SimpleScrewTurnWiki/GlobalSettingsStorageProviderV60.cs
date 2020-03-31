using System;
using System.Collections.Generic;
using ScrewTurn.Wiki.Configuration;
using ScrewTurn.Wiki.PluginFramework;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public class GlobalSettingsStorageProviderV60 : IGlobalSettingsStorageProviderV60
    {
        public int LogSize => throw new NotImplementedException();

        public string CurrentWiki => throw new NotImplementedException();

        public ComponentInformation Information => throw new NotImplementedException();

        public string ConfigHelpHtml => throw new NotImplementedException();

        public void ClearLog()
        {
            throw new NotImplementedException();
        }

        public bool DeletePluginAssembly(string filename)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string ExtractWikiName(string host)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public Wiki[] GetAllWikis()
        {
            throw new NotImplementedException();
        }

        public LogEntry[] GetLogEntries()
        {
            throw new NotImplementedException();
        }

        public string GetSetting(string name)
        {
            switch(name)
            {
                case "DefaultPagesProvider": return typeof(PagesStorageProviderV60).FullName;
            }
#warning string GetSetting(string name)

            //throw new NotImplementedException();
            return name;
        }

        public void Init(IHostV50 host, string config, string wiki)
        {
#warning void Init(IHostV50 host, string config, string wiki)
            //throw new NotImplementedException();
        }

        public string[] ListPluginAssemblies()
        {
            throw new NotImplementedException();
        }

        public void LogEntry(string message, EntryType entryType, string user, string wiki)
        {
            throw new NotImplementedException();
        }

        public byte[] RetrievePluginAssembly(string filename)
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

        public bool StorePluginAssembly(string filename, byte[] assembly)
        {
            throw new NotImplementedException();
        }

        public void ValidateConfig(string config)
        {
            throw new NotImplementedException();
        }
    }
}