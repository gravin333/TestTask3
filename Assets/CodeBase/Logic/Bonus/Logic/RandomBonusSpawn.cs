using UnityEngine;

namespace CodeBase.Logic.Bonus.Logic
{
  public class RandomBonusSpawn
  {
    private int _bonusIndex;
    private readonly GameBonus[] _gameBonus;
    private bool _waitNextBonus;
    private float _waitTimeForBonus;
    private readonly float time1 = 5;
    private readonly float time2 = 10;

    public RandomBonusSpawn(GameBonus[] gameBonus) => 
      _gameBonus = gameBonus;

    public void Tick()
    {
      if (!_waitNextBonus)
      {
        _waitTimeForBonus = (int) Random.Range(time1, time2);
        _bonusIndex = Random.Range(0, _gameBonus.Length);
        _waitNextBonus = true;
      }

      if (_waitTimeForBonus <= 0)
      {
        Spawn(_gameBonus[_bonusIndex]);
        _waitNextBonus = false;
      }
      else
      {
        _waitTimeForBonus -= Time.deltaTime;
      }
    }

    public void Spawn(GameBonus spawnBonus)
    {
      if (spawnBonus != null)
        spawnBonus.InstantiateBonus();
    }

    public void ResetLogic() => 
      _waitNextBonus = false;
  }
}