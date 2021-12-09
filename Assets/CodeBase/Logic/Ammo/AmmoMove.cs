using System;
using CodeBase.Infrastructure.GamePool;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Bonus;
using CodeBase.Logic.Character;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Ammo
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class AmmoMove : MonoBehaviour
  {
    [SerializeField] private float speed = 10;
    [SerializeField] private Vector3 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private PoolGameObjects _sparksParticlePool;
    private SpriteRenderer _sprite;
    private bool isTrigged;
    private bool lateСollision;

    public Player Player { get; private set; }

    private void Awake()
    {
      _sparksParticlePool = AllServices.Container.Single<PoolGameObjects>();
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable() => 
      lateСollision = false;

    private void OnBecameInvisible() => 
      gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!isTrigged)
      {
        CheckShieldAndDestroy(other);
        CheckBodyCollideAndHitBody(other);
        CheckAnotherAmmoCollideAndCreateParticle(other);
        CheckBonusAndHit(other);
      }
    }

    private void CheckShieldAndDestroy(Collider2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("Shield"))
      {
        var player = other.gameObject.GetComponentInParent<GamePlayer>();
        if (!player.Player.Equals(Player)) gameObject.SetActive(false);
      }
    }

    private void CheckBonusAndHit(Collider2D other)
    {
      var bonusItem = other.GetComponentInParent<BonusItem>();
      if (bonusItem == null) return;
      bonusItem.Hit(_moveDirection, this);
      gameObject.SetActive(false);
    }

    private void CheckAnotherAmmoCollideAndCreateParticle(Collider2D other)
    {
      var ammoMove = other.GetComponent<AmmoMove>();
      if (ammoMove == null) return;
      if (lateСollision == false)
      {
        other.GetComponent<AmmoMove>().lateСollision = true;
        _sparksParticlePool.SetPositionAndActive(transform.position);
      }

      gameObject.SetActive(false);
    }

    private void CheckBodyCollideAndHitBody(Collider2D other)
    {
      var bodyPart = other.GetComponent<BodyPart>();
      if (bodyPart != null)
      {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
        if (hit.collider != null)
          bodyPart.GamePlayer.HitBodyPart(bodyPart.BodyPartType, hit.point, _moveDirection, other.GetComponent<Rigidbody2D>());

        gameObject.SetActive(false);
      }
    }

    public void Shot(ScreenSide screenSide, Player player)
    {
      Player = player;
      isTrigged = false;
      switch (screenSide)
      {
        case ScreenSide.Left:
          _moveDirection = Vector3.right;
          SetSpriteLeft();
          break;
        case ScreenSide.Right:
          _moveDirection = Vector3.left;
          SetSpriteRight();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(screenSide), screenSide, null);
      }

      gameObject.SetActive(true);
      Shoot();
    }

    private void SetSpriteRight() => 
      _sprite.flipX = true;

    private void SetSpriteLeft() => 
      _sprite.flipX = false;

    private void Shoot() => 
      _rigidbody2D.AddForce(_moveDirection * speed, ForceMode2D.Impulse);
  }
}