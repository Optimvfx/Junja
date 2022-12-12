using Game.Core;
using Game.GameLogic;
using System;
using UnityEngine;

namespace Game.Gamplay
{
    public class HexTilemapChangmentRenderer : MonoBehaviour, IInitable<HexTilemapGameplay, HexTilemapRenderer>
    {
        private HexTilemapGameplay _hexTilemapGameplay;
        private HexTilemapRenderer _hexTilemapRenderer;

        private bool _isInited => _hexTilemapGameplay != null && _hexTilemapRenderer != null;

        private bool _isSubscribed = false;

        private void OnEnable()
        {
            TrySubscribe();
        }

        private void OnDisable()
        {
            TryUnSubscribe();
        }

        public void Init(HexTilemapGameplay hexTilemapGameplay, HexTilemapRenderer hexTilemapRenderer)
        {
            if (_isInited)
                throw new ArgumentException();

            _hexTilemapGameplay = hexTilemapGameplay;
            _hexTilemapRenderer = hexTilemapRenderer;

            TrySubscribe();
        }

        private void TrySubscribe()
        {
            if (_isInited == false || _isSubscribed)
                return;

            _hexTilemapGameplay.EditedAllGameField += EditedAllGameField;
            _hexTilemapGameplay.EditedSinglePoint += EditedSinglePoint;

            _isSubscribed = true;
        }

        private void TryUnSubscribe()
        {
            if (_isInited == false || _isSubscribed == false)
                return;

            _hexTilemapGameplay.EditedAllGameField -= EditedAllGameField;
            _hexTilemapGameplay.EditedSinglePoint -= EditedSinglePoint;

            _isSubscribed = false;
        }

        private void EditedAllGameField(ReadOnlyGameField gameField)
        {
            _hexTilemapRenderer.RenderAllPoints(gameField);
        }

        private void EditedSinglePoint(ReadOnlyGameField gameField, HexVectorInt position)
        {
            _hexTilemapRenderer.RenderSinglePoint(gameField, position);
        }
    }
}