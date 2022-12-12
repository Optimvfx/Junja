using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

namespace Game.GameLogic
{
    [RequireComponent(typeof(Tilemap))]
    public class HexTilemap : MonoBehaviour, IReadOnlyHexArray<Tile>
    {
        private Tilemap _tileMap;

        //Hex Base
        public static Vector2Int HexPositionToTilePosition(HexVectorInt vector)
        {
            var x = vector.R;
            var z = vector.Q;

            var col = x + (z - (z & 1)) / 2;
            var row = z;

            return new Vector2Int(col, row);
        }

        public static HexVectorInt TilePositionToHexPosition(Vector2Int tileMapPosition)
        {
            var tilePositionY = tileMapPosition.x;
            var tilePositionX = tileMapPosition.y;

            var r = tilePositionY - (tilePositionX - (tilePositionX & 1)) / 2;
            var s = tilePositionX;
            var q = -r - s;

            return new HexVectorInt(r, q, s);
        }

        public void Set(Tile tile, HexVectorInt position)
        {
            GetTileMap().SetTile((Vector3Int)HexPositionToTilePosition(position), tile);
        }

        public void Set(Tile tile, Vector2Int position)
        {
            GetTileMap().SetTile((Vector3Int)position, tile);
        }

        public Tile Get(HexVectorInt position)
        {
            return Get<Tile>(position);
        }

        public T Get<T>(HexVectorInt position)
            where T : Tile
        {
            return GetTileMap().GetTile<T>((Vector3Int)HexPositionToTilePosition(position));
        }

        public IEnumerable<HexCell<Tile>> GetAllCells()
        {
            return GetAllCells<Tile>();
        }

        public IEnumerable<HexCell<T>> GetAllCells<T>()
           where T : Tile
        {
            var tileMapSize = GetAllTileBounds();

            for (int x = tileMapSize.xMin; x < tileMapSize.xMax; x++)
            {
                for (int y = tileMapSize.yMin; y < tileMapSize.yMax; y++)
                {
                    var hexPosition = TilePositionToHexPosition(new Vector2Int(x, y));
                    var tile = Get<T>(hexPosition);

                    yield return new HexCell<T>(hexPosition, tile);
                }
            }
        }

        public void ClearAtPoint(HexVectorInt point)
        {
            GetTileMap().SetTile((Vector3Int)HexPositionToTilePosition(point), null);
        }

        public void Clear()
        {
            GetTileMap().ClearAllTiles();
        }

        public HexVectorInt GetSize()
        {
            return TilePositionToHexPosition(GetAllTileSize());
        }

        public HexVectorIntBounds GetBounds()
        {
            var tileBounds = GetAllTileBounds();

            var minBounds = TilePositionToHexPosition((Vector2Int)tileBounds.min);
            var maxBounds = TilePositionToHexPosition((Vector2Int)tileBounds.max);

            return new HexVectorIntBounds(minBounds, maxBounds);
        }

        public Vector2Int GetAllTileSize()
        {
            var bounds = GetAllTileBounds();

            return new Vector2Int(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin);
        }

        public BoundsInt GetAllTileBounds()
        {
            GetTileMap().CompressBounds();

            return GetTileMap().cellBounds;
        }

        public HexVectorInt GlobalPositionToHexTilePosition(Vector2 worldPosition)
        {
            var tilePosition = GlobalPositionToTilePosition(worldPosition);

            return TilePositionToHexPosition(tilePosition);
        }

        public Vector2 HexTilePositionToGlobalPosition(HexVectorInt hexTilePosition)
        {
            var tilePosition = HexPositionToTilePosition(hexTilePosition);

            return TilePositionToGlobalPosition(tilePosition);
        }

        //Base
        public Vector2Int GlobalPositionToTilePosition(Vector2 worldPosition)
        {
            return (Vector2Int)GetTileMap().WorldToCell(worldPosition);
        }

        public Vector2 TilePositionToGlobalPosition(Vector2Int tilePosition)
        {
            return GetTileMap().CellToWorld((Vector3Int)tilePosition);
        }

        private Tilemap GetTileMap()
        {
            if (_tileMap == null)
                _tileMap = GetComponent<Tilemap>();

            return _tileMap;
        }

        public class TilemapHexPositionToCellPositionConvertor : IHexPositionToCellPositionConvertor
        {
            public HexVectorInt CellToHex(Vector2Int cellPosition)
            {
                return TilePositionToHexPosition(cellPosition);
            }

            public Vector2Int HexToCell(HexVectorInt hexPosition)
            {
                return HexPositionToTilePosition(hexPosition);
            }
        }
    }
}