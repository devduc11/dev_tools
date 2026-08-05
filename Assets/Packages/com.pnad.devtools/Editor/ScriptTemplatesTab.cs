using UnityEditor;
using UnityEngine;

namespace PNAD.DevTools.Editor
{
    /// <summary>
    /// Quản lý toàn bộ UI và logic cho Tab "Script Templates".
    /// Được gọi từ <see cref="DevToolsWindow"/>.
    /// </summary>
    internal static class ScriptTemplatesTab
    {
        // ----------------------------------------------------------------
        // Cấu trúc mô tả một script template entry
        // ----------------------------------------------------------------
        private struct ScriptTemplateEntry
        {
            public string Label;        // Tên hiển thị trên nút
            public string RelativePath; // Đường dẫn file tương đối từ root
            public string Content;      // Nội dung template
        }

        // ----------------------------------------------------------------
        // State
        // ----------------------------------------------------------------
        private static Vector2 _scrollPos;

        // ----------------------------------------------------------------
        // Entry point: được gọi mỗi frame từ DevToolsWindow.OnGUI()
        // ----------------------------------------------------------------
        public static void Draw()
        {
            const string root = "Assets/_Project";

            // ---- Danh sách các template cần generate ----
            // Thêm entry mới vào đây để tự động tạo thêm nút
            ScriptTemplateEntry[] entries =
            {
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate UIManager Script",
                    RelativePath = "Scripts/UI/UIManager/UIManager.cs",
                    Content      = TemplateLoader.Load("UIManager.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate LoadingUI Script",
                    RelativePath = "Scripts/UI/UIManager/LoadingUI/LoadingUI.cs",
                    Content      = TemplateLoader.Load("LoadingUI.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate BaseTimeScaleUI Script",
                    RelativePath = "Scripts/Base/BaseTimeScaleUI.cs",
                    Content      = TemplateLoader.Load("BaseTimeScaleUI.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate BaseSoundButton Script",
                    RelativePath = "Scripts/Base/BaseSoundButton.cs",
                    Content      = TemplateLoader.Load("BaseSoundButton.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SaveManager Script",
                    RelativePath = "Scripts/SaveGame/SaveManager.cs",
                    Content      = TemplateLoader.Load("SaveManager.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate DataSave Script",
                    RelativePath = "Scripts/SaveGame/DataSave.cs",
                    Content      = TemplateLoader.Load("DataSave.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate Constant Script",
                    RelativePath = "Scripts/Constant/Constant.cs",
                    Content      = TemplateLoader.Load("Constant.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate GameAction Script",
                    RelativePath = "Scripts/Constant/GameAction.cs",
                    Content      = TemplateLoader.Load("GameAction.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SoundManager Script",
                    RelativePath = "Scripts/Sound/SoundManager.cs",
                    Content      = TemplateLoader.Load("SoundManager.cs.txt"),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SoundSO Script",
                    RelativePath = "Scripts/Sound/SoundSO.cs",
                    Content      = TemplateLoader.Load("SoundSO.cs.txt"),
                },
            };

            EditorGUILayout.HelpBox(
                "Tạo nhanh script mẫu vào đúng cấu trúc thư mục của dự án.",
                MessageType.Info
            );

            GUILayout.Space(10);

            // ---- ScrollView bao toàn bộ danh sách nút ----
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            GUILayout.Space(10);

            foreach (ScriptTemplateEntry entry in entries)
            {
                DrawScriptButton(entry.Label, $"{root}/{entry.RelativePath}", entry.Content);
            }

            EditorGUILayout.EndScrollView();
        }

        // ----------------------------------------------------------------
        // Helper: vẽ một nút generate và xử lý logic tạo file
        // ----------------------------------------------------------------
        private static void DrawScriptButton(string label, string fullPath, string content)
        {
            if (GUILayout.Button(label, GUILayout.Height(45)))
            {
                ScriptCreator.CreateScriptFile(fullPath, content);
                AssetDatabase.Refresh();
                string fileName = System.IO.Path.GetFileName(fullPath);
                Debug.Log($"✅ {fileName} created successfully!");
            }
        }
    }
}
