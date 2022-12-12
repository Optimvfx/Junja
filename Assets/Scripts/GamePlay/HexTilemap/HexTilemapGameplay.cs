using Game.GameLogic;
using Game.GameLogic.Edit;
using UnityEngine;
using System;
using Game.Core;

namespace Game.Gamplay
{
    public class HexTilemapGameplay : MonoBehaviour, IGameField, IInitable<GameLvlInfo>
    {
        private GameField _gameField;

        private bool _isInited => _gameField != null;

        public event Action<ReadOnlyGameField> EditedAllGameField;
        public event Action<ReadOnlyGameField, HexVectorInt> EditedSinglePoint;

        public void Init(GameLvlInfo lvlInfo)
        {
            if (_isInited)
                throw new ArgumentException();

            _gameField = lvlInfo.GenerateGameField();

            EditedAllGameField?.Invoke(_gameField);
        }

        public void ApplayTick()
        {
            ApplayTick(1);
        }

        public void ApplayTick(int value)
        {
            _gameField.ApplayTick(value);

            EditedAllGameField?.Invoke(_gameField);
        }

        public bool TryMove(HexVectorInt moveStartPosition, HexDirection.Direction moveDirection)
        {
            var result = _gameField.TryMove(moveStartPosition, moveDirection);

            if(result == true)
            {
                var moveTargetPosition = moveStartPosition + HexDirection.ConvertDirectionToVector(moveDirection);

                EditedSinglePoint?.Invoke(_gameField, moveStartPosition);
                EditedSinglePoint?.Invoke(_gameField, moveTargetPosition);
            }

            return result;
        }
    }
}