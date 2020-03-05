using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelObstacle))]
    [CanEditMultipleObjects]
    public class LevelItemEditor : UnityEditor.Editor
    {
        private LevelObstacle LevelObstacle => target as LevelObstacle;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();
            
            if (!LevelObstacle.IsNormalLevel())
            {
                GUILayout.Label("Image is un supported");
            }
            
            GUILayout.Label($"Elements : {LevelObstacle.ItemsCount()}");
            
            EditorGUILayout.EndVertical();
        }
    }
}