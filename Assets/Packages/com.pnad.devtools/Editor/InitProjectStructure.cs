using UnityEditor;
using UnityEngine;
using System.IO;

public class InitProjectStructure
{
    // [MenuItem("Tools/Init Project Structure")]
    public static void Init()
    {
        string root = "Assets/_Project";

        string[] folders = {
            "Material",
            "Prefab/UI",
            "ScriptableObject",
            "Scripts/Base",
            "Scripts/SaveGame",
            "Scripts/ScriptableObject",
            "Scripts/Constant",
            "Scripts/Manager",
            "Scripts/Sound",
            "Scripts/UI/UIManager",
            "Scripts/UI/UIManager/LoadingUI",
            "Sprites"
        };

        foreach (string folder in folders)
        {
            CreateFolderRecursive($"{root}/{folder}");
        }

        // ===== SCENES LOGIC =====
        HandleScenesFolder(root);

        AssetDatabase.Refresh();
        Debug.Log("✅ Init Project Structure DONE");
    }

    // ==============================
    static void HandleScenesFolder(string root)
    {
        string oldScenesPath = "Assets/Scenes";
        string newScenesPath = $"{root}/Scenes";

        // Không có Assets/Scenes → chỉ cần tạo mới
        if (!AssetDatabase.IsValidFolder(oldScenesPath))
        {
            if (!AssetDatabase.IsValidFolder(newScenesPath))
            {
                AssetDatabase.CreateFolder(root, "Scenes");
            }
            Debug.Log("ℹ No Assets/Scenes found → created _Project/Scenes");
            return;
        }

        // Có Assets/Scenes
        if (!AssetDatabase.IsValidFolder(newScenesPath))
        {
            // Chưa có _Project/Scenes → move nguyên folder
            string result = AssetDatabase.MoveAsset(oldScenesPath, newScenesPath);
            if (!string.IsNullOrEmpty(result))
            {
                Debug.LogError("❌ Move Scenes failed: " + result);
            }
            else
            {
                Debug.Log("➡ Moved Assets/Scenes → _Project/Scenes");
            }
        }
        else
        {
            // ĐÃ có _Project/Scenes → move từng scene
            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { oldScenesPath });

            foreach (string guid in sceneGuids)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guid);
                string fileName = Path.GetFileName(scenePath);
                string targetPath = $"{newScenesPath}/{fileName}";

                AssetDatabase.MoveAsset(scenePath, targetPath);
            }

            // Nếu Assets/Scenes rỗng → xóa
            if (Directory.GetFiles(oldScenesPath).Length <= 1) // chỉ còn .meta
            {
                AssetDatabase.DeleteAsset(oldScenesPath);
            }

            Debug.Log("➡ Moved scene files into _Project/Scenes");
        }
    }


    static void CreateFolderRecursive(string path)
    {
        string[] parts = path.Split('/');
        string currentPath = parts[0];
        
        for (int i = 1; i < parts.Length; i++)
        {
            string newPath = $"{currentPath}/{parts[i]}";
            if (!AssetDatabase.IsValidFolder(newPath))
            {
                AssetDatabase.CreateFolder(currentPath, parts[i]);
            }
            currentPath = newPath;
        }
    }
}