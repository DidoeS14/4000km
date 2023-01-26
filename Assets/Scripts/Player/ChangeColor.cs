using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private bool hair;

    private void Update()
    {
        if (hair) ChangeHair();
        if (!hair) ChangeClothes();
    }
    private void ChangeHair()
    {
        renderer.color = new Color(slider.value, slider.value, slider.value);
    }
    private void ChangeClothes()
    {
        if(slider.value == 0)
        {
            renderer.color = new Color(5, 5, 5);
        }
        else if(slider.value == slider.maxValue)
        {
            renderer.color = new Color(255, 255, 255);
        }
        else
        {
            renderer.color = Color.HSVToRGB(slider.value, 1f, 1f);
        }
    }
}
