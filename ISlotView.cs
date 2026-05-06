using UnityEngine;
using TMPro;

public class ISlotView : DataView<ISlot> {
    public DataView ItemView;
    public TextMeshProUGUI amount;
    public int minimalAmountToShow = 2;
    public GameObject favoriteIcon;

    public override void Subscribe() {
        data.OnItemChanged += Slot_OnItemChanged;
        data.OnAmountChanged += Slot_OnAmountChanged;
        data.OnFavoriteChanged += Slot_OnFavoriteChanged;

        Slot_OnItemChanged(data.item);
        Slot_OnAmountChanged(data.amount);
        Slot_OnFavoriteChanged(data.favorite);
    }

    public override void Unsubscribe() {
        data.OnItemChanged -= Slot_OnItemChanged;
        data.OnAmountChanged -= Slot_OnAmountChanged;
        data.OnFavoriteChanged -= Slot_OnFavoriteChanged;

        Slot_OnItemChanged(null);
        Slot_OnAmountChanged(0);
        Slot_OnFavoriteChanged(false);
    }


    public void SetFavorite(bool value) {
        data.favorite = value;
    }

    public void ToggleFavorite() {
        data.favorite = !data.favorite;
    }

    public void RemoveItem() {
        data.amount = 0;
    }

    private void Slot_OnItemChanged(IItem iitem) {
        if (ItemView) ItemView.SetData(iitem);
    }

    private void Slot_OnAmountChanged(int iamount) {
        if (amount) {
            amount.text = iamount.ToString();
            amount.enabled = iamount >= minimalAmountToShow;
        }
    }

    private void Slot_OnFavoriteChanged(bool ifavorite) {
        if (favoriteIcon) favoriteIcon.SetActive(ifavorite);
    }
}
