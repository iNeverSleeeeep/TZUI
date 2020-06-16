using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UIView))]
    public class UIViewInspector : UINodeInspector
    {
        SerializedProperty m_LoadMode;

        protected new void OnEnable()
        {
            base.OnEnable();

            m_LoadMode = serializedObject.FindProperty("m_LoadMode");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_LoadMode);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

