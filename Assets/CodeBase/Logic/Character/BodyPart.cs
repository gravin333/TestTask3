using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.Character
{
  public class BodyPart : MonoBehaviour
  {
    public BodyPartType BodyPartType;
    public GamePlayer GamePlayer { get; private set; }

    private void Awake() => 
      GamePlayer = GetComponentInParent<GamePlayer>();
  }
}