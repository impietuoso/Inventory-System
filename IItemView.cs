using TMPro;
using UnityEngine.UI;

public class IItemView : DataView<IItem> {
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI description;
    public Image sprite;
    
    public override void Subscribe() {
        if (displayName) displayName.text = Data.displayName;
        if (description) description.text = Data.description;
        if (sprite) sprite.overrideSprite = Data.sprite;
    }

    public override void Unsubscribe() {
        if (displayName) displayName.text = "";
        if (description) description.text = "";
        if (sprite) sprite.overrideSprite = null;
    }
}