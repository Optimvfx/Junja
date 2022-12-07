using UnityEngine;
using System;
using System.Linq;

namespace Game.GameLogic.Edit
{
    public class GameLvlInfo : ScriptableObject
    {
        [SerializeField] private string _newLvlName;

        private HexArray<PlantTile> _tileArray;

        public HexVectorInt _offsetToTileMap { get; private set; }

        public string NewLvlName => _newLvlName;

        public GameLvlInfo(HexArray<PlantTile> tileArray, HexVectorInt offsetToTileMap, string newLvlName)
        {
            _tileArray = tileArray.Clone();

            _offsetToTileMap = offsetToTileMap;

            _newLvlName = newLvlName;
        }

        public GameField GenerateGameField()
        {
            var gameFieldHexArray = new HexArray<Plant>(_tileArray.GetSize(), new HexTilemap.TilemapHexPositionToCellPositionConvertor());

            foreach(var cell in _tileArray.GetAllCells().Where(cell => cell.Value != null))
            {
                gameFieldHexArray.Set(cell.Position, cell.Value.GeneratePlant());
            }

            return new GameField(gameFieldHexArray);
        }
    }
}