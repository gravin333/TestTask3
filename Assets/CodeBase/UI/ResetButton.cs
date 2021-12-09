using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    private void Awake()
    {
        _gameStateMachine = AllServices.Container.Single<GameStateMachine>();
        GetComponent<Button>().onClick.AddListener(ResetScene);
    }

    private void ResetScene()
    {
        FindObjectOfType<GameBootstrap>().StartFromBegin();
    }
}
