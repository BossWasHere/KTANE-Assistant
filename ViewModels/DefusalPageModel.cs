using KTANEAssistant.API;
using KTANEAssistant.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KTANEAssistant.ViewModels
{
    public class DefusalPageModel : MasterPageModel
    {
        private readonly DefusalPage Page;
        public Visibility OutputVisibility { get; set; }
        public int ModuleFrameRowSpan { get; set; }
        public string ModuleTitle { get; set; }
        public string AdditionalInfo { get; set; }

        public DefusalPageModel(DefusalPage page) : base (page.window)
        {
            Page = page;
            OutputVisibility = Visibility.Hidden;
            ModuleFrameRowSpan = 2;
            RebuildCollection();
        }

        public ObservableCollection<Control> ListedModuleCollection { get; private set; }
        internal void RebuildCollection()
        {
            ListedModuleCollection = new ObservableCollection<Control>
            {
                GetAssignmentItem("Modules", 0)
            };

            foreach (IModuleModel model in ModuleManager.NativeModules)
            {
                ListedModuleCollection.Add(new ListBoxItem() { Content = model.FriendlyName, Tag = model });
            }
            
            ListedModuleCollection.Add(new Separator());
            ListedModuleCollection.Add(GetAssignmentItem("Custom Modules", 20));

            foreach (IModuleModel model in ModuleManager.CustomModules)
            {
                ListedModuleCollection.Add(new ListBoxItem() { Content = model.FriendlyName, Tag = model });
            }
        }

        private static ListBoxItem GetAssignmentItem(string name, int tag)
        {
            return new ListBoxItem() { Content = name, Tag = tag, FontWeight = FontWeights.Bold };
        }

        private Dictionary<IModuleModel, ModulePage> PageCollector = new Dictionary<IModuleModel, ModulePage>();

        private ModuleListPage ModuleListPage;
        private CustomModuleListPage CustomModuleListPage;

        /* Begin long mess of page loading */
        public void ModuleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0].GetType().Equals(typeof(ListBoxItem)))
            {
                ModulePage nextNavigate;
                object tag = (e.AddedItems[0] as ListBoxItem).Tag;
                if (tag is IModuleModel model)
                {
                    if (PageCollector.TryGetValue(model, out ModulePage page))
                    {
                        nextNavigate = page;
                    }
                    else
                    {
                        nextNavigate = model.CreateModulePage();
                        if (nextNavigate == null)
                        {
                            return;
                        }
                        nextNavigate.DefusalPage = Page;
                        nextNavigate.BombManager = Page.window.Bomb;
                        if (string.IsNullOrEmpty(nextNavigate.ModuleTitle))
                        {
                            nextNavigate.ModuleTitle = model.FriendlyName;
                        }
                        PageCollector.Add(model, nextNavigate);
                    }
                }
                else if (tag is int index)
                {
                    if (index == 20)
                    {
                        nextNavigate = InitPage(ref CustomModuleListPage);
                    }
                    else
                    {
                        nextNavigate = ModuleListPage;
                    }
                }
                else
                {
                    return;
                }
                Page.ModuleFrame.Navigate(nextNavigate);
                ModuleTitle = nextNavigate.ModuleTitle;
                OnPropertyChanged(nameof(ModuleTitle));
                if (nextNavigate.HideOutput)
                {
                    OutputVisibility = Visibility.Collapsed;
                    ModuleFrameRowSpan = 2;
                }
                else
                {
                    OutputVisibility = Visibility.Visible;
                    ModuleFrameRowSpan = 1;
                }
                OnPropertyChanged(nameof(OutputVisibility));
                OnPropertyChanged(nameof(ModuleFrameRowSpan));
            }
        }
        /* End long mess of page loading */

        public T InitPage<T>(ref T existing) where T : ModulePage, new()
        {
            if (existing == null)
            {
                existing = new T
                {
                    DefusalPage = Page,
                    BombManager = Page.window.Bomb
                };
            }
            return existing;
        }

        public void LoadDefaults()
        {
            ModuleListPage page = InitPage(ref ModuleListPage);
            page.DataContext = this;
            if (!Page.ModuleFrame.HasContent)
            {
                Page.ModuleFrame.Navigate(page);
                ModuleTitle = ModuleListPage.ModuleTitle;
                OnPropertyChanged(nameof(ModuleTitle));
            }
        }
    }
}
