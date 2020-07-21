using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UIWidget))]
    public class UIWidgetInspector : UINodeInspector
    {
        SerializedProperty m_Binder;

        protected new void OnEnable()
        {
            base.OnEnable();
            m_Binder = serializedObject.FindProperty("m_Binder");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

