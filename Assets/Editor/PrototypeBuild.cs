using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class PrototypeBuild
{
    [MenuItem("DeepJam/Build WebGL Prototype")]
    public static void WebGL()
    {
        PlayerSettings.companyName = "Yagmur Cem Gul";
        PlayerSettings.productName = "My Awesome Game";
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = false;
        PlayerSettings.WebGL.initialMemorySize = 128;
        PlayerSettings.WebGL.template = "PROJECT:DeepJam";
        PlayerSettings.defaultWebScreenWidth = 1280;
        PlayerSettings.defaultWebScreenHeight = 720;
        Build(BuildTarget.WebGL, "Builds/WebGL");
    }

    [MenuItem("DeepJam/Build Mac Prototype")]
    public static void Mac() { Build(BuildTarget.StandaloneOSX, "Builds/Mac/My Awesome Game.app"); }
    [MenuItem("DeepJam/Build Windows Prototype")]
    public static void Windows() { Build(BuildTarget.StandaloneWindows64, "Builds/Windows/My Awesome Game.exe"); }

    private static void Build(BuildTarget target, string output)
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        if (scenes.Length != 5 || !scenes[0].EndsWith("start.unity")) throw new System.Exception("Expected the five original prototype scenes.");
        ValidateScenes(scenes);
        output = Path.GetFullPath(Path.Combine(Application.dataPath, "..", output));
        Directory.CreateDirectory(Path.GetDirectoryName(output));
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions { scenes = scenes, locationPathName = output, target = target, options = BuildOptions.None });
        if (report.summary.result != BuildResult.Succeeded) throw new System.Exception("Prototype build failed: " + report.summary.result);
        Debug.Log("PROTOTYPE_BUILD_SUCCEEDED: " + output + " (" + report.summary.totalSize + " bytes)");
    }

    private static void ValidateScenes(string[] scenes)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            var scene = EditorSceneManager.OpenScene(scenes[i]);
            foreach (var root in scene.GetRootGameObjects())
            foreach (var transform in root.GetComponentsInChildren<Transform>(true))
                if (GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(transform.gameObject) != 0)
                    throw new System.Exception("Missing script in " + scenes[i] + ": " + transform.name);
            if (i == 1 || i == 2)
            {
                var hero = GameObject.FindGameObjectWithTag("Player");
                if (hero == null || hero.GetComponent<Rigidbody2D>() == null || hero.GetComponent<characterController2>() == null)
                    throw new System.Exception("Missing playable character in " + scenes[i]);
                var points = hero.GetComponent<PlayerPoints>();
                if (points == null || new SerializedObject(points).FindProperty("_text").objectReferenceValue == null)
                    throw new System.Exception("Missing score UI reference in " + scenes[i]);
                if (Object.FindObjectsByType<NextLevel>(FindObjectsSortMode.None).Length == 0)
                    throw new System.Exception("Missing exit in " + scenes[i]);
            }
            Debug.Log("PROTOTYPE_SCENE_VALIDATED: " + scenes[i]);
        }
        EditorSceneManager.OpenScene(scenes[0]);
    }
}
