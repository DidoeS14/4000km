using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartLevel : MonoBehaviour
{
    [SerializeField, Range(1, 5)] public int partLevel = 1;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite[] sprites;

    // Update is called once per frame
    void Update()
    {
        switch (partLevel)
        {
            case 1:
                renderer.sprite = sprites[0];
                break;
            case 2:
                renderer.sprite = sprites[1];
                break;
            case 3:
                renderer.sprite = sprites[2];
                break;
            case 4:
                renderer.sprite = sprites[3];
                break;
            case 5:
                renderer.sprite = sprites[4];
                break;
        }
    }
}
