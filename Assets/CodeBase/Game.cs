using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.StaticData;

namespace CodeBase
{
  public class Game
  {
    public readonly GameStateMachine GameStateMachine;

    public Game(LoadScene loadScene) =>
      GameStateMachine = new GameStateMachine(new SceneLoader(), AllServices.Container, loadScene);
  }
}