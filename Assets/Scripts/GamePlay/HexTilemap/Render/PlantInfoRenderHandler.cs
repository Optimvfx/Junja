using Game.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gamplay
{
    public class PlantInfoRenderHandler : TileRenderHandler
    {
        private readonly Dictionary<HexVectorInt, PlantInfoRenderer> _createdRenderers = new Dictionary<HexVectorInt, PlantInfoRenderer>();

        [SerializeField] private HexTilemap _renderField;
        [SerializeField] private Canvas _renderersContainer;
        [SerializeField] private PlantInfoRenderer _rendererPrefab;

        public override void RenderTile(ReadOnlyGameField gameField, HexVectorInt position, HexVectorInt tileMapOffset)
        {
            var tilemapTilePosition = position + tileMapOffset;
            var renderTarget = gameField.Get(position);

            if (renderTarget == null)
                return;

            if (RendererAlreadyCreated(tilemapTilePosition))
            {
                RenderNewInfo(tilemapTilePosition, renderTarget);
            }
            else
            {
                CreateNewRenderer(tilemapTilePosition, renderTarget);
            }
        }

        private bool RendererAlreadyCreated(HexVectorInt position)
        {
            return _createdRenderers.ContainsKey(position);
        }

        private void RenderNewInfo(HexVectorInt position, ReadOnlyPlant renderTarget)
        {
            var renderer = _createdRenderers[position];

            renderer.Render(renderTarget);
        }

        private void CreateNewRenderer(HexVectorInt position, ReadOnlyPlant renderTarget)
        {
            var globalPlantPosition = _renderField.HexTilePositionToGlobalPosition(position);

            var newRenderer = Instantiate(_rendererPrefab, globalPlantPosition, Quaternion.identity, _renderersContainer.transform);

            newRenderer.Render(renderTarget);
        }
    }
}