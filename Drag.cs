using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public CanvasGroup target;
    public float safePixelDistance;
    public bool returnToPosition;
    private Vector2 initialPos;

    public void OnBeginDrag(PointerEventData eventData) {
        if (Touch.activeFingers.Count > 1) return;
        target.blocksRaycasts = false;
        initialPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        if (Touch.activeFingers.Count > 1) return;
        var position = eventData.position;
        bool onScreen = position.x > safePixelDistance && position.x < Screen.width - safePixelDistance && position.y > safePixelDistance && position.y < Screen.height - safePixelDistance;
        if (onScreen)
            target.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //if (Touch.activeFingers.Count > 1) return;
        target.blocksRaycasts = true;
        if (returnToPosition) target.transform.position = initialPos;
    }
}
