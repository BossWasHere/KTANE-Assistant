using KTANEAssistant.ModuleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTANEAssistant.API
{
    public abstract class CustomModuleModel<T> : ModuleModel<T>, ICustomModuleModel
    {
        public abstract ExternalModuleProvider Provider { get; }
        public abstract string ModName { get; }
        public abstract string ModAuthor { get; }
    }
}
