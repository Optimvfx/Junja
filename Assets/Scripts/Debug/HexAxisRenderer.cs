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
        [SerializeField] private Color _AxixColorR = Color.blue;
        [SerializeField] private Color _AxixColorS = new Color(230, 230, 250);
        [SerializeField] private Color _AxixColorQ = Color.green;
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

            DrawAxis(_AxixColorR, HexDirection.Direction.NegativeR);
            DrawAxis(_AxixColorS, HexDirection.Direction.NegativeS);
            DrawAxis(_AxixColorQ, HexDirection.Direction.NegativeQ);
        }

        private void DrawAxis(Color color, HexDirection.Direction direction)
        {
            if (_tileMap == null)
                return;

            Gizmos.color = color;

            var hexDrawDirection = HexDirection.ConvertDirectionToVector(direction);

            var drawDirection = (_tileMap.HexTilePositionToGlobalPosition(hexDrawDirection)).normalized;

            Gizmos.DrawLine(_tileMap.transform.position, _tileMap.transform.position + ((Vector3)drawDirection * _drawDistance));
        }
    }
}
