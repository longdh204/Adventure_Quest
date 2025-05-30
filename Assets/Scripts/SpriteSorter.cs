using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer SpriteRenderer;
    void Start()
    {
          
    }

    void Update()
    {
        SpriteRenderer.sortingOrder = 1000 - (int)(transform.position.y * 10);
    }
}
