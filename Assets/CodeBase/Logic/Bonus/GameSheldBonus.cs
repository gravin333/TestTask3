using UnityEngine;

public class GameSheldBonus : MonoBehaviour
{
  [SerializeField] private float workingTime = 5;
  private float timer;

  private void Awake() => 
    timer = workingTime;

  private void Update()
  {
    if (timer <= 0)
      Destroy(gameObject);
    else
      timer -= Time.deltaTime;
  }
}