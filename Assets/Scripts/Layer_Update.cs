using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_Update : MonoBehaviour
{
    private SpriteRenderer sprite;

    public Vector3 pos;
    public Collider2D col;

    private int desiredLayer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        float boundExtent = col.bounds.extents.y;

        pos = col.bounds.center;

        desiredLayer = Mathf.RoundToInt((pos.y - boundExtent) * 10) * -1;

        if (sprite)
        {
            if (sprite.sortingOrder != desiredLayer)
            {
                sprite.sortingOrder = desiredLayer;
            }
        }
    }
}
