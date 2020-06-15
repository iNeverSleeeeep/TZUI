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
        SerializedProperty m_Binder;

        protected void OnEnable()
        {
            m_Binder = serializedObject.FindProperty("m_Binder");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_Binder);
        }

        protected void AddWidgetToBinder()
        {
            foreach (var widget in (target as UINode).GetComponentsInChildren<UIWidget>(true))
            {
                var view = widget.GetComponentInParentHard<UIView>();
                if ((view == target) || (target is UIMaster) && view == null)
                {
                    var objectsBinds = m_Binder.FindPropertyRelative("m_ObjectBinds");
                    var alreadyAdded = false;
                    for (var i = 0; i < objectsBinds.arraySize; ++i)
                    {
                        if (objectsBinds.GetArrayElementAtIndex(i).objectReferenceValue == widget)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }
                    if (alreadyAdded == false)
                    {
                        objectsBinds.arraySize++;
                        objectsBinds.GetArrayElementAtIndex(objectsBinds.arraySize - 1).objectReferenceValue = widget;
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

