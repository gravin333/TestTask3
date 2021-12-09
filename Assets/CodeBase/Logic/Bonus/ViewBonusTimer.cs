using TMPro;
using UnityEngine;

namespace CodeBase.Logic.Bonus
{
  public class ViewBonusTimer : MonoBehaviour
  {
    [SerializeField] private Transform timerObject;
    [SerializeField] private TextMeshProUGUI timerText;

    private Quaternion timerRotation;

    private void Awake() => 
      timerRotation = timerObject.rotation;

    private void LateUpdate() => 
      timerObject.transform.rotation = timerRotation;

    public void SetTime(string time) => 
      timerText.text = time;
  }
}