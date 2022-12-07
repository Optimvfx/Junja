using Game.BaseLogic.FlodersSystems;
using Game.GameLogic.Edit;
using UnityEditor;
using UnityEngine;

namespace Game.Editors
{
    [CustomEditor(typeof(GameLvlCreator))]
    public class GameLvlCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameLvlCreator gameLvlCreator = (target as GameLvlCreator);

            if (GUILayout.Button("Create Game Field"))
            {
                var gameLvlInfo = gameLvlCreator.CalculateGameLvlInfo();

                ScriptableSaveLoadSysytem.Save(gameLvlInfo, $"{gameLvlInfo.NewLvlName} Game Lvl", "Lvls");

                Debug.Log("Game Lvl Created");
            }
        }
    }
}