using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UIMaster))]
    public class UIMasterInspector : UINodeInspector
    {

        protected new void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            DrawPublish();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPublish()
        {
            if (GUILayout.Button("发布"))
            {
                AssetDatabase.StartAssetEditing();
                try
                {
                    AddWidgetToBinder();
                    LuaScriptGenerator.Generate(target as UIMaster);

                    PrefabUtility.ApplyPrefabInstance((target as UIMaster).gameObject, InteractionMode.AutomatedAction);
                }
                finally
                {
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.Refresh();
                }

            }
        }
    }
}

