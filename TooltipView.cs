using UnityEngine;

public class TooltipView : DataView<object> {
    public DataView view;
    float xPivot;
    float yPivot;
    RectTransform rect;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    private void Update() {
        transform.position = Input.mousePosition;

        xPivot = (transform.position.x > Screen.width / 2) ? 1 : 0;
        yPivot = (transform.position.y > Screen.height / 2) ? 1 : 0;

        rect.pivot = new (xPivot, yPivot);
    }

    protected override void Subscribe() {
        view.SetData(Data);
        gameObject.SetActive(true);
    }

    protected override void Unsubscribe() {
        gameObject.SetActive(false);
    }
}