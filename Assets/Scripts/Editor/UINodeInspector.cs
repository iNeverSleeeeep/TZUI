using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UINode))]
    public class UINodeInspector : Editor
    {
        protected void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Binder"));
        }
    }
}

