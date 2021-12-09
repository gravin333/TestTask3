using CodeBase.Infrastructure.StateMachine;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  // Entrypoint class
  public class GameBootstrap : MonoBehaviour
  {
    private Game _game;
    private LoadScene _loadScene;

    private void Awake()
    {
      _game = new Game(_loadScene);
      _game.GameStateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }

    public void StartFromBegin()
    {
      _game = new Game(_loadScene);
      _game.GameStateMachine.Enter<BootstrapState>();
    }

    public void Load(LoadScene loadScene) => 
      _loadScene = loadScene;
    
  }
}