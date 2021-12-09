using System;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Character
{
  public class Player
  {
    private readonly IPlayerDamage _playerDamage;
    public readonly int Lives;
    public readonly ScreenSide ScreenSidePosition;
    private int _currentLives;
    public Action ActivateShield;
    public Action<Player> Death;
    public Action Die;
    public Action ExplosionDie;
    public Action<int> LivesChanged;
    public string Name;
    public Action OnFire;
    public Action<BodyPartType> OnHit;
    public Action OnJump;

    public Player(ScreenSide screenSidePosition, int lives, IPlayerDamage playerDamage, string name)
    {
      Name = name;
      _playerDamage = playerDamage;
      Lives = lives;
      _currentLives = lives;
      ScreenSidePosition = screenSidePosition;

      OnHit += DecreaseCurrentLives;
    }

    public bool IsDead => _currentLives <= 0;

    public void DieImmediately()
    {
      _currentLives = 0;
      ExplosionDie?.Invoke();
      LivesChanged?.Invoke(_currentLives);
      Death?.Invoke(this);
    }

    public void IncreaseCurrenLives(int lives)
    {
      _currentLives = Mathf.Clamp(_currentLives + lives, 0, Lives);
      LivesChanged?.Invoke(_currentLives);
    }

    private void DecreaseCurrentLives(BodyPartType bodyPartType)
    {
      int damage = _playerDamage.Damage(bodyPartType);
      _currentLives = Mathf.Clamp(_currentLives - damage, 0, Lives);
      LivesChanged?.Invoke(_currentLives);
      CheckCurrentLivesOnDead();
    }

    private void CheckCurrentLivesOnDead()
    {
      if (IsDead)
      {
        Die?.Invoke();
        Death?.Invoke(this);
      }
    }
  }
}