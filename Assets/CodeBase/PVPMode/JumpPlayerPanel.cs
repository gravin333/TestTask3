using CodeBase.Logic.Character;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.PVPMode
{
  public class JumpPlayerPanel : MonoBehaviour, IPointerClickHandler
  {
    public ScreenSide ScreenSide;
    private Player _character;

    public void OnPointerClick(PointerEventData eventData) => 
      _character?.OnJump?.Invoke();

    public void Construct(Player character) => 
      _character = character;
  }
}