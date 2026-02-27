using UnityEngine;
using UnityEditor;

public class HideFlagsEditorWindow : EditorWindow
{
    private string assetPath = "";
    private Object selectedAsset;
    private HideFlags newHideFlags = HideFlags.None;

    [MenuItem("Tools/HideFlags Modifier")]
    public static void ShowWindow()
    {
        GetWindow<HideFlagsEditorWindow>("HideFlags Modifier");
    }

    void OnGUI()
    {
        GUILayout.Label("Modify Asset HideFlags", EditorStyles.boldLabel);
        assetPath = EditorGUILayout.TextField("Asset Path", assetPath);

        if (GUILayout.Button("Load Asset"))
        {
            LoadAsset();
        }

        if (selectedAsset != null)
        {
            GUILayout.Space(10);
            GUILayout.Label("Current Asset: " + selectedAsset.name, EditorStyles.boldLabel);
            GUILayout.Label("Current HideFlags: " + selectedAsset.hideFlags);
            newHideFlags = (HideFlags)EditorGUILayout.EnumFlagsField("New HideFlags", newHideFlags);
            if (GUILayout.Button("Apply HideFlags"))
            {
                ApplyHideFlags();
            }
        }
    }
    
    void LoadAsset()
    {
        selectedAsset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        if (selectedAsset != null)
        {
            Debug.Log("Asset Loaded: " + selectedAsset.name);
        }
        else
        {
            Debug.LogError("Asset not found at path: " + assetPath);
        }
    }

    void ApplyHideFlags()
    {
        if (selectedAsset != null)
        {
            selectedAsset.hideFlags = newHideFlags;
            EditorUtility.SetDirty(selectedAsset);
            AssetDatabase.SaveAssets();

            Debug.Log("HideFlags changed to: " + newHideFlags);
        }
        else
        {
            Debug.LogError("No asset selected. Please load an asset first.");
        }
    }
}
