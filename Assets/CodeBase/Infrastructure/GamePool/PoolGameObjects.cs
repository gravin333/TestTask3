using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.GamePool
{
  public class PoolGameObjects : IGamePoolService
  {
    private readonly IGameObjectPoolType _gameObjectPoolType;
    private readonly List<GameObject> _list;
    private readonly int _prepareObjects;

    public PoolGameObjects(IGameObjectPoolType gameObjectPoolType, int prepareObjectsObjects = 0)
    {
      _prepareObjects = prepareObjectsObjects;
      _gameObjectPoolType = gameObjectPoolType;
      _list = new List<GameObject>();

      PrepareObjects();
    }

    public async void SetPositionAndActive(Vector3 position, GameObject parent = null)
    {
      GameObject gameObject = _list.Find(item => item.activeSelf == false);
      if (gameObject != null)
      {
        _gameObjectPoolType.SetPositionAndActive(position, gameObject, parent);
      }
      else
      {
        gameObject = await _gameObjectPoolType.CreateObject();
        _list.Add(gameObject);
        _gameObjectPoolType.SetPositionAndActive(position, gameObject, parent);
      }
    }

    private async void PrepareObjects()
    {
      for (var i = 0; i < _prepareObjects; i++)
      {
        GameObject go = await _gameObjectPoolType.CreateObject();
        _list.Add(go);
        go.SetActive(false);
      }
    }
  }
}