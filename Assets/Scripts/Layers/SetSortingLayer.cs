using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    public string sortingLayerName;
    public int orderInLayer;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = orderInLayer;
        }
    }
}
