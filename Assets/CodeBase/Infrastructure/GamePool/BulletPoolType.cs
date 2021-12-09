using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Ammo;
using CodeBase.Logic.Character;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public class BulletPoolType : IGameObjectPoolType
  {
    private readonly IGameFactory _gameFactory;
    private readonly Player _player;
    private readonly ScreenSide _screenSide;

    public BulletPoolType(IGameFactory gameFactory, ScreenSide screenSide, Player player)
    {
      _player = player;
      _screenSide = screenSide;
      _gameFactory = gameFactory;
    }

    public async Task<GameObject> CreateObject()
    {
      GameObject gameObject = await _gameFactory.CreateBullet();
      return gameObject;
    }

    public void SetPositionAndActive(Vector3 position, GameObject gameObject, GameObject parent = null)
    {
      gameObject.transform.position = position;
      gameObject.GetComponent<AmmoMove>().Shot(_screenSide, _player);
      gameObject.SetActive(true);
    }
  }
}