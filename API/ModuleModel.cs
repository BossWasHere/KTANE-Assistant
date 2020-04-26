namespace KTANEAssistant.API
{
    public abstract class ModuleModel<T> : IModuleModel
    {
        public abstract string FriendlyName { get; }
        public virtual DependencyFlag[] Dependencies { get; }
        public abstract ModulePage CreateModulePage();

        public T Solution { get; private set; }
        public virtual void SetSolution(T solution)
        {
            T lastSolution = Solution;
            Solution = solution;
            SolutionChangedEvent?.Invoke(this, new SolutionChangedEventArgs<T> { PreviousSolution = lastSolution, CurrentSolution = solution});
        }

        public delegate void SolutionChangedEventHandler(object sender, SolutionChangedEventArgs<T> args);
        public event SolutionChangedEventHandler SolutionChangedEvent;

        public delegate void DependencyRaisedEventHandler(DependencyFlag flag);
        public event DependencyRaisedEventHandler DependencyRaisedEvent;

        public virtual void RaiseDependencyFlag(DependencyFlag flag)
        {
            DependencyRaisedEvent?.Invoke(flag);
        }
    }

    public struct SolutionChangedEventArgs<T>
    {
        public T PreviousSolution;
        public T CurrentSolution;
    }
}
