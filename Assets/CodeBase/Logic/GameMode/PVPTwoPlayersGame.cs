using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Bonus;
using CodeBase.Logic.Character;
using CodeBase.UI;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Logic.GameMode
{
  public class PVPTwoPlayersGame
  {
    private IGameFactory _gameFactory;
    private readonly Player _player1;
    private readonly Player _player2;
    private readonly IUIFactory _uiFactory;
    private readonly bool GameIsFinished = false;

    public PVPTwoPlayersGame(Player player1, Player player2, IUIFactory uiFactory, IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
      _player1 = player1;
      _player2 = player2;

      _player1.Death += ShowEndGameScreen;
      _player2.Death += ShowEndGameScreen;
    }

    private async void ShowEndGameScreen(Player player)
    {
      if (GameIsFinished)
        return;
      GameObject endGameScreen = await _uiFactory.CreateEndGameScreen();
      endGameScreen.GetComponent<EndGameWindow>().SetWinnerText(player.Equals(_player2) ? _player1 : _player2);
      Object.FindObjectOfType<SpawnBonus>().SpawnAutomatically = false;
    }
  }
}