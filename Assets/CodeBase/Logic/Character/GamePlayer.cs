using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.GamePool;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace CodeBase.Logic.Character
{
  public class GamePlayer : MonoBehaviour
  {
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int IsJump = Animator.StringToHash("IsJump");
    [SerializeField] private ScreenSide ScreenSide;
    [SerializeField] private GameObject weaponTruck;
    [SerializeField] private float jumpForce = 10;
    private readonly string _groundLayer = "Ground";
    private Rigidbody2D[] _allBodyPartsRigidbody2Ds;
    private Animator _animation;
    private IGameFactory _gameFactory;
    private HingeJoint2D[] _hingeJoint2Ds;
    private IKManager2D _ikManager2D;
    private bool _isDoubleJump = true;
    private bool _isJump = true;
    private Rigidbody2D _mainRigidbody2D;
    private PoolGameObjects _poolBloodParticle;
    private PoolGameObjects _poolGameBullets;
    private PoolGameObjects _poolGameParticle;

    public Player Player { get; private set; }

    private void Awake()
    {
      _animation = GetComponent<Animator>();
      _mainRigidbody2D = GetComponent<Rigidbody2D>();

      _allBodyPartsRigidbody2Ds = GetComponentsInChildren<Rigidbody2D>();
      _ikManager2D = GetComponent<IKManager2D>();
      _hingeJoint2Ds = GetComponentsInChildren<HingeJoint2D>();
    }

    private void OnDestroy()
    {
      Player.OnFire -= OnFire;
      Player.OnJump -= OnJump;
      Player.Die -= Die;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
      {
        _isJump = false;
        _isDoubleJump = false;
        _animation.SetBool(IsJump, false);
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer(_groundLayer))
      {
        _isJump = true;
        _isDoubleJump = false;
        _animation.SetBool(IsJump, true);
      }
    }

    public void Construct(Player player, IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
      ScreenSide = player.ScreenSidePosition;
      Player = player;
      Player.OnFire += OnFire;
      Player.OnJump += OnJump;
      Player.Die += Die;
      Player.ExplosionDie += ExplosionDie;
      Player.ActivateShield += InstantiateShield;

      _poolGameParticle = new PoolGameObjects(new ShotParticlePoolType(_gameFactory, ScreenSide));
      _poolGameBullets = new PoolGameObjects(new BulletPoolType(_gameFactory, ScreenSide, player), 10);
      _poolBloodParticle = new PoolGameObjects(new BloodParticlePoolType(_gameFactory, ScreenSide));

      SetLookDirection(player.ScreenSidePosition);
    }

    private void InstantiateShield() => 
      _gameFactory.CreateShieldEffect(transform);

    public void Alive()
    {
      transform.position = _gameFactory.GetSpawnMarker(ScreenSide).transform.position;
      JoinBodyParts();
      SetRigidbodiesParams(false, RigidbodyType2D.Kinematic);

      _animation.enabled = true;
      _ikManager2D.enabled = true;
    }

    public void Die()
    {
      SetRigidbodiesParams(true, RigidbodyType2D.Dynamic);

      _animation.enabled = false;
      _ikManager2D.enabled = false;
    }

    private void ExplosionDie()
    {
      SeparateBodyParts();
      Die();
    }

    public void HitBodyPart(BodyPartType bodyPartType, Vector2 hitPoint, Vector3 hitDirection, Rigidbody2D getComponent)
    {
      Player.OnHit.Invoke(bodyPartType);
      _poolBloodParticle.SetPositionAndActive(hitPoint, gameObject);
      if (Player.IsDead)
        getComponent.AddForce(hitDirection * 100, ForceMode2D.Impulse);
    }

    private void SetLookDirection(ScreenSide screenSide)
    {
      if (screenSide == ScreenSide.Right) gameObject.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void SetRigidbodiesParams(bool simulated, RigidbodyType2D rigidbodyType)
    {
      for (var i = 1; i < _allBodyPartsRigidbody2Ds.Length; i++)
      {
        _allBodyPartsRigidbody2Ds[i].simulated = simulated;
        _allBodyPartsRigidbody2Ds[i].bodyType = rigidbodyType;
      }
    }

    private void OnJump()
    {
      if (!_isJump)
      {
        JumpImpulse();
        return;
      }

      if (!_isDoubleJump)
      {
        ResetVelocity();
        JumpImpulse();
        _isDoubleJump = true;
      }
    }

    private void ResetVelocity() => 
      _mainRigidbody2D.velocity = Vector2.zero;

    private void JumpImpulse() => 
      _mainRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


    private void OnFire()
    {
      _animation.SetTrigger(Shoot);
      Vector3 trunkPosition = weaponTruck.transform.position;
      _poolGameParticle.SetPositionAndActive(trunkPosition);
      _poolGameBullets.SetPositionAndActive(trunkPosition);
    }

    private void SeparateBodyParts()
    {
      foreach (HingeJoint2D hingeJoint2D in _hingeJoint2Ds) hingeJoint2D.enabled = false;
    }

    private void JoinBodyParts()
    {
      foreach (HingeJoint2D hingeJoint2D in _hingeJoint2Ds) hingeJoint2D.enabled = true;
    }
  }
}