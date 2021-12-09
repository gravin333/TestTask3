using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.StateMachine
{
  public class BootstrapState : IBaseState
  {
    private readonly AllServices _allServices;
    private readonly GameStateMachine _gameStateMachine;
    private readonly LoadScene _loadScene;
    private SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine gameStateMachine, AllServices allServices, LoadScene loadScene, SceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
      _allServices = allServices;
      _loadScene = loadScene;
      _gameStateMachine = gameStateMachine;

      RegistrationServices();
    }

    public void Exit() {}

    public void Enter()
    {
      _sceneLoader.LoadScene(SceneAssetPath.Init,OnLoad);
    }

    private void OnLoad()
    {
      _gameStateMachine.Enter<LoadSceneState, LoadScene>(_loadScene);
    }

    private void RegistrationServices()
    {
      _allServices.RegisterSingle(_gameStateMachine);
      _allServices.RegisterSingle(_sceneLoader);
      _allServices.RegisterSingle<IAsset>(new AssetProvider());
      _allServices.RegisterSingle<IUIFactory>(new UIFactory(_allServices.Single<IAsset>()));
      _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAsset>()));
    }
  }
}