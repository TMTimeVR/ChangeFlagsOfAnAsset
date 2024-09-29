using UnityEngine;
using UnityEditor;

public class HideFlagsEditorWindow : EditorWindow
{
    // The path to the asset
    private string assetPath = "Assets/YourAssetPath/YourAssetName.asset";
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

        // Input field for the asset path
        assetPath = EditorGUILayout.TextField("Asset Path", assetPath);

        // Button to load the asset
        if (GUILayout.Button("Load Asset"))
        {
            LoadAsset();
        }

        // If an asset is loaded, display current HideFlags and options to modify it
        if (selectedAsset != null)
        {
            GUILayout.Space(10);
            GUILayout.Label("Current Asset: " + selectedAsset.name, EditorStyles.boldLabel);
            GUILayout.Label("Current HideFlags: " + selectedAsset.hideFlags);

            // Dropdown to choose new HideFlags
            newHideFlags = (HideFlags)EditorGUILayout.EnumFlagsField("New HideFlags", newHideFlags);

            // Button to apply the changes
            if (GUILayout.Button("Apply HideFlags"))
            {
                ApplyHideFlags();
            }
        }
    }

    // Method to load the asset from the provided path
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

    // Method to apply the selected HideFlags
    void ApplyHideFlags()
    {
        if (selectedAsset != null)
        {
            selectedAsset.hideFlags = newHideFlags;

            // Mark asset as dirty to ensure changes are saved
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
