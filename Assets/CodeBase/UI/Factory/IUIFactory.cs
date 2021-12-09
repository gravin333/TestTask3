using System.Threading.Tasks;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Factory
{
  public interface IUIFactory
  {
    GameObject RootUI { get; }
    Task<GameObject> CreateRootUI();
    Task<GameObject> CreateLives(ScreenSide screenSide, int lives);
    Task<GameObject> CreateJumpPanel(ScreenSide screenSide);
    Task<GameObject> CreateFirePanel(ScreenSide screenSide);
    Task<GameObject> CreateEndGameScreen();
  }
}