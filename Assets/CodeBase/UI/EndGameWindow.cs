using CodeBase.Logic.Character;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
  public class EndGameWindow : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI textWinner;

    public void SetWinnerText(Player player) => 
      textWinner.text = $"{player.Name} player win!!!";
  }
}