using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UIMaster))]
    public class UIMasterInspector : Editor
    {

        private void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Binder"));

            DrawPublish();
            serializedObject.ApplyModifiedProperties();
        }

 

        private void DrawPublish()
        {
            if (GUILayout.Button("发布UI"))
            {

            }
        }
    }
}

