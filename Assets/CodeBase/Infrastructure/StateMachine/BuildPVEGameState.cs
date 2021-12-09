using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.StateMachine
{
  public class BuildPVEGameState : IBaseState
  {
    public BuildPVEGameState(GameStateMachine gameStateMachine, IUIFactory single, IGameFactory gameFactory) {}

    public void Exit() {}

    public void Enter() {}
  }
}