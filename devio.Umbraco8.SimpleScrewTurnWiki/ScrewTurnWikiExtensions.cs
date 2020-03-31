using System.Web.Mvc;
using ScrewTurn.Wiki;
using ScrewTurn.Wiki.Configuration;
using ScrewTurn.Wiki.PluginFramework;
using Umbraco.Web;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public static class ScrewTurnWikiExtensions
    {
        static ScrewTurnWikiExtensions()
        {
            Collectors.InitCollectors();
            Collectors.AddProvider(typeof(SettingsStorageProviderV60), typeof(SettingsStorageProviderV60).Assembly, "", typeof(ISettingsStorageProviderV60));
            Collectors.AddProvider(typeof(PagesStorageProviderV60), typeof(PagesStorageProviderV60).Assembly, "", typeof(IPagesStorageProviderV60));
            Collectors.AddGlobalSettingsStorageProvider(typeof(GlobalSettingsStorageProviderV60), typeof(GlobalSettingsStorageProviderV60).Assembly);

            Host.Instance = new Host();
            ApplicationSettings.Instance = new ApplicationSettings();

            Exchanger.ResourceExchanger = new ResourceExchanger();
        }

        public static MvcHtmlString ScrewTurnWiki(this UmbracoHelper umbracoHelper, string markup)
        {
            // from \ScrewTurnWiki.Core\Content.cs FormattedContent.GetFormattedPageContent(string wiki, PageContent page)
            var wiki = "";
            var fullname = umbracoHelper.AssignedContentItem.Name;

            string[] linkedPages;
            string content = FormattingPipeline.FormatWithPhase1And2(wiki, 
                markup, // page.Content
                false, FormattingContext.PageContent, 
                fullname, // page.FullName
                out linkedPages);
            //page.LinkedPages = linkedPages;

            var html = FormattingPipeline.FormatWithPhase3(wiki, content, FormattingContext.PageContent, fullname); // page.FullName);
            return new MvcHtmlString(html);
        }
    }
}