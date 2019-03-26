using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollViewContent : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        CaculateGrid();
    }

    private void CaculateGrid()
    {
        GridLayoutGroup gridGroup = GetComponent<GridLayoutGroup>();
        float cellX = gridGroup.cellSize.x;
        float spaceX = gridGroup.spacing.x;
        float width = (transform.childCount) * (cellX + spaceX) - spaceX;
        float height = GetComponent<RectTransform>().rect.height;
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
