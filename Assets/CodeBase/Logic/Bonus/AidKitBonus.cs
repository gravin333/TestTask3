using CodeBase.Logic.Ammo;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public class AidKitBonus : BonusItem
  {
    [SerializeField] private int durability = 10;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private int healValue = 3;
    private int _currentDurability;

    protected override void SetOnEnableBonusState()
    {
      timer = time;
      timerIsStart = false;
      bonusIsActive = false;
      _currentDurability = durability;
    }

    public override void TimerStopped() => 
      Destroy(gameObject);

    public override void Hit(Vector3 direction, AmmoMove ammoMove)
    {
      _currentDurability--;
      float progress = (float) _currentDurability / durability;
      _progressBar.SetProgress(progress);
      if (_currentDurability <= 0)
        ActivateByHit(ammoMove);
    }

    private void ActivateByHit(AmmoMove ammoMove)
    {
      ammoMove.Player.IncreaseCurrenLives(healValue);
      Destroy(gameObject);
    }

    public override void OnCollision(Collision2D collision2D) {}
  }
}