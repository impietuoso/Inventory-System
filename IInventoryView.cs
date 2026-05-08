using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IInventoryView : MonoBehaviour {
    public ISlotView slotView;
    IInventory currentData;
    List<ISlotView> createdSlots = new();

    private void Start() {
        slotView.gameObject.SetActive(false);
    }
    
    public void SetData(IInventory inventory) {
        ResetData();
        currentData = inventory;

        currentData.OnSlotAdded += CurrentData_OnSlotAdded;
        currentData.OnSlotRemoved += CurrentData_OnSlotRemoved;

        foreach (var slot in inventory) {
            CurrentData_OnSlotAdded(slot);
        }
    }

    private void CurrentData_OnSlotAdded(ISlot obj) {
        ISlotView newSlotView = Instantiate(slotView,slotView.transform.parent);
        newSlotView.gameObject.SetActive(true);
        createdSlots.Add(newSlotView);
        newSlotView.SetData(obj);
    }

    private void CurrentData_OnSlotRemoved(ISlot obj) {
        var targetSlot = createdSlots.First(v => v.Data == obj);
        createdSlots.Remove(targetSlot);
        Destroy(targetSlot.gameObject);
    }

    private void ResetData() {
        if (currentData == null) return;
        currentData.OnSlotAdded -= CurrentData_OnSlotAdded;
        currentData.OnSlotRemoved -= CurrentData_OnSlotRemoved;
    }

    private void OnDestroy() {
        ResetData();
    }
}