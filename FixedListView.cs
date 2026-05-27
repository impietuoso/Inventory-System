using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FixedListView : DataView <IEnumerable> {
    public List<DataView> templateList = new();

    public void AddItem(int index, object newData) {
        if (index >= templateList.Count) return;
        var newTemplate = templateList[index];
        newTemplate.SetData(newData);
        newTemplate.gameObject.SetActive(true);
    }
    
    public void RemoveItem(int index) {
        if (index >= templateList.Count) return;
        templateList[index].SetData(null);
    }

    public void ReplaceItem(int index, object newData) {
        if (index >= templateList.Count) return;
        templateList[index].SetData(newData);
    }

    protected override void Subscribe() {
        var index = 0;
        
        foreach (var newData in Data) {
            AddItem(index++, newData);
        }

        if (Data is INotifyCollectionChanged coll) {
            coll.CollectionChanged += OnCollectionChanged;
        }
    }

    protected override void Unsubscribe() {
        foreach (var item in templateList) {
            Destroy(item.gameObject);
        }
        
        templateList.Clear();
        
        if (Data is INotifyCollectionChanged coll) {
            coll.CollectionChanged -= OnCollectionChanged;
        }
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
        switch (e.Action) {
            case NotifyCollectionChangedAction.Add:
                for (int i = 0; i < e.NewItems.Count; i++) {
                    AddItem(e.NewStartingIndex + i, e.NewItems[i]);
                }
                break;
            case NotifyCollectionChangedAction.Move:
                throw new Exception();
            case NotifyCollectionChangedAction.Remove:
                for (int i = 0; i < e.OldItems.Count; i++) {
                    RemoveItem(e.OldStartingIndex + i);
                }
                break;
            case NotifyCollectionChangedAction.Replace:
                for (int i = 0; i < e.NewItems.Count; i++) {
                    ReplaceItem(e.NewStartingIndex + i, e.NewItems[i]);
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                for (int i = 0; i < templateList.Count; i++) {
                    Destroy(templateList[i + 1]);
                }
                templateList.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}