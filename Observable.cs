using System;
using UnityEngine;

[Serializable]
public class Observable<T> {
    [SerializeField]
    private T value;
    public event Action<T> OnChange;

    public T Value {
        get => value;
        set {
            if (Equals(this.value, value)) return;
            this.value = value;
            OnChange?.Invoke(value);
        }
    }

    public Observable(T value = default) {
        this.value = value;
    }
}
