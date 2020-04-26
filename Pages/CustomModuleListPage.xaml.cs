﻿using KTANEAssistant.API;
using System.Windows.Controls;

namespace KTANEAssistant.Pages
{
    /// <summary>
    /// Interaction logic for ModuleListPage.xaml
    /// </summary>
    public partial class CustomModuleListPage : ModulePage, IModuleListPage
    {
        public CustomModuleListPage()
        {
            HideOutput = true;
            InitializeComponent();
        }

        public void ModuleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DefusalPage.ModuleSelectionChanged(this, e);
                ModuleSelection.UnselectAll();
            }
        }

        public override string ModuleTitle => "Custom Modules";
    }
}
