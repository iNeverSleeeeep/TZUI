using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TZUI
{
    [CustomEditor(typeof(AssetListWidget))]
    class AssetListWidgetInspector : UIWidgetInspector
    {
        SerializedProperty m_AssetList;
        ReorderableList m_ReorderableList;

        bool m_ShowAssetList = false;

        protected new void OnEnable()
        {
            base.OnEnable();
            var assetNames = Enum.GetNames(typeof(Asset));
            m_AssetList = serializedObject.FindProperty("m_AssetList");
            m_ReorderableList = new ReorderableList(serializedObject, m_AssetList);
            m_ReorderableList.onAddCallback = list => m_AssetList.arraySize++;
            m_ReorderableList.onRemoveCallback = list => m_AssetList.DeleteArrayElementAtIndex(list.index);
            m_ReorderableList.headerHeight = 0;
            m_ReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var currentIndex = m_AssetList.GetArrayElementAtIndex(index).intValue;
                var newIndex = EditorGUI.Popup(rect, currentIndex, assetNames);
                if (newIndex != currentIndex)
                    m_AssetList.GetArrayElementAtIndex(index).intValue = newIndex;
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var assetNames = Enum.GetNames(typeof(Asset));

            var label = string.Empty;
            for (var i = 0; i < m_AssetList.arraySize; ++i)
            {
                var index = m_AssetList.GetArrayElementAtIndex(i).intValue;
                label += assetNames[index] + " ";
            }
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            m_ShowAssetList = EditorGUILayout.Foldout(m_ShowAssetList, "资产条：" + label);
            EditorGUI.indentLevel--;
            if (m_ShowAssetList)
            {
                m_ReorderableList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
