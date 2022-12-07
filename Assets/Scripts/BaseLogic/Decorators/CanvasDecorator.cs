using UnityEngine;

namespace Game.GameLogic.Decorator
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(RectTransform))]
    public class CanvasDecorator : MonoBehaviour
    {
        private Canvas _canvas;

        private RectTransform _canvasRectTransform;

        public Vector3 CanvasPositionToWorldPosition(Vector3 position, Camera camera = null)
        {
            if (camera == null)
                camera = Camera.main;

            return position;
        }

        public Canvas GetCanvas()
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();

            return _canvas;
        }

        public RectTransform GetCanvasRect()
        {
            if (_canvasRectTransform == null)
               _canvasRectTransform = GetComponent<RectTransform>();

            return _canvasRectTransform;
        }

        public static implicit operator Canvas(CanvasDecorator canvasDecorator) => canvasDecorator.GetCanvas();
    }
}