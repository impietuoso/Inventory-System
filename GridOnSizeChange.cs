using UnityEngine;
using UnityEngine.UI;

public class GridOnSizeChange : MonoBehaviour {
    public GridLayoutGroup grid;
    public int collumNumber;
    public float padding;

    public void OnRectTransformDimensionsChange() {
        ChangeCellSize();
    }

    [ContextMenu("Change Cell Size")]
    public void ChangeCellSize() {
        var size = GetComponent<RectTransform>().rect.size;
        float totalSpacing = grid.spacing.x * (collumNumber - 1);
        float totalPadding = grid.padding.left + grid.padding.right;
        var cellSize = ((size.x - totalPadding - totalSpacing) / collumNumber) - padding;
        grid.cellSize = new Vector2(cellSize, cellSize);
    }
}