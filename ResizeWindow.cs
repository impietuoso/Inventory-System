using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public RectTransform target;
    public float safePixelDistance;
    public float snapX;
    Vector2 deltaDrag;
    Vector2 initialDeltaSize;
    public Vector2 sensitivity = new Vector2(-1, 1);
    public Vector2 maxSize = new Vector2(1000, 1000);
    public Vector2 minSize = new Vector2(320, 420);
    public Vector2 borderOffset;

    public void OnBeginDrag(PointerEventData eventData) {
        deltaDrag = Vector2.zero;
        initialDeltaSize = target.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData) {
        deltaDrag += eventData.delta * sensitivity;

        bool onScreen = Input.mousePosition.x > safePixelDistance && Input.mousePosition.x < Screen.width - safePixelDistance && Input.mousePosition.y > safePixelDistance && Input.mousePosition.y < Screen.height - safePixelDistance;
        if (onScreen) 
            Resize(eventData);
    }

    void Resize(PointerEventData eventData) {
        //var newSize = target.sizeDelta - eventData.delta * sensitivity;
        var newSize = initialDeltaSize - deltaDrag;

        //Snap newSize.x TO snapX
        newSize.x = Mathf.Round(newSize.x / snapX) * snapX;
        newSize += borderOffset;

        if (newSize.x < minSize.x) newSize.x = minSize.x;
        else if (newSize.x > maxSize.x) newSize.x = maxSize.x;

        if (newSize.y < minSize.y) newSize.y = minSize.y;
        else if (newSize.y > maxSize.y) newSize.y = maxSize.y;

        target.sizeDelta = newSize;
    }

    public void OnEndDrag(PointerEventData eventData) {

    }
}