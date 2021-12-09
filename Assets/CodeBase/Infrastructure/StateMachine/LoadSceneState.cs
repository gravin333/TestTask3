using System;
using CodeBase.Infrastructure.Assets;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.StateMachine
{
  public class LoadSceneState : IPayloadState<LoadScene>
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter(LoadScene payload)
    {
      switch (payload)
      {
        case LoadScene.Scene1:
          _sceneLoader.LoadScene(SceneAssetPath.Scene1, OnLoadedScene1);
          break;
        case LoadScene.Scene2:
          _sceneLoader.LoadScene(SceneAssetPath.Scene2, OnLoadedScene2);
          break;
        case LoadScene.MainMenu:
          break;
        case LoadScene.Init:
          _sceneLoader.LoadScene(SceneAssetPath.Init);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(payload), payload, null);
      }
    }

    public void Exit() {}

    private void OnLoadedScene1() => 
      _gameStateMachine.Enter<BuildPVPGameState>();

    private void OnLoadedScene2() => 
      _gameStateMachine.Enter<BuildPVEGameState>();
  }
}