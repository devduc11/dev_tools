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
        private void DrawScriptTemplatesTab()
        {
            ScriptTemplatesTab.Draw();
        }

        // ------------------------------------------------------------------
        // TAB 3: Import Package
        // ------------------------------------------------------------------
        private void DrawImportPackageTab()
        {
            ImportPackageTab.Draw();
        }
    }
}