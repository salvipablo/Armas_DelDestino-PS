using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ArmasDestinoPS.Inventory
{
  [RequireComponent(typeof(Image))]
  public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    #region Properties
      private RectTransform _rectTransform;
      private Canvas _canvas;
      private CanvasGroup _canvasGroup;

      private Transform _originalParent;
      private Vector2 _originalPosition;

      [SerializeField] private Canvas _dragCanvas;
      private Vector3 _dragOffset;
    #endregion

    #region Methods
      private void Awake()
      {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();

        _canvasGroup = GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
        {
          _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
      }

      public void OnBeginDrag(PointerEventData eventData)
      {
        _originalParent = transform.parent;
        _originalPosition = _rectTransform.anchoredPosition;

        // Pasamos el item al Canvas de Drag.
        transform.SetParent(_dragCanvas.transform, true);

        // Evitamos que el item bloquee el Raycast del objeto que está debajo.
        _canvasGroup.blocksRaycasts = false;

        // Calculamos la posición del mouse en el espacio del DragCanvas.
        RectTransform dragCanvasRect =
          _dragCanvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
              dragCanvasRect,
              eventData.position,
              eventData.pressEventCamera,
              out Vector3 worldPosition))
        {
          // Guardamos la distancia entre el mouse y el pivot del Image.
          _dragOffset = _rectTransform.position - worldPosition;
        }

        //Debug.Log($"Comenzando drag: {gameObject.name}");
      }

      public void OnDrag(PointerEventData eventData)
      {
        RectTransform dragCanvasRect = _dragCanvas.transform as RectTransform;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(dragCanvasRect,
                eventData.position, eventData.pressEventCamera, out Vector3 worldPosition))
        {
          _rectTransform.position = worldPosition + _dragOffset;
        }
      }

      public void OnEndDrag(PointerEventData eventData)
      {
        _canvasGroup.blocksRaycasts = true;

        // Volvemos a la jerarquía original.
        transform.SetParent(_originalParent, true);

        // Volvemos a la posición original.
        _rectTransform.anchoredPosition = _originalPosition;

        //Debug.Log($"Terminando drag: {gameObject.name}");
      }
    #endregion
  }
}



