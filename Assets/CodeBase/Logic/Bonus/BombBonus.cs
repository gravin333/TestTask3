using CodeBase.Logic.Ammo;
using CodeBase.Logic.Character;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public class BombBonus : BonusItem
  {
    [SerializeField] protected float forcePower = 2;
    [SerializeField] protected float explosionRadius = 3.5f;

    private void OnDrawGizmos()
    {
      Gizmos.color = new Color(.5f, .5f, .5f, .5f);
      Gizmos.DrawSphere(transform.position, explosionRadius);
    }

    protected override void SetOnEnableBonusState()
    {
      timer = time;
      timerIsStart = false;
      bonusIsActive = false;
    }

    public override void TimerStopped()
    {
      bonusIsActive = true;
      Explosion(CreateExplosionCircle());
      Destroy(gameObject);
    }

    private Collider2D[] CreateExplosionCircle()
    {
      var collider2Ds = new Collider2D[10];
      Physics2D.OverlapCircleNonAlloc(transform.position, explosionRadius, collider2Ds, LayerMask.GetMask("ArmPart", "BodyPart", "HeadPart", "LegPart"));
      if (collider2Ds[0] == null) return null;
      return collider2Ds;
    }

    public override void Hit(Vector3 moveDirection, AmmoMove ammoMove)
    {
      _rigidbody2D.velocity = Vector2.zero;
      _rigidbody2D.AddForce(moveDirection * forcePower, ForceMode2D.Impulse);
    }

    public override void OnCollision(Collision2D collision2D)
    {
      if (collision2D.gameObject.layer == LayerMask.NameToLayer("LegPart")) Explosion(CreateExplosionCircle());
    }

    private void Explosion(Collider2D[] collision2Ds)
    {
      if (collision2Ds == null || collision2Ds[0] == null)
        return;

      collision2Ds[0].gameObject.GetComponentInParent<GamePlayer>().Player.DieImmediately();

      foreach (Collider2D collision in collision2Ds)
      {
        Vector2 direction = collision.transform.position - transform.position;
        collision.GetComponent<Rigidbody2D>().AddForce(direction * 500f);
      }

      Destroy(gameObject);
    }
  }
}