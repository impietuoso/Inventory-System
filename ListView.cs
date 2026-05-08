using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ListView : DataView <IEnumerable> {
    public DataView template;
    [HideInInspector]
    public List<DataView> templateList = new();

    private void Awake() {
        template.gameObject.SetActive(false);
    }

    public void AddItem(int index, object newData) {
        var newTemplate = Instantiate(template, template.transform.parent);
        newTemplate.SetData(newData);
        newTemplate.gameObject.SetActive(true);
        newTemplate.transform.SetSiblingIndex(index + 1);
        templateList.Insert(index, newTemplate);
    }
    
    public void RemoveItem(int index) {
        Destroy(templateList[index].gameObject);
        templateList.RemoveAt(index);
    }

    public void ReplaceItem(int index, object newData) {
        templateList[index].SetData(newData);
    }
    
    public override void Subscribe() {
        var index = 0;
        
        foreach (var newData in Data) {
            AddItem(index++, newData);
        }

        if (Data is INotifyCollectionChanged coll) {
            coll.CollectionChanged += OnCollectionChanged;
        }
    }

    public override void Unsubscribe() {
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