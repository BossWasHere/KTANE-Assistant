using KTANEAssistant.ModuleData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KTANEAssistant.API
{
    public static class ModuleManager
    {
        public static IReadOnlyCollection<ExternalModuleProvider> ExternalProviders
        {
            get
            {
                return _ExternalProviders;
            }
        }
        public static ReadOnlyObservableCollection<INativeModuleModel> NativeModules
        {
            get
            {
                return new ReadOnlyObservableCollection<INativeModuleModel>(_NativeModules);
            }
        }
        public static ReadOnlyObservableCollection<ICustomModuleModel> CustomModules
        {
            get
            {
                return new ReadOnlyObservableCollection<ICustomModuleModel>(_CustomModules);
            }
        }

        private static bool AcceptingRegistrations { get; set; }
        private static HashSet<ExternalModuleProvider> _ExternalProviders;
        private static ObservableCollection<INativeModuleModel> _NativeModules = new ObservableCollection<INativeModuleModel>();
        private static ObservableCollection<ICustomModuleModel> _CustomModules = new ObservableCollection<ICustomModuleModel>();

        static ModuleManager()
        {
            _NativeModules.Add(new SimpleWiresModule());
            _NativeModules.Add(new ButtonModule());
            _NativeModules.Add(new KeypadModule());
            _NativeModules.Add(new SimonSaysModule());
            _NativeModules.Add(new WhoIsModule());
            _NativeModules.Add(new MemoryModule());
            _NativeModules.Add(new MorseCodeModule());
            _NativeModules.Add(new ComplicatedWiresModule());
            _NativeModules.Add(new WireSequencesModule());
            _NativeModules.Add(new MazeModule());
            _NativeModules.Add(new PasswordModule());
            _NativeModules.Add(new KnobsModule());
        }

        internal static void SetProvidingCollection(HashSet<ExternalModuleProvider> providers)
        {
            _ExternalProviders = providers;
        }

        internal static void LoadAll()
        {
            if (_ExternalProviders == null) return;
            Parallel.ForEach(_ExternalProviders, x =>
            {
                x.Loaded();
            });
        }

        internal static void OpenRegistration()
        {
            if (_ExternalProviders == null) return;
            AcceptingRegistrations = true;
            foreach (var m in _ExternalProviders)
            {
                m.RegistrationOpened();
            }
            AcceptingRegistrations = false;
        }

        internal static void UnloadAll()
        {
            if (_ExternalProviders == null) return;
            Parallel.ForEach(_ExternalProviders, x =>
            {
                x.Unloaded();
            });
        }

        public static void RegisterModule(ICustomModuleModel customModule)
        {
            if (AcceptingRegistrations)
            {
                _CustomModules.Add(customModule);
            }
            else
            {
                throw new InvalidOperationException("Module Manager is not currently accepting registrations");
            }
        }
    }
}
