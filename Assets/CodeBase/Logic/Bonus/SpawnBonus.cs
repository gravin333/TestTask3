using System.Linq;
using CodeBase.Logic.Bonus.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public class SpawnBonus : MonoBehaviour
  {
    [HideInInspector] public BonusType BonusType;
    [HideInInspector] public bool SpawnAutomatically = true;
    [HideInInspector] public bool GameFinished;
    private GameBonus[] _gameBonus;
    private RandomBonusSpawn _randomBonusSpawn;

    private void Update()
    {
      if (SpawnAutomatically && !GameFinished)
        _randomBonusSpawn.Tick();
    }

    public void ResetSpawner() => 
      _randomBonusSpawn.ResetLogic();

    public void Spawn()
    {
      GameBonus spawnBonus = _gameBonus.First(item => item.BonusType == BonusType);
      _randomBonusSpawn.Spawn(spawnBonus);
    }

    public void SetBonuses(GameBonus[] gameBonus)
    {
      _gameBonus = gameBonus;
      _randomBonusSpawn = new RandomBonusSpawn(_gameBonus);
    }
  }
}