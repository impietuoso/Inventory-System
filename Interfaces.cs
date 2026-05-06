using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem {
    Sprite sprite { get; }
    string displayName { get; }
    string description { get; }
    int maxStack { get; }
}

public interface ISlot {
    IItem item { get; }
    int amount { get; set; }
    bool empty { get; }
    bool favorite { get; set; }

    //Split Stack
    //Merge Stack

    //Callback
    event Action<IItem> OnItemChanged;
    event Action<int> OnAmountChanged;
    event Action<bool> OnFavoriteChanged;
}

public interface IInventory : IEnumerable<ISlot> {
    int Add(IItem item, int amount);
    int Remove(IItem item, int amount);
    int CountItem(IItem item);
    

    //Callback
    event Action<ISlot> OnSlotAdded;
    event Action<ISlot> OnSlotRemoved;
}

//Praximas Funcoes//////////////
//Tooltip Mouse Over Compare Info
//Context Menu
//Separate Stack
//Auto Refil
//Quick Stack
//Compact
//Auto Organize
//Search
//Prioritize Slots