using System;
using ScrewTurn.Wiki.AclEngine;

namespace devio.Umbraco8.SimpleScrewTurnWiki
{
    public class AclManager : IAclManager
    {
        public int TotalEntries => 0;   //throw new NotImplementedException();

        public event EventHandler<AclChangedEventArgs> AclChanged;

        public bool DeleteEntriesForResource(string resource)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntriesForSubject(string subject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntry(string resource, string action, string subject)
        {
            throw new NotImplementedException();
        }

        public void InitializeData(AclEntry[] entries)
        {
            throw new NotImplementedException();
        }

        public bool RenameResource(string resource, string newName)
        {
            throw new NotImplementedException();
        }

        public AclEntry[] RetrieveAllEntries()
        {
            throw new NotImplementedException();
        }

        public AclEntry[] RetrieveEntriesForResource(string resource)
        {
            return new AclEntry[0];
        }

        public AclEntry[] RetrieveEntriesForSubject(string subject)
        {
            throw new NotImplementedException();
        }

        public bool StoreEntry(string resource, string action, string subject, Value value)
        {
            throw new NotImplementedException();
        }
    }
}