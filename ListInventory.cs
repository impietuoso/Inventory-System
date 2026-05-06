using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListInventory<T> : IInventory where T : IItem {
    public List<Slot<T>> slots = new();

    //Callbacks
    public event Action<ISlot> OnSlotAdded;
    public event Action<ISlot> OnSlotRemoved;

    //Funcoes de inventario
    public int Add(IItem item, int amount) {
        foreach (var currentSlot in slots) {
            if (Equals(currentSlot.item, item)) {
                int newMaxAmount = currentSlot.amount + amount;
                if (newMaxAmount <= currentSlot.item.maxStack) {
                    currentSlot.amount += amount;
                    return 0;
                } else {
                    amount = newMaxAmount - currentSlot.item.maxStack;
                    currentSlot.amount = currentSlot.item.maxStack;
                    return amount;
                }
            }
        }

        var slot = new Slot<T>();
        slot.item = (T)item;
        slot.amount = amount;
        slots.Add(slot);
        OnSlotAdded?.Invoke(slot);
        return Mathf.Max(0,amount-slot.item.maxStack);
    }

    public int Remove(IItem item, int amount) {
        if (item is null) throw new ArgumentNullException("item");

        int min = 0;
        
        for (int i = slots.Count - 1; i >= 0; i--) {
            Slot<T> slot = slots[i];
            if (Equals(slot.item, item)) {
                min = Mathf.Min(slot.amount, amount);
                slot.amount -= min;
                amount -= min;
                if (slot.amount <= 0) {
                    slots.RemoveAt(i);
                    OnSlotRemoved?.Invoke(slot);
                }
            }
        }
        return amount;
    }

    public int CountItem(IItem item) {
        int totalAmount = 0;
        foreach (var slot in slots) {
            if (Equals(slot.item, item)) {
                totalAmount += slot.amount;
            }
        }

        return totalAmount;
    }

    public IEnumerator<ISlot> GetEnumerator() {
        return slots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

}