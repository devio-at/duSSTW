using ScrewTurn.Wiki;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public class ResourceExchanger : IResourceExchanger
    {
        public string GetResource(string name)
        {
            return "[res: name]";
        }
    }
}