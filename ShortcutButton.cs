using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShortcutButton : Selectable {
    public UnityEvent onClick;
    public CustomKey[] shortcuts;

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        if (!IsInteractable()) return;

        foreach (var item in shortcuts) {
            if (item.mouseButton == eventData.button) {
                if (Input.GetKey(item.key)) {
                    item.keyEvent.Invoke();
                    return;
                }
            }
        }

        onClick.Invoke();
    }

    #if UNITY_EDITOR
    protected override void Reset() {
        base.Reset();
        shortcuts = new CustomKey[] {
            new CustomKey(){
                key = KeyCode.LeftAlt, mouseButton = 0
            },
            new CustomKey(){
                key = KeyCode.LeftControl, mouseButton = 0
            },
            new CustomKey(){
                key = KeyCode.LeftShift, mouseButton = 0
            }
        };
    }
    #endif

    [Serializable]
    public class CustomKey {
        public PointerEventData.InputButton mouseButton;
        public KeyCode key;
        public UnityEvent keyEvent;
    }
}