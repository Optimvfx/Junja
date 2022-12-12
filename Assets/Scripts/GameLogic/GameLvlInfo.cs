using UnityEngine;
using System;
using System.Linq;

namespace Game.GameLogic.Edit
{
    public class GameLvlInfo : ScriptableObject
    {
        [SerializeField] private string _newLvlName;

        [SerializeField] private Vector2Int _offsetToTilemap;

        [SerializeField] private HexCell<PlantTile>[] _gameCells;

        public HexVectorInt OffsetToTilemap => HexTilemap.TilePositionToHexPosition(_offsetToTilemap);

        public string NewLvlName => _newLvlName;

        public GameLvlInfo(HexArray<PlantTile> tileArray, HexVectorInt offsetToTileMap, string newLvlName)
        {
            _gameCells = tileArray.GetAllCells().Where(tile => tile.Value != null).ToArray();

            _offsetToTilemap = HexTilemap.HexPositionToTilePosition(offsetToTileMap);

            _newLvlName = newLvlName;
        }

        public GameField GenerateGameField()
        {
            var notEmptyGameCells = _gameCells.Where(tile => tile.Value != null);

            var hexArraySizeX = _gameCells.Max(cell => HexTilemap.HexPositionToTilePosition(cell.Position).x) + 1;
            var hexArraySizeY = _gameCells.Max(cell => HexTilemap.HexPositionToTilePosition(cell.Position).y) + 1;

            var gameFieldHexArray = new HexArray<Plant>(new Vector2Int(hexArraySizeX, hexArraySizeY), new HexTilemap.TilemapHexPositionToCellPositionConvertor());

            foreach (var cell in notEmptyGameCells)
            {
                gameFieldHexArray.Set(cell.Position, cell.Value.GeneratePlant());
            }

            return new GameField(gameFieldHexArray);
        }
    }
}