using Game.GameLogic;
using Game.GameLogic.Decorator;
using Game.GameLogic.Edit;
using TMPro;
using UnityEngine;

namespace Game.Debuging
{
    public class TilemapWorldToCellDebuger : MonoBehaviour
    {
        [SerializeField] private CanvasDecorator _canvas;
        [SerializeField] private TMP_Text _textRenderer;
        [SerializeField] private Transform _debugTarget;
        [Header("Text Debug")]
        [SerializeField] private bool _debugHex;
        [Header("Info Source")]
        [SerializeField] private HexTilemap _tileMap;

        private void Reset()
        {
            ShowCellPosition();
        }

        private void OnValidate()
        {
            ShowCellPosition();
        }

        private void OnDrawGizmos()
        {
            ShowCellPosition();
        }

        private void ShowCellPosition()
        {
            var targetPosition = _debugTarget.position;

            var globalTargetPosition = _canvas.CanvasPositionToWorldPosition(targetPosition);

            var tilePosition = _tileMap.GlobalPositionToTilePosition(globalTargetPosition);

            var hexPosition = HexTilemap.TilePositionToHexPosition(tilePosition);

            if (_debugHex)
                _textRenderer.text = hexPosition.ToString();
            else
                _textRenderer.text = tilePosition.ToString();
        }
    }
}
