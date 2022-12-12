using Game.Core;
using Game.GameLogic;
using Game.GameLogic.Edit;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gamplay
{
    public class HexTilemapRenderer : MonoBehaviour, IInitable<HexVectorInt>
    {
        private HexVectorInt _offset = HexVectorInt.Zero;

        [Header("Render Views")]
        [SerializeField] private TileRenderer[] _gameRenderes;
        [SerializeField] private TileRenderHandler[] _tileRenderHandlers;

        public void Init(HexVectorInt offset)
        {
            _offset = offset;
        }

        public void RenderAllPoints(ReadOnlyGameField gameField)
        {
            ClearMaps();

            foreach (var cell in gameField.GetAllCells())
            {
                RenderSinglePoint(gameField, cell.Position);
            }
        }

        public void RenderSinglePoint(ReadOnlyGameField gameField, HexVectorInt position)
        {
            foreach (var gameRenderer in _gameRenderes)
            {
                gameRenderer.RenderTile(gameField, position, _offset);
            }

            foreach(var renderHandler in _tileRenderHandlers)
            {
                renderHandler.RenderTile(gameField, position, _offset);
            }
        }

        [ContextMenu("Clear")]
        private void ClearMaps()
        {
            foreach (var gameRenderer in _gameRenderes)
            {
                gameRenderer.Clear();
            }
        }

        public struct GameFieldPosition
        {
            public readonly ReadOnlyGameField GameField;

            public readonly HexVectorInt Position;

            public GameFieldPosition(ReadOnlyGameField gameField, HexVectorInt position)
            {
                GameField = gameField;
                Position = position;
            }
        }

        [System.Serializable]
        public class TileRenderer
        {
            [SerializeField] private string _name;
            [Header("Render")]
            [SerializeField] HexTilemap _renderTarget;
            [SerializeField] ScriptableTileFactory _renderingTileSellector;

            public void RenderTile(ReadOnlyGameField gameField, HexVectorInt position, HexVectorInt tileMapOffset)
            {
                if (_renderingTileSellector == null)
                {
                    Debug.LogWarning($"No tile sellector sellected. {{{_name}}}");

                    return;
                }

                var tile = SellectTile(gameField, position, _renderingTileSellector);

                if (tile == null)
                    return;

                _renderTarget.Set(tile, position + tileMapOffset);
            }

            public void Clear()
            {
                _renderTarget.Clear();
            }

            private Tile SellectTile(ReadOnlyGameField gameField, HexVectorInt position, ITileFactory<GameFieldPosition> tileFactory)
            {
                return tileFactory.GenerateTile(new GameFieldPosition(gameField, position));
            }
        }
    }
}