using Game.Core;
using Game.GameLogic.Edit;
using UnityEngine;

namespace Game.Gamplay
{
    public class StandartGameIniter : GameIniter
    {
        [SerializeField] private GameLvlInfo _gameLvlInfo;

        private void Awake()
        {
            Init(_gameLvlInfo);
        }
    }
}