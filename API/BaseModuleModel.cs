using KTANEAssistant.Pages.Modules;

namespace KTANEAssistant.API
{
    public interface IModuleModel
    {
        string FriendlyName { get; }
        DependencyFlag[] Dependencies { get; }
        ModulePage CreateModulePage();
    }

    public interface INativeModuleModel : IModuleModel
    {
        string PageDescriptor { get; }
    }

    public interface ICustomModuleModel : IModuleModel
    {
        ExternalModuleProvider Provider { get; }
        string ModName { get; }
        string ModAuthor { get; }
    }
}
