using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class DataView : MonoBehaviour {
    public abstract void SetData(object data);
    public abstract object GetData();

    public void CopyData(DataView view) {
        SetData(view.GetData());
    }
}

public abstract class DataView<T> : DataView {
    public T data { get; private set; }
    public abstract void Subscribe();
    public abstract void Unsubscribe();

    public override void SetData(object newData) {
        try {
            if (data != null) Unsubscribe();
            if (newData != null && newData is not T)
                throw new Exception("Expected: " + typeof(T).Name + ". But got: " + newData.GetType().Name);
            data = (T)newData;
            if (data != null) Subscribe();
        } catch (Exception e) {
          Debug.LogError("Erro Set Data for Object " + newData, newData as Object);
          Debug.LogException(e, newData as Object);
        }
        
    }
    
    public override object GetData() {
        return data;
    }
}