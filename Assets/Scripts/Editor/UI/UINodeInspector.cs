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

        protected static void AddWidgetToBinder(UINode node)
        {
            var so = new SerializedObject(node);
            var objectsBinds = so.FindProperty("m_Binder").FindPropertyRelative("m_ObjectBinds");
            foreach (var widget in node.GetComponentsInChildren<UIWidget>(true))
            {
                var view = widget.GetComponentInParentHard<UIView>();
                if ((view == node) || (node is UIMaster) && view == null)
                {
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
            so.ApplyModifiedProperties();
        }

        protected static void AddViewParentToBinder(UINode node)
        {
            var so = new SerializedObject(node);
            var objectsBinds = so.FindProperty("m_Binder").FindPropertyRelative("m_ObjectBinds");
            foreach (var view in node.GetComponentsInChildren<UIView>(true))
            {
                var alreadyAdded = false;
                for (var i = 0; i < objectsBinds.arraySize; ++i)
                {
                    if (objectsBinds.GetArrayElementAtIndex(i).objectReferenceValue == view.transform.parent)
                    {
                        alreadyAdded = true;
                        break;
                    }
                }
                if (alreadyAdded == false)
                {
                    objectsBinds.arraySize++;
                    objectsBinds.GetArrayElementAtIndex(objectsBinds.arraySize - 1).objectReferenceValue = view.transform.parent;
                }
            }
            so.ApplyModifiedProperties();
        }
    }
}

