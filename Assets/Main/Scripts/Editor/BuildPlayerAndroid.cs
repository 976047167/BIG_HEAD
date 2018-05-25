using System.IO;
using UnityEditor;

namespace Genesis.GameClient.Editor
{
    /// <summary>
    /// Android 版本打包类，目前同时用于自动打包脚本和菜单项。
    /// </summary>
    public static class BuildPlayerAndroid
    {
        private const string TargetPath = "../GameClient_Android";
        private const string ToCopyBaseDir = "Android/ToCopy";

        [MenuItem("Tools/Build/Android Development Version", false, 1010)]
        public static void BuildDev()
        {
            BuildInternal(BuildOptions.Development);
        }

        [MenuItem("Tools/Build/Android Release Version", false, 1011)]
        public static void BuildRelease()
        {
            BuildInternal(BuildOptions.None);
        }

        private static void BuildInternal(BuildOptions buildOption)
        {
            if (Directory.Exists(TargetPath))
            {
                Directory.Delete(TargetPath, true);
            }

            DirectoryInfo dir = new DirectoryInfo(TargetPath);
            var scenes = new string[] { "Assets/Lanch.unity" };
            string errorMessage = BuildPipeline.BuildPlayer(scenes, dir.FullName, BuildTarget.Android, buildOption | BuildOptions.AcceptExternalModificationsToPlayer);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return;
            }


        }

    }
}
