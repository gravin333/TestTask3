using System;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Assets;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Factory
{
  public class UIFactory : IUIFactory
  {
    private readonly IAsset _asset;


    public UIFactory(IAsset asset) => 
      _asset = asset;

    public GameObject RootUI { get; private set; }

    public async Task<GameObject> CreateRootUI()
    {
      GameObject gameObject = await _asset.Instantiate(UIAssetPath.RootCanvas);
      RootUI = gameObject;
      return gameObject;
    }

    public async Task<GameObject> CreateLives(ScreenSide screenSide, int lives)
    {
      await CreateRootCanvasIfNotExist();

      GameObject uiLives = await _asset.Instantiate(UIAssetPath.UILives, RootUI.transform);
      var playerLives = uiLives.GetComponentInChildren<UIPlayerLives>();
      playerLives.SetParameters(screenSide, lives);
      return uiLives;
    }

    public async Task<GameObject> CreateJumpPanel(ScreenSide screenSide)
    {
      await CreateRootCanvasIfNotExist();

      GameObject jumpPanel;
      switch (screenSide)
      {
        case ScreenSide.Left:
          jumpPanel = await _asset.Instantiate(UIAssetPath.UILeftJumpPanel, RootUI.transform);
          break;
        case ScreenSide.Right:
          jumpPanel = await _asset.Instantiate(UIAssetPath.UIRightJumpPanel, RootUI.transform);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(screenSide), screenSide, null);
      }

      return jumpPanel;
    }

    public async Task<GameObject> CreateFirePanel(ScreenSide screenSide)
    {
      await CreateRootCanvasIfNotExist();

      GameObject fireButton;
      switch (screenSide)
      {
        case ScreenSide.Left:
          fireButton = await _asset.Instantiate(UIAssetPath.UILeftFireButton, RootUI.transform);
          break;
        case ScreenSide.Right:
          fireButton = await _asset.Instantiate(UIAssetPath.UIRightFireButton, RootUI.transform);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(screenSide), screenSide, null);
      }

      return fireButton;
    }

    public async Task<GameObject> CreateEndGameScreen()
    {
      await CreateRootCanvasIfNotExist();
      return await _asset.Instantiate(UIAssetPath.UIEndGameScreen, RootUI.transform);
    }

    private async Task CreateRootCanvasIfNotExist()
    {
      if (RootUI == null)
        RootUI = await _asset.Instantiate(UIAssetPath.RootCanvas);
    }
  }
}