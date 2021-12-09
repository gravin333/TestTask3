using CodeBase.Logic.GameMode;

namespace CodeBase.Infrastructure.StateMachine
{
  public class GameLoopState : IPayloadState<PVPTwoPlayersGame>
  {
    private PVPTwoPlayersGame _pvpTwoPlayersGame;

    public GameLoopState(GameStateMachine gameStateMachine) {}


    public void Enter(PVPTwoPlayersGame pvpTwoPlayersGame)
    {
      _pvpTwoPlayersGame = pvpTwoPlayersGame;
    }

    public void Exit() {}
  }
}