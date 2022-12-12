using Game.Gamplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameLogic
{
    [CreateAssetMenu(fileName = "TileSellectorByPosibleDirection", menuName = "ScriptableObjects/TileSellector/TileSellectorByPosibleDirection", order = 1)]
    public class TileSellectorByPosibleDirection : ScriptableTileFactory
    {
        [SerializeField] private HexDirection.Direction _directionToMove;
        [Header("")]
        [SerializeField] private Tile _movePosibeleTile;
        [SerializeField] private Tile _moveUnPosibleTile;
        [SerializeField] private Tile _emptyTile;

        public override Tile GenerateTile(HexTilemapRenderer.GameFieldPosition arguments)
        {
            if (arguments.GameField.Get(arguments.Position) == null)
                return _emptyTile;

            if (arguments.GameField.IsMovePosible(arguments.Position, _directionToMove))
                return _movePosibeleTile;
            else
                return _moveUnPosibleTile;
        }
    }
}