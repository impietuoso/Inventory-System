using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Drop : MonoBehaviour, IDropHandler {
    public UnityEvent<GameObject, PointerEventData> dropEvent;

    public void OnDrop(PointerEventData eventData) {
        //if (Touch.activeFingers.Count > 1) return;
        dropEvent.Invoke(gameObject, eventData);
    }
}