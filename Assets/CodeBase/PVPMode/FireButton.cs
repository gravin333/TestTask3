using CodeBase.Logic.Character;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.PVPMode
{
  public class FireButton : MonoBehaviour
  {
    public ScreenSide ScreenSide;
    private Button _button;
    private Player _character;

    private void Awake()
    {
      _button = GetComponent<Button>();

      _button.onClick.AddListener(OnFire);
    }

    private void OnDestroy() => 
      _button.onClick.RemoveListener(OnFire);

    private void OnFire() => 
      _character?.OnFire?.Invoke();

    public void Construct(Player character) => 
      _character = character;
  }
}