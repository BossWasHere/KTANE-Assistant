using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANEAssistant.API
{
    public abstract class ExternalModuleProvider
    {
        public abstract string ProjectAuthor { get; }
        public abstract string ProjectAuthorWebsite { get; }

        public abstract void Loaded();
        public abstract void RegistrationOpened();
        public abstract void Unloaded();
    }
}
