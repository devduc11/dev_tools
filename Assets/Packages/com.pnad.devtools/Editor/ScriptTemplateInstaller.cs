using UnityEditor;
using UnityEngine;
using System.IO;

namespace PNAD.DevTools.Editor
{
    /// <summary>
    /// Copy các file Script Template từ package Runtime/ScriptTemplates/
    /// vào Assets/ScriptTemplates/ để Unity nhận diện và hiển thị
    /// trong menu Create của Project window.
    /// </summary>
    internal static class ScriptTemplateInstaller
    {
        // Thư mục nguồn trong package (embedded)
        private const string SourceRelativePath =
            "Assets/Packages/com.pnad.devtools/Runtime/ScriptTemplates";

        // Thư mục đích Unity đọc Script Templates
        private const string DestinationPath = "Assets/ScriptTemplates";

        // ----------------------------------------------------------------
        // State
        // ----------------------------------------------------------------

        // True sau khi Install() copy ít nhất 1 file mới → cần restart Unity
        public static bool NeedsRestart { get; private set; } = false;

        // ----------------------------------------------------------------
        // Entry point
        // ----------------------------------------------------------------
        public static void Install()
        {
            string sourceAbsolute = Path.GetFullPath(SourceRelativePath);

            if (!Directory.Exists(sourceAbsolute))
            {
                Debug.LogError($"[ScriptTemplateInstaller] ❌ Không tìm thấy thư mục nguồn: {sourceAbsolute}");
                return;
            }

            // Tạo Assets/ScriptTemplates nếu chưa có
            if (!AssetDatabase.IsValidFolder(DestinationPath))
            {
                AssetDatabase.CreateFolder("Assets", "ScriptTemplates");
                Debug.Log($"[ScriptTemplateInstaller] 📁 Đã tạo thư mục: {DestinationPath}");
            }

            string[] txtFiles = Directory.GetFiles(sourceAbsolute, "*.txt", SearchOption.TopDirectoryOnly);

            if (txtFiles.Length == 0)
            {
                Debug.LogWarning("[ScriptTemplateInstaller] ⚠️ Không tìm thấy file .txt nào trong thư mục nguồn.");
                return;
            }

            int copiedCount  = 0;
            int skippedCount = 0;

            foreach (string srcFile in txtFiles)
            {
                string fileName = Path.GetFileName(srcFile);
                string destFile = Path.Combine(Path.GetFullPath(DestinationPath), fileName);

                // Bỏ qua nếu file đích đã giống hệt (so sánh nội dung)
                if (File.Exists(destFile) && FilesAreIdentical(srcFile, destFile))
                {
                    skippedCount++;
                    continue;
                }

                File.Copy(srcFile, destFile, overwrite: true);
                copiedCount++;
                Debug.Log($"[ScriptTemplateInstaller] ✅ Đã copy: {fileName}");
            }

            AssetDatabase.Refresh();

            if (copiedCount > 0)
            {
                NeedsRestart = true;
            }

            Debug.Log($"[ScriptTemplateInstaller] 🎉 Hoàn tất! " +
                      $"Copy: {copiedCount} file | Bỏ qua (không đổi): {skippedCount} file");
        }

        // ----------------------------------------------------------------
        // So sánh nội dung 2 file (tránh copy thừa)
        // ----------------------------------------------------------------
        private static bool FilesAreIdentical(string path1, string path2)
        {
            byte[] bytes1 = File.ReadAllBytes(path1);
            byte[] bytes2 = File.ReadAllBytes(path2);

            if (bytes1.Length != bytes2.Length) return false;

            for (int i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i]) return false;
            }

            return true;
        }
    }
}
