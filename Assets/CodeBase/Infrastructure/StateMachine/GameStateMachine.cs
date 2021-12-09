using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.StateMachine
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices allServices, LoadScene loadScene)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, allServices, loadScene,sceneLoader),
        [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader),
        [typeof(BuildPVPGameState)] = new BuildPVPGameState(this, allServices.Single<IUIFactory>(), allServices.Single<IGameFactory>()),
        [typeof(BuildPVEGameState)] = new BuildPVEGameState(this, allServices.Single<IUIFactory>(), allServices.Single<IGameFactory>()),
        [typeof(GameLoopState)] = new GameLoopState(this)
      };
    }

    public void Enter<TState>() where TState : class, IBaseState
    {
      var state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      var state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      var state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;

    public void ReloadScene()
    {
      Enter<LoadSceneState,LoadScene>(LoadScene.Init);
    }
  }
}