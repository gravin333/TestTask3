using System.Threading.Tasks;
using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.GamePool;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Bonus;
using CodeBase.Logic.Character;
using CodeBase.Logic.GameMode;
using CodeBase.PVPMode;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
  public class BuildPVPGameState : IBaseState
  {
    private readonly IGameFactory _gameFactory;
    private readonly GameStateMachine _gameStateMachine;
    private readonly IUIFactory _uiFactory;

    public BuildPVPGameState(GameStateMachine gameStateMachine, IUIFactory uiFactory, IGameFactory gameFactory)
    {
      _gameStateMachine = gameStateMachine;
      _uiFactory = uiFactory;
      _gameFactory = gameFactory;
    }

    public void Exit() {}

    public async void Enter()
    {
      CreateAndRegisterSparksPool();
      CreateAndSetGameBonuses();

      var player1 = new Player(ScreenSide.Left, 5, new SimplePlayerDamage(), "Left");
      var player2 = new Player(ScreenSide.Right, 5, new SimplePlayerDamage(), "Right");

      await CreateCharacters(player1, player2);

      var twoPlayersGame = new PVPTwoPlayersGame(player1, player2, _uiFactory, _gameFactory);

      _gameStateMachine.Enter<GameLoopState, PVPTwoPlayersGame>(twoPlayersGame);
    }

    private void CreateAndSetGameBonuses()
    {
      var findObjectOfType = Object.FindObjectOfType<SpawnBonus>();
      findObjectOfType.SetBonuses(
        new[]
        {
          new GameBonus(BonusType.AidKit, BonusAssetPath.AidKitBonus),
          new GameBonus(BonusType.Bomb, BonusAssetPath.BombBonus),
          new GameBonus(BonusType.Shield, BonusAssetPath.ShieldBonus)
        }
      );
    }

    private void CreateAndRegisterSparksPool()
    {
      var sparks = new PoolGameObjects(new BulletCollisionParticlePoolType(_gameFactory));
      AllServices.Container.RegisterSingle(sparks);
    }

    private async Task CreateCharacters(params Player[] characters)
    {
      foreach (Player character in characters)
      {
        await CreatePlayer(character);
        await CreateUILives(character);
        await CreateJumpPanel(character);
        await CreateFireButton(character);
      }
    }

    private async Task CreateFireButton(Player player)
    {
      GameObject fireButton = await _uiFactory.CreateFirePanel(player.ScreenSidePosition);
      fireButton.GetComponentInChildren<FireButton>().Construct(player);
    }

    private async Task CreateJumpPanel(Player player)
    {
      GameObject jumpPanel = await _uiFactory.CreateJumpPanel(player.ScreenSidePosition);
      jumpPanel.GetComponentInChildren<JumpPlayerPanel>().Construct(player);
    }

    private async Task CreateUILives(Player player)
    {
      GameObject livesView = await _uiFactory.CreateLives(player.ScreenSidePosition, player.Lives);
      livesView.GetComponentInChildren<UIPlayerLives>().Construct(player);
    }

    private async Task CreatePlayer(Player player)
    {
      GameObject playerObject = await _gameFactory.CreateCharacter(player.ScreenSidePosition);
      playerObject.GetComponent<GamePlayer>().Construct(player, _gameFactory);
    }
  }
}