using System.Windows;
using System.Windows.Forms;

namespace KTANEAssistant.ViewModels
{
    public class SettingsPageModel : MasterPageModel
    {
        /*
         * Setting Page Properties
         */
        public bool DarkMode
        {
            get
            {
                return App.DarkMode;
            }
            set
            {
                App.DarkMode = value;
                OnPropertyChanged(nameof(DarkMode));
            }
        }
        public bool DoUpdateCheck
        {
            get
            {
                return App.DoUpdateCheck;
            }
            set
            {
                App.DoUpdateCheck = value;
                OnPropertyChanged(nameof(DoUpdateCheck));
            }
        }
        public bool DoExternalLoading
        {
            get
            {
                return App.DoExternalLoading;
            }
            set
            {
                if (!WarnedAboutCustomStuff && value)
                {
                    var dialog = ConfirmDialog.YesNoDialog("Enable Custom Modules?",
                        "If you choose to enable custom module loading, you accept that you are essentially running someone else's program on your machine.",
                        ConfirmDialog.ExclamationImagePath, ConfirmExternalLoading);
                    dialog.Owner = Window;
                    dialog.ShowDialog();
                }
                else
                {
                    App.DoExternalLoading = value;
                    OnPropertyChanged(nameof(DoExternalLoading));
                }
            }
        }
        public string ExternalLoadingPath
        {
            get
            {
                return App.ExternalLoadingPath;
            }
            set
            {
                App.ExternalLoadingPath = value;
                OnPropertyChanged(nameof(ExternalLoadingPath));
            }
        }

        private bool WarnedAboutCustomStuff;
        private void ConfirmExternalLoading(ConfirmActions callback)
        {
            if (callback.Equals(ConfirmActions.Yes))
            {
                WarnedAboutCustomStuff = true;
                DoExternalLoading = true;
            }
        }

        public Visibility CustomModuleDropdownVisibility { get; set; }
        public Visibility CustomModulePathVisibility { get; set; }
        private bool _CMSettingDropdownState = false;
        public bool CMSettingDropdownState
        {
            get
            {
                return _CMSettingDropdownState;
            }
            set
            {
                _CMSettingDropdownState = value;
                UpdateCMPathDropdown();
            }
        }
        public SimpleCommand BrowseModulesClick
        {
            get
            {
                return new SimpleCommand(OpenModulePathBrowser);
            }
        }
        internal void OpenModulePathBrowser()
        {
            var dialog = new FolderBrowserDialog
            {
                Description = "Select Custom Module Path",
                ShowNewFolderButton = true
            };
            var root = ExternalLoadingPath;
            if (string.IsNullOrEmpty(root))
            {

            }
            dialog.ShowDialog();
            if (dialog.SelectedPath != null)
            {
                ExternalLoadingPath = dialog.SelectedPath;
            }
        }

        public SettingsPageModel(MainWindow window) : base(window)
        {
            WarnedAboutCustomStuff = false;
            CustomModuleDropdownVisibility = Visibility.Collapsed;
            CustomModulePathVisibility = Visibility.Collapsed;
        }

        internal void UpdateCMPathDropdown()
        {
            if (_CMSettingDropdownState)
            {
                CustomModulePathVisibility = Visibility.Visible;
            }
            else
            {
                CustomModulePathVisibility = Visibility.Collapsed;
            }
            OnPropertyChanged(nameof(CustomModulePathVisibility));
        }
    }
}
