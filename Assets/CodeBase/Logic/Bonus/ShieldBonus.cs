using CodeBase.Logic.Ammo;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public class ShieldBonus : BonusItem
  {
    protected override void SetOnEnableBonusState()
    {
      timer = time;
      timerIsStart = false;
      bonusIsActive = false;
    }

    public override void TimerStopped() => 
      gameObject.SetActive(false);

    public override void Hit(Vector3 direction, AmmoMove ammoMove)
    {
      gameObject.SetActive(false);
      ActivateByHit(ammoMove);
    }

    private void ActivateByHit(AmmoMove ammoMove) => 
      ammoMove.Player.ActivateShield?.Invoke();

    public override void OnCollision(Collision2D collision2D) {}
  }
}