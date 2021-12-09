using System;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public class ShotParticlePoolType : IGameObjectPoolType
  {
    private readonly IGameFactory _gameFactory;
    private readonly ScreenSide _screenSide;

    public ShotParticlePoolType(IGameFactory gameFactory, ScreenSide screenSide)
    {
      _screenSide = screenSide;
      _gameFactory = gameFactory;
    }

    public async Task<GameObject> CreateObject() => 
      await _gameFactory.CreateShootParticle();

    public void SetPositionAndActive(Vector3 position, GameObject gameObject, GameObject parent = null)
    {
      switch (_screenSide)
      {
        case ScreenSide.Left:
          gameObject.transform.rotation = Quaternion.Euler(0, 90, -90);
          break;
        case ScreenSide.Right:
          gameObject.transform.rotation = Quaternion.Euler(180, 90, -90);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      gameObject.transform.position = position;
      gameObject.SetActive(true);
    }
  }
}