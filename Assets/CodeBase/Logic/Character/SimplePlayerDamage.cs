using System;
using CodeBase.StaticData;

namespace CodeBase.Logic.Character
{
  public class SimplePlayerDamage : IPlayerDamage
  {
    public int Damage(BodyPartType bodyPartType)
    {
      switch (bodyPartType)
      {
        case BodyPartType.Head:
          return 3;
        case BodyPartType.Body:
          return 2;
        case BodyPartType.Leg:
          return 1;
        default:
          throw new ArgumentOutOfRangeException(nameof(bodyPartType), bodyPartType, null);
      }
    }
  }
}