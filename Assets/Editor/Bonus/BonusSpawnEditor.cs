using CodeBase.Logic.Bonus;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor.Bonus
{
  [CustomEditor(typeof(SpawnBonus))]
  public class BonusSpawnEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      SpawnBonus spawnBonus = (SpawnBonus) target;

      spawnBonus.SpawnAutomatically = (bool) EditorGUILayout.Toggle("Spawn Automatically",spawnBonus.SpawnAutomatically);
      spawnBonus.BonusType = (BonusType) EditorGUILayout.EnumPopup("Manual spawn bonus", spawnBonus.BonusType);
      if (GUILayout.Button("Spawn bonus"))
      {
        spawnBonus.Spawn();
      }
    }
  }
}