using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] GameObject menu;
    [Header("First message is required!")]
    [SerializeField] string firstMessage = "<...>";
    [SerializeField] string secondMessage = "<...>";
    private float increaseAmount = 1.0f;
    private float decreaseAmount = 1.0f;
    private float increaseTime = 1.0f;
    private float decreaseTime = 1.0f;
    private float currentTime = 0.0f;
    private bool increasing = false;
    private bool decreasing = false;

    private void Awake()
    {
        text.alpha = 0.0f;
        if (menu != null)
        menu.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            text.text = firstMessage;
            increasing = true;
            decreasing = false;
            currentTime = 0.0f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            decreasing = true;
            increasing = false;
            currentTime = 0.0f;
            if (menu != null)
                menu.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                text.text = secondMessage;
                if (menu != null)
                    menu.SetActive(true);
            }
        }
    }


    void Update()
    {
        if (increasing)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / increaseTime;
            float value = Mathf.Lerp(0, increaseAmount, t);
            // Use the value to increase something
            increasing = (t < 1.0f);
            text.alpha = value;
        }
        else if (decreasing)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / decreaseTime;
            float value = Mathf.Lerp(increaseAmount, 0, t);
            // Use the value to decrease something
            decreasing = (t < 1.0f);
            text.alpha = value;
        }
    }
}
