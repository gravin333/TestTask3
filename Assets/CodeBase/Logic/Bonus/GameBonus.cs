using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;

namespace CodeBase.Logic.Bonus
{
  public class GameBonus
  {
    public readonly BonusType BonusType;
    private readonly string _bonusAssetPath;
    private readonly IGameFactory _gameFactory;

    public GameBonus(BonusType aidKit, string bonusAssetPath)
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();
      _bonusAssetPath = bonusAssetPath;
      BonusType = aidKit;
      LoadBonus();
    }

    private void LoadBonus() => 
      _gameFactory.LoadBonus(_bonusAssetPath);

    public void InstantiateBonus() => 
      _gameFactory.InstantiateBonus(_bonusAssetPath);
  }
}