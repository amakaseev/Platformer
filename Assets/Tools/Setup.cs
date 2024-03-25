using System.IO;
using UnityEditor;
using UnityEngine;

public static class Setup {
#if UNITY_EDITOR
  static class Folders {
    public static void CreateDefault(string root, params string[] folders) {
      var fullPath = Path.Combine(Application.dataPath, root);
      foreach (var folder in folders) {
        var path = Path.Combine(fullPath, folder);
        if (!Directory.Exists(path)) {
          Directory.CreateDirectory(path);
        }
      }
    }
  }

  [MenuItem("Tools/Setup/Create Default Folders")]
  public static void CreateDefaultFolders() {
    Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "Prtefabs", "ScriptableObjects", "Scripts", "Settings");
    UnityEditor.AssetDatabase.Refresh();
  }
#endif
}
