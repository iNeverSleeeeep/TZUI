using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using static UnityEditorInternal.ReorderableList;
using System.Collections.Generic;

namespace TZUI
{
    [CustomEditor(typeof(UIMaster))]
    [CanEditMultipleObjects]
    public class UIMasterInspector : UINodeInspector
    {

        protected new void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (targets.Length == 1)
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
                    foreach (var origin in targets)
                    {
                        AddWidgetToBinder(origin as UINode);
                        AddViewToBinder(origin as UINode);
                        LuaScriptGenerator.Generate(origin as UIMaster);
                        Save(origin as UIMaster);
                    }
                }
                finally
                {
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.Refresh();
                }

            }
        }

        private static void Save(UIMaster origin)
        {
            UIMaster master = null;
            try
            {
                master = GameObject.Instantiate((origin as UIMaster).gameObject).GetComponent<UIMaster>();
                master.gameObject.SetActive(true);
                master.gameObject.name = origin.name;
                SplitDelayLoadViews(master);
                PrefabUtility.SaveAsPrefabAsset(master.gameObject, "Assets/Resources/Output/" + master.name + ".prefab");
                PrefabUtility.SaveAsPrefabAsset((origin as UIMaster).gameObject, "Assets/Resources/" + master.name + ".prefab");
                GameObject.DestroyImmediate(master.gameObject);
            }
            finally
            {
                if (master != null)
                    GameObject.DestroyImmediate(master.gameObject);
            }
            Debug.Log("Save " + origin.name);
        }

        private static void SplitDelayLoadViews(UIMaster master)
        {
            foreach (var view in master.GetComponentsInChildren<UIView>(true))
            {
                if (view.LoadMode == UIViewLoadMode.Always)
                    continue;
                PrefabUtility.SaveAsPrefabAsset(view.gameObject, "Assets/Resources/Output/" + master.name + view.name + ".prefab");
                GameObject.DestroyImmediate(view.gameObject);
            }
        }
    }
}

