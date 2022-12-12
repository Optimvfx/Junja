using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.GameLogic.Edit
{
    public class GameLvlCreator : MonoBehaviour
    {
        [SerializeField] private HexTilemap _lvlEditMap;
        [Header("Generation Config")]
        [SerializeField] private string _newLvlName;

        public string NewLvlName => _newLvlName;

        public GameLvlInfo CalculateGameLvlInfo()
        {
            var vectorBounds = _lvlEditMap.GetAllTileBounds();
            var hexBounds = _lvlEditMap.GetBounds();

            var hexSize = _lvlEditMap.GetSize();

            var plantCells = _lvlEditMap.GetAllCells<PlantTile>();

            var hexArray = new HexArray<PlantTile>(hexSize, new HexTilemap.TilemapHexPositionToCellPositionConvertor());
            var hexArrayOffset = hexBounds.Min;

            foreach (var plantCell in plantCells.Where(cell => cell.Value != null))
            {
                var arrayCellPosition = plantCell.Position - hexArrayOffset;

                hexArray.Set(arrayCellPosition, plantCell.Value);
            }

            return new GameLvlInfo(hexArray, hexArrayOffset, _newLvlName);
        }
    }
}
