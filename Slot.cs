using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Slot<T> : ISlot where T : IItem {
    //Propriedades
    IItem ISlot.item => item;
    public T item { get { return _item; } set { _item = value; OnItemChanged?.Invoke(value); } }
    public int amount { get { return _amount; } set {
            _amount = value;
            OnAmountChanged?.Invoke(value);
            if (_amount <= 0 && !favorite) item = default;
        } }
    public bool favorite { get { return _favorite; } set { _favorite = value; OnFavoriteChanged?.Invoke(value); } }
    public bool empty { get { return item == null; } }

    //Campos
    [SerializeField] T _item;
    [SerializeField] int _amount;
    bool _favorite;

    //Callbacks
    public event Action<IItem> OnItemChanged;
    public event Action<int> OnAmountChanged;
    public event Action<bool> OnFavoriteChanged;
}
