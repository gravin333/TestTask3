using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
  public class ProgressBar : MonoBehaviour
  {
    [SerializeField] private Image progressImage;

    public void SetProgress(float progress) => 
      progressImage.fillAmount = progress;
  }
}