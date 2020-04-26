using KTANEAssistant.Pages;
using System.Windows.Controls;

namespace KTANEAssistant.API
{
    public partial class ModulePage : Page
    {
        public bool HideOutput = false;
        public DefusalPage DefusalPage { get; set; }
        public BombManager BombManager { get; set; }
        public virtual string ModuleTitle { get; set; }
    }
}
