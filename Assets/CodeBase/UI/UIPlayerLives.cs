using System;
using CodeBase.Logic.Character;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI
{
  public class UIPlayerLives : MonoBehaviour
  {
    [SerializeField] private GameObject HealImage;
    [SerializeField] private ScreenSide ScreenSide;
    [SerializeField] private Vector3 StartPosition = new Vector3(80, -80, 0);
    [SerializeField] private float ShiftDistance = 100;
    [SerializeField] private int defaultHealCount = 4;
    private Player _player;
    private RectTransform _rectTransform;
    private GameObject[] healImages;

    public void SetParameters(ScreenSide playerScreenSide, int characterLives)
    {
      defaultHealCount = characterLives;
      healImages = new GameObject[characterLives];
      ScreenSide = playerScreenSide;
      SetAnchorPreset();
      ClearHeals();
      InstantiateHeals();
    }

    public void Construct(Player character)
    {
      _player = character;
      _player.LivesChanged += ChangeLives;
    }

    private void ChangeLives(int lives)
    {
      for (var i = 0; i < healImages.Length; i++) healImages[i].SetActive(lives - 1 >= i);
    }

    private void SetAnchorPreset()
    {
      var rectTransform = GetComponent<RectTransform>();
      rectTransform.pivot = new Vector2(.5f, .5f);

      switch (ScreenSide)
      {
        case ScreenSide.Left:
          rectTransform.anchorMax = new Vector2(0, 1);
          rectTransform.anchorMin = new Vector2(0, 1);
          break;
        case ScreenSide.Right:
          rectTransform.anchorMax = new Vector2(1, 1);
          rectTransform.anchorMin = new Vector2(1, 1);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void InstantiateHeals()
    {
      for (var i = 0; i < defaultHealCount; i++)
      {
        GameObject image = Instantiate(HealImage, transform);
        healImages[i] = image;

        _rectTransform = image.GetComponent<RectTransform>();

        switch (ScreenSide)
        {
          case ScreenSide.Left:
            _rectTransform.localPosition = new Vector3(ShiftDistance * i + StartPosition.x, StartPosition.y, 0);
            break;
          case ScreenSide.Right:
            _rectTransform.localPosition = new Vector3(-StartPosition.x - ShiftDistance * i, StartPosition.y, 0);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void ClearHeals()
    {
      if (transform.childCount > 0)
        for (var i = 0; i < transform.childCount; i++)
          Destroy(transform.GetChild(i));
    }
  }
}