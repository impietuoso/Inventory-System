using System;
using UnityEngine;
public interface IObservable<out T>
{
    T Value { get; }
    event Action<T> OnChange;
}

[Serializable]
public class Observable<T> : IObservable<T> {
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
