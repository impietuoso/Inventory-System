using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridInventory<T> : IInventory where T : IItem {
    public List<Slot<T>> slots = new();

    public GridInventory(int size) {
        for (int i = 0; i < size; i++) {
            Slot<T> newSlot = new Slot<T>();
            slots.Add(newSlot);
        }
    }

    //Callbacks
    public event Action<ISlot> OnSlotAdded;
    public event Action<ISlot> OnSlotRemoved;

    //Funcoes de inventario
    public int Add(IItem item, int amount) {
        foreach (var slot in slots) {
            if (Equals(slot.item, item)) {
                int newMaxAmount = slot.amount + amount;
                if (newMaxAmount <= slot.item.maxStack) {
                    slot.amount += amount;
                    return 0;
                } else {
                    amount = newMaxAmount - slot.item.maxStack;
                    slot.amount = slot.item.maxStack;
                }
            }
        }

        foreach (var slot in slots) {
            if (slot.empty) {
                slot.item = (T)item;

                int newMaxAmount = slot.amount + amount;
                if (newMaxAmount <= slot.item.maxStack) {
                    slot.amount += amount;
                    return 0;
                } else {
                    amount = newMaxAmount - slot.item.maxStack;
                    slot.amount = slot.item.maxStack;
                }
            }
        }

        return amount;
    }

    public int Remove(IItem item, int amount) {
        if (item is null) throw new ArgumentNullException("item is null");

        int min = 0;
        
        for (int i = slots.Count - 1; i >= 0; i--) {
            Slot<T> slot = slots[i];
            if (Equals(slot.item, item)) {
                min = Mathf.Min(slot.amount, amount);
                slot.amount -= min;
                amount -= min;
                slot.amount = Mathf.Min(slot.amount, amount);
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
