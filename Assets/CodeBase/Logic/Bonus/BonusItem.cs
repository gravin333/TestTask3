using CodeBase.Logic.Ammo;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public abstract class BonusItem : MonoBehaviour
  {
    [SerializeField] protected float time = 5;
    protected Rigidbody2D _rigidbody2D;
    protected ViewBonusTimer _viewBonusTimer;
    protected bool bonusIsActive = false;
    protected float timer;
    protected bool timerIsStart;

    private void Awake()
    {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _viewBonusTimer = GetComponent<ViewBonusTimer>();
    }

    private void Update()
    {
      if (timerIsStart)
      {
        if (timer <= 0)
        {
          if (!bonusIsActive)
            TimerStopped();
        }
        else
        {
          timer -= Time.deltaTime;
        }

        UpdateView();
      }
    }

    private void OnEnable() =>
      SetOnEnableBonusState();

    private void OnCollisionEnter2D(Collision2D other)
    {
      timerIsStart = true;
      OnCollision(other);
    }

    protected abstract void SetOnEnableBonusState();

    private void UpdateView()
    {
      var seconds = (int) (timer % 60);
      _viewBonusTimer.SetTime(seconds.ToString());
    }


    public abstract void TimerStopped();
    public abstract void Hit(Vector3 direction, AmmoMove ammoMove);
    public abstract void OnCollision(Collision2D collision2D);
  }
}