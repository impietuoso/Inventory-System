using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public interface IObservableList<out T> : INotifyCollectionChanged, IReadOnlyList<T> {
    
}

[Serializable]
public class ObservableList<T> : IList<T>, IObservableList<T> {
    [SerializeField]
    private List<T> list;

    public ObservableList(IEnumerable<T> itens) {
        list = new List<T>(itens);
    }
    
    public ObservableList() {
        list = new List<T>();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public T this[int index] {
        get => list[index];
        set {
            T oldItem = list[index];
            list[index] = value;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
        }
    }

    public int Count => list.Count;
    public bool IsReadOnly => false;

    public void Add(T item) {
        list.Add(item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, list.Count - 1));
    }

    public void Clear() {
        list.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(T item) => list.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

    public int IndexOf(T item) => list.IndexOf(item);

    public void Insert(int index, T item) {
        list.Insert(index, item);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    public bool Remove(T item) {
        int index = list.IndexOf(item);
        if (index >= 0) {
            list.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return true;
        }
        return false;
    }

    public void RemoveAt(int index) {
        T item = list[index];
        list.RemoveAt(index);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
    }

    IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
        CollectionChanged?.Invoke(this, e);
    }
}
