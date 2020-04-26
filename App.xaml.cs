using KTANEAssistant.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KTANEAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string Version { get; private set; }

        /* Start Setting Properties */
        public static bool DarkMode
        {
            get
            {
                return KTANEAssistant.Properties.Settings.Default.DarkMode;
            }
            set
            {
                KTANEAssistant.Properties.Settings.Default.DarkMode = value;
            }
        }
        public static bool DoUpdateCheck
        {
            get
            {
                return KTANEAssistant.Properties.Settings.Default.CheckForUpdatesOnStart;
            }
            set
            {
                KTANEAssistant.Properties.Settings.Default.CheckForUpdatesOnStart = value;
            }
        }
        public static bool DoExternalLoading
        {
            get
            {
                return KTANEAssistant.Properties.Settings.Default.DoExternalLoading;
            }
            set
            {
                KTANEAssistant.Properties.Settings.Default.DoExternalLoading = value;
            }
        }
        public static string ExternalLoadingPath
        {
            get
            {
                return KTANEAssistant.Properties.Settings.Default.ExternalLoadingPath;
            }
            set
            {
                KTANEAssistant.Properties.Settings.Default.ExternalLoadingPath = value;
            }
        }
        /* End Setting Properties */

        public App()
        {
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var updates = DoUpdateCheck;
            bool externalDll = DoExternalLoading;
            for (int i = 0; i < e.Args.Length; i++)
            {
                string arg = e.Args[i].ToLower();
                if (arg.Equals("--skipupdatecheck"))
                {
                    updates = false;
                }
                else if (arg.Equals("--forceupdatecheck"))
                {
                    updates = true;
                }
                else if (arg.Equals("--skipexternals"))
                {
                    externalDll = false;
                }
            }
            if (updates)
            {
                new Task(() => { Updater.Service.CheckForUpdates(); }).Start();
            }
            if (externalDll)
            {
                ModuleManager.SetProvidingCollection(LoadExternalModules());
                ModuleManager.LoadAll();
                ModuleManager.OpenRegistration();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            ModuleManager.UnloadAll();
            KTANEAssistant.Properties.Settings.Default.Save();
        }

        public static readonly DrawingBrush CheckeredBrush;

        static App()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private HashSet<ExternalModuleProvider> LoadExternalModules()
        {
            HashSet<ExternalModuleProvider> modules = new HashSet<ExternalModuleProvider>();
            if (string.IsNullOrEmpty(ExternalLoadingPath))
            {
                ExternalLoadingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "KTANE-Assistant", "CustomModules");
                try
                {
                    Directory.CreateDirectory(ExternalLoadingPath);
                }
                catch
                {
                    return modules;
                }
            }
            DirectoryInfo loadDirectory = new DirectoryInfo(ExternalLoadingPath);
            if (loadDirectory.Exists)
            {
                foreach (FileInfo file in loadDirectory.GetFiles("*.dll"))
                {
                    Console.WriteLine("Trying to inject {0}", file.Name);
                    Assembly dll = Assembly.LoadFrom(file.FullName);

                    try
                    {
                        foreach (Type type in dll.GetExportedTypes())
                        {
                            if (type.IsSubclassOf(typeof(ExternalModuleProvider)) && !type.IsAbstract)
                            {
                                var instance = Activator.CreateInstance(type);
                                modules.Add((ExternalModuleProvider)instance);
                            }
                        }
                        Console.WriteLine("Loaded data from {0}", file.Name);
                    }
                    catch
                    {
                        Console.WriteLine("Error occured while initializing {0}", file.Name);
                    }
                }
            }
            return modules;
        }
    }
}
