using Game.Core;
using Game.GameLogic;
using Game.GameLogic.Edit;
using UnityEngine;

namespace Game.Debuging
{
    public class HexAxisRenderer : MonoBehaviour
    {
        [SerializeField] private HexTilemap _tileMap;
        [Header("Render")]
        [SerializeField] private Color _AxixColorS = Color.green;
        [SerializeField] private Color _AxixColorQ = new Color(230, 230, 250);
        [SerializeField] private Color _AxixColorR = Color.red;
        [SerializeField] private UFloat _drawDistance;

        private void OnDrawGizmos()
        {
            DrawAxises();
        }

        private void DrawAxises()
        {
            DrawAxis(_AxixColorR, HexDirection.Direction.PositiveR);
            DrawAxis(_AxixColorS, HexDirection.Direction.PositiveS);
            DrawAxis(_AxixColorQ, HexDirection.Direction.PositiveQ);

            DrawAxis(_AxixColorR - Color.gray, HexDirection.Direction.NegativeR);
            DrawAxis(_AxixColorS - Color.gray, HexDirection.Direction.NegativeS);
            DrawAxis(_AxixColorQ - Color.gray, HexDirection.Direction.NegativeQ);
        }

        private void DrawAxis(Color color, HexDirection.Direction direction)
        {
            const float _standartAlpha = 1;

            color.a = _standartAlpha;

            if (_tileMap == null)
                return;

            Gizmos.color = color;

            var hexDrawDirection = HexDirection.ConvertDirectionToVector(direction);

            var drawDirection = (_tileMap.HexTilePositionToGlobalPosition(hexDrawDirection)).normalized;

            Gizmos.DrawLine(_tileMap.transform.position, _tileMap.transform.position + ((Vector3)drawDirection * _drawDistance));
        }
    }
}
