using System.Collections;
using System;
using System.Collections.Generic;
namespace LogSpy.Core.Model
{
    public abstract class ProviderCreationContext
    {
        private IList<String> creationErrors;

        protected ProviderCreationContext()
        {
            creationErrors = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            creationErrors.Add(errorMessage);
        }

        public bool WasCreated { get
        {
            return creationErrors.Count == 0;
        }}

        public IEnumerable<string> CreationErrors
        {
            get { return creationErrors; }
        }
    }
}