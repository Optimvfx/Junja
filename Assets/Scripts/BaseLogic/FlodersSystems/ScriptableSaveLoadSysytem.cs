using UnityEditor;
using UnityEngine;

namespace Game.BaseLogic.FlodersSystems
{
    public static class ScriptableSaveLoadSysytem
    {
        private const string _basePatch = "Assets/ScriptableObjects";

        public static void Save(ScriptableObject scriptableObject, string name, string folderName)
        {
            if (AssetDatabase.IsValidFolder($"{_basePatch}/{folderName}") == false)
                AssetDatabase.CreateFolder($"{_basePatch}", folderName);

            try
            {
                AssetDatabase.DeleteAsset($"{_basePatch}/{folderName}/{name}.asset");
            }
            finally
            {
                AssetDatabase.CreateAsset(scriptableObject, $"{_basePatch}/{folderName}/{name}.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
}