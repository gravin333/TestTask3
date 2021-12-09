namespace CodeBase.Infrastructure.StateMachine
{
  public interface IBaseState : IExitableState
  {
    void Enter();
  }
}