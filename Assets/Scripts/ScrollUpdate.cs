using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUpdate : MonoBehaviour
{
    public GameObject loadMoreObjectPosition;
    public GameObject loadMorePivotTop;
    private void OnScrollRectChange(Vector2 position)
    {
        //if (loading || !enableScrollLoadMore)
        //    return;

        // Detect if more messages should be loaded
        if (loadMoreObjectPosition.transform.position.y < loadMorePivotTop.transform.position.y - 20)
        {
            Debug.Log("Load more relics");
        }
    }

}
