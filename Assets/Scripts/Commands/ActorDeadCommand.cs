namespace Commands
{
    public struct ActorDeadCommand : IDeadActorCommand
    {
    }

    public interface IDeadActorCommand : ICommand { }
}
