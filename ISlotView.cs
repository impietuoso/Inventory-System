using UnityEngine;
using TMPro;

public class ISlotView : DataView<ISlot> {
    public DataView ItemView;
    public TextMeshProUGUI amount;
    public int minimalAmountToShow = 2;
    public GameObject favoriteIcon;

    public override void Subscribe() {
        Data.OnItemChanged += Slot_OnItemChanged;
        Data.OnAmountChanged += Slot_OnAmountChanged;
        Data.OnFavoriteChanged += Slot_OnFavoriteChanged;

        Slot_OnItemChanged(Data.item);
        Slot_OnAmountChanged(Data.amount);
        Slot_OnFavoriteChanged(Data.favorite);
    }

    public override void Unsubscribe() {
        Data.OnItemChanged -= Slot_OnItemChanged;
        Data.OnAmountChanged -= Slot_OnAmountChanged;
        Data.OnFavoriteChanged -= Slot_OnFavoriteChanged;

        Slot_OnItemChanged(null);
        Slot_OnAmountChanged(0);
        Slot_OnFavoriteChanged(false);
    }


    public void SetFavorite(bool value) {
        Data.favorite = value;
    }

    public void ToggleFavorite() {
        Data.favorite = !Data.favorite;
    }

    public void RemoveItem() {
        Data.amount = 0;
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
