using CodeBase.Logic.Character;
using UnityEditor;
using UnityEngine;

namespace Editor.Bonus
{
  [CustomEditor(typeof(GamePlayer))]
  [CanEditMultipleObjects]
  public class GamePlayerEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();
      
      GamePlayer gamePlayer = (GamePlayer)target;
      
      if (GUILayout.Button("Kill player"))
      {
        gamePlayer.Die();
      }
      
      if (GUILayout.Button("Alive player"))
      {
        gamePlayer.Alive();
      }
    }

  }
}