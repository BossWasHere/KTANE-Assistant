using KTANEAssistant.API;
using KTANEAssistant.ModuleData;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KTANEAssistant.Pages.Modules
{
    /// <summary>
    /// Interaction logic for KeypadsPage.xaml
    /// </summary>
    public partial class KeypadsPage : ModulePage
    {
        private KeypadModule ModuleData;

        public KeypadsPage()
        {
            InitializeComponent();
            HideOutput = true;
            ModuleData = new KeypadModule();
            ModuleData.SolutionChangedEvent += ModuleData_SolutionChangedEvent;
        }

        private void ModuleData_SolutionChangedEvent(object sender, SolutionChangedEventArgs<int[]> args)
        {
            if (args.CurrentSolution == null)
            {
                ImageA.Source = null;
                ImageB.Source = null;
                ImageC.Source = null;
                ImageD.Source = null;
                bool skip;
                if (ModuleData.OtherOptions == null)
                {
                    skip = true;
                }
                else
                {
                    skip = ModuleData.OtherOptions.Count == 0;
                }
                foreach (ListBoxItem item in MainSelection.Items)
                {
                    if (skip)
                    {
                        item.IsEnabled = true;
                    }
                    else if (ModuleData.OtherOptions.Remove((int)item.Tag))
                    {
                        item.IsEnabled = true;
                    }
                    else
                    {
                        item.IsEnabled = false;
                    }
                }
            }
            else
            {
                SetSource(ImageA, args.CurrentSolution[0]);
                SetSource(ImageB, args.CurrentSolution[1]);
                SetSource(ImageC, args.CurrentSolution[2]);
                SetSource(ImageD, args.CurrentSolution[3]);
            }
        }

        private void SetSource(Image control, int resourceId)
        {
            if (resourceId < 0) return;
            control.Source = new BitmapImage(new Uri(KeypadModule.GetSource(resourceId)));
        }

        public ObservableCollection<Control> ImageCollection
        {
            get
            {
                ObservableCollection<Control> target = new ObservableCollection<Control>();
                foreach (SymbolImage x in KeypadModule.Symbols)
                {
                    target.Add(new ListBoxItem() { Tag = x.ResourceID, Content = new Image() { ToolTip = x.FriendlyName, Source = new BitmapImage(new Uri(KeypadModule.GetSource(x.ResourceID))) } });
                }
                return target;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainSelection.SelectedItems.Count > 4)
            {
                MainSelection.UnselectAll();
            }
            else
            {
                ModuleData.Selected = new int[] { -1, -1, -1, -1};
                int i = 0;
                foreach (ListBoxItem item in MainSelection.SelectedItems)
                {
                    ModuleData.Selected[i] = (int)item.Tag;
                    i++;
                }
                ModuleSolver.SolveKeypads(ref ModuleData);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MainSelection.UnselectAll();
            ModuleData.OtherOptions.Clear();
            ModuleData.SetSolution(null);
        }
    }
}
