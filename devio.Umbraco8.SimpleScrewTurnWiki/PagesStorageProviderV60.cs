using System;
using ScrewTurn.Wiki.PluginFramework;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public class PagesStorageProviderV60 : IPagesStorageProviderV60
    {
        public bool ReadOnly => throw new NotImplementedException();

        public string CurrentWiki => throw new NotImplementedException();

        public ComponentInformation Information => throw new NotImplementedException();

        public string ConfigHelpHtml => throw new NotImplementedException();

        public CategoryInfo AddCategory(string nspace, string name)
        {
            throw new NotImplementedException();
        }

        public ContentTemplate AddContentTemplate(string name, string content)
        {
            throw new NotImplementedException();
        }

        public int AddMessage(string pageFullName, string username, string subject, DateTime dateTime, string body, int parent)
        {
            throw new NotImplementedException();
        }

        public NamespaceInfo AddNamespace(string name)
        {
            throw new NotImplementedException();
        }

        public NavigationPath AddNavigationPath(string nspace, string name, string[] pages)
        {
            throw new NotImplementedException();
        }

        public Snippet AddSnippet(string name, string content)
        {
            throw new NotImplementedException();
        }

        public bool BulkStoreMessages(string pageFullName, Message[] messages)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBackups(string pageFullName, int revision)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDraft(string fullName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public PageContent GetBackupContent(string fullName, int revision)
        {
            throw new NotImplementedException();
        }

        public int[] GetBackups(string fullName)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategories(NamespaceInfo nspace)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategoriesForPage(string pageFullName)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo GetCategory(string fullName)
        {
            throw new NotImplementedException();
        }

        public ContentTemplate[] GetContentTemplates()
        {
            throw new NotImplementedException();
        }

        public PageContent GetDraft(string fullName)
        {
            throw new NotImplementedException();
        }

        public int GetMessageCount(string pageFullName)
        {
            throw new NotImplementedException();
        }

        public Message[] GetMessages(string pageFullName)
        {
            throw new NotImplementedException();
        }

        public NamespaceInfo GetNamespace(string name)
        {
            throw new NotImplementedException();
        }

        public NamespaceInfo[] GetNamespaces()
        {
            throw new NotImplementedException();
        }

        public NavigationPath[] GetNavigationPaths(NamespaceInfo nspace)
        {
            throw new NotImplementedException();
        }

        public PageContent GetPage(string fullName)
        {
#warning PageContent GetPage(string fullName)
            return null;
        }

        public PageContent[] GetPages(NamespaceInfo nspace)
        {
            throw new NotImplementedException();
        }

        public Snippet[] GetSnippets()
        {
            throw new NotImplementedException();
        }

        public PageContent[] GetUncategorizedPages(NamespaceInfo nspace)
        {
            throw new NotImplementedException();
        }

        public void Init(IHostV50 host, string config, string wiki)
        {
#warning void Init(IHostV50 host, string config, string wiki)
            
        }

        public CategoryInfo MergeCategories(CategoryInfo source, CategoryInfo destination)
        {
            throw new NotImplementedException();
        }

        public ContentTemplate ModifyContentTemplate(string name, string content)
        {
            throw new NotImplementedException();
        }

        public bool ModifyMessage(string pageFullName, int id, string username, string subject, DateTime dateTime, string body)
        {
            throw new NotImplementedException();
        }

        public NavigationPath ModifyNavigationPath(NavigationPath path, string[] pages)
        {
            throw new NotImplementedException();
        }

        public Snippet ModifySnippet(string name, string content)
        {
            throw new NotImplementedException();
        }

        public PageContent MovePage(string pageFullName, NamespaceInfo destination, bool copyCategories)
        {
            throw new NotImplementedException();
        }

        public bool RebindPage(string pageFullName, string[] categories)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCategory(CategoryInfo category)
        {
            throw new NotImplementedException();
        }

        public bool RemoveContentTemplate(string name)
        {
            throw new NotImplementedException();
        }

        public bool RemoveMessage(string pageFullName, int id, bool removeReplies)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNamespace(NamespaceInfo nspace)
        {
            throw new NotImplementedException();
        }

        public bool RemoveNavigationPath(NavigationPath path)
        {
            throw new NotImplementedException();
        }

        public bool RemovePage(string pageFullName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSnippet(string name)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo RenameCategory(CategoryInfo category, string newName)
        {
            throw new NotImplementedException();
        }

        public NamespaceInfo RenameNamespace(NamespaceInfo nspace, string newName)
        {
            throw new NotImplementedException();
        }

        public PageContent RenamePage(string fullName, string newName)
        {
            throw new NotImplementedException();
        }

        public bool RollbackPage(string pageFullName, int revision)
        {
            throw new NotImplementedException();
        }

        public bool SetBackupContent(PageContent content, int revision)
        {
            throw new NotImplementedException();
        }

        public NamespaceInfo SetNamespaceDefaultPage(NamespaceInfo nspace, string pageFullName)
        {
            throw new NotImplementedException();
        }

        public PageContent SetPageContent(string nspace, string pageName, DateTime creationDateTime, string title, string username, DateTime dateTime, string comment, string content, string[] keywords, string description, SaveMode saveMode)
        {
            throw new NotImplementedException();
        }

        public void SetUp(IHostV50 host, string config)
        {
            throw new NotImplementedException();
        }

        public void ValidateConfig(string config)
        {
            throw new NotImplementedException();
        }
    }
}