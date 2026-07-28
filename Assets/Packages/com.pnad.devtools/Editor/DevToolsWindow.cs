using UnityEditor;
using UnityEngine;

namespace PNAD.DevTools.Editor
{
    public class DevToolsWindow : EditorWindow
    {
        // Danh sách các tab bên cột trái
        private readonly string[] tabNames = new string[]
        {
            "Init Project",
            "Script Templates",
            "Import Package"
        };

        private int selectedTabIndex = 0;

        // Scroll position cho Tab Script Templates
        private Vector2 _scriptTemplatesScrollPos;

        // Mở cửa sổ từ Menu Bar
        [MenuItem("Tools/PNAD DevTools")]
        public static void ShowWindow()
        {
            DevToolsWindow window = GetWindow<DevToolsWindow>("PNAD DevTools");
            window.minSize = new Vector2(750, 500);
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            // ==========================================
            // CỘT BÊN TRÁI: SIDEBAR (NAVIGATION)
            // ==========================================
            EditorGUILayout.BeginVertical(GUILayout.Width(200), GUILayout.ExpandHeight(true));
            GUILayout.Space(15);

            // Tiêu đề Sidebar
            GUILayout.Label("  PNAD DevTools", EditorStyles.boldLabel);
            GUILayout.Space(10);

            // Danh sách các tab bấm chọn (Đã fix style)
            selectedTabIndex = GUILayout.SelectionGrid(
                selectedTabIndex,
                tabNames,
                1,
                "LargeButton",
                GUILayout.Height(tabNames.Length * 38)
            );

            EditorGUILayout.EndVertical();

            // Đường vạch kẻ đứng phân cách 2 cột
            Rect rect = GUILayoutUtility.GetLastRect();
            Handles.color = new Color(0.12f, 0.12f, 0.12f, 1f);
            Handles.DrawLine(new Vector3(rect.xMax, 0, 0), new Vector3(rect.xMax, position.height, 0));

            // ==========================================
            // CỘT BÊN PHẢI: NỘI DUNG TƯƠNG ỨNG
            // ==========================================
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.Space(15);

            // Tiêu đề tab hiện tại
            GUILayout.Label($"  {tabNames[selectedTabIndex]}", EditorStyles.largeLabel, GUILayout.Height(30));
            GUILayout.Space(10);

            // Nội dung chi tiết từng Tab
            switch (selectedTabIndex)
            {
                case 0:
                    DrawInitProjectTab();
                    break;
                case 1:
                    DrawScriptTemplatesTab();
                    break;
                case 2:
                    DrawImportPackageTab();
                    break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        // ------------------------------------------------------------------
        // TAB 1: INIT PROJECT
        // ------------------------------------------------------------------
        private void DrawInitProjectTab()
        {
            EditorGUILayout.HelpBox(
                "Tự động khởi tạo cấu trúc thư mục chuẩn Assets/_Project",
                MessageType.Info
            );

            GUILayout.Space(20);

            if (GUILayout.Button("🚀 Execute Init Project Structure", GUILayout.Height(45)))
            {
                // Gọi hàm Init từ class InitProjectStructure của bạn
                InitProjectStructure.Init();
            }
        }

        // ------------------------------------------------------------------
        // TAB 2: Script Templates
        // ------------------------------------------------------------------

        // Cấu trúc mô tả một script template entry
        private struct ScriptTemplateEntry
        {
            public string Label;       // Tên hiển thị trên nút
            public string RelativePath; // Đường dẫn file tương đối từ root
            public string Content;     // Nội dung template
        }

        private void DrawScriptTemplatesTab()
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
                    Content      = EditorTools.ProjectInit.Templates.UIManagerTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate LoadingUI Script",
                    RelativePath = "Scripts/UI/UIManager/LoadingUI/LoadingUI.cs",
                    Content      = EditorTools.ProjectInit.Templates.LoadingUITemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate BaseTimeScaleUI Script",
                    RelativePath = "Scripts/Base/BaseTimeScaleUI.cs",
                    Content      = EditorTools.ProjectInit.Templates.BaseTimeScaleUITemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate BaseSoundButton Script",
                    RelativePath = "Scripts/Base/BaseSoundButton.cs",
                    Content      = EditorTools.ProjectInit.Templates.BaseSoundButtonTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SaveManager Script",
                    RelativePath = "Scripts/SaveGame/SaveManager.cs",
                    Content      = EditorTools.ProjectInit.Templates.SaveManagerTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate DataSave Script",
                    RelativePath = "Scripts/SaveGame/DataSave.cs",
                    Content      = EditorTools.ProjectInit.Templates.DataSaveTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate Constant Script",
                    RelativePath = "Scripts/Constant/Constant.cs",
                    Content      = EditorTools.ProjectInit.Templates.ConstantTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate GameAction Script",
                    RelativePath = "Scripts/Constant/GameAction.cs",
                    Content      = EditorTools.ProjectInit.Templates.GameActionTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SoundManager Script",
                    RelativePath = "Scripts/Sound/SoundManager.cs",
                    Content      = EditorTools.ProjectInit.Templates.SoundManagerTemplate.GetContent(),
                },
                new ScriptTemplateEntry
                {
                    Label        = "📄 Generate SoundSO Script",
                    RelativePath = "Scripts/Sound/SoundSO.cs",
                    Content      = EditorTools.ProjectInit.Templates.SoundSOTemplate.GetContent(),
                },
            };

            EditorGUILayout.HelpBox(
                "Tạo nhanh script mẫu vào đúng cấu trúc thư mục của dự án.",
                MessageType.Info
            );

            GUILayout.Space(10);

            // ---- ScrollView bao toàn bộ danh sách nút ----
            _scriptTemplatesScrollPos = EditorGUILayout.BeginScrollView(_scriptTemplatesScrollPos);

            GUILayout.Space(10);

            foreach (ScriptTemplateEntry entry in entries)
            {
                DrawScriptButton(entry.Label, $"{root}/{entry.RelativePath}", entry.Content);
            }

            EditorGUILayout.EndScrollView();
        }

        // Helper: vẽ một nút generate và xử lý logic tạo file
        private void DrawScriptButton(string label, string fullPath, string content)
        {
            if (GUILayout.Button(label, GUILayout.Height(45)))
            {
                ScriptCreator.CreateScriptFile(fullPath, content);
                AssetDatabase.Refresh();
                string fileName = System.IO.Path.GetFileName(fullPath);
                Debug.Log($"✅ {fileName} created successfully!");
            }
        }

        // ------------------------------------------------------------------
        // TAB 3: Import Package
        // ------------------------------------------------------------------

        private void DrawImportPackageTab()
        {
            EditorGUILayout.HelpBox(
            "Import package cần thiết của dự án.",
            MessageType.Info
            );
        }
    }
}