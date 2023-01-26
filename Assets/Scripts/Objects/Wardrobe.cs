using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public float increaseAmount = 1.0f;
    public float decreaseAmount = 1.0f;
    public float increaseTime = 1.0f;
    public float decreaseTime = 1.0f;
    private float currentTime = 0.0f;
    private bool increasing = false;
    private bool decreasing = false;

    [Header("Changin part")]
    [SerializeField] private List<Sprite> hairs = new List<Sprite>();
    [SerializeField] private List<Sprite> beards = new List<Sprite>();
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer beard;
     private int hair_index;
     private int beard_index;
    [SerializeField] private ClothesData clothesData;
    [SerializeField] private GameObject buttons;

    private void Awake()
    {
        text.alpha = 0.0f;
        buttons.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            text.text = "Press E";
            increasing = true;
            decreasing = false;
            currentTime = 0.0f;
            Load();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            decreasing = true;
            increasing = false;
            currentTime = 0.0f;
            Save();
            buttons.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                text.text = "<...>";
                buttons.SetActive(true);
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
            float value = Mathf.Lerp(increaseAmount,0, t);
            // Use the value to decrease something
            decreasing = (t < 1.0f);
            text.alpha = value;
        }
    }
    public void ChangeHair()
    {
        hair_index = Scroll(hair_index, hairs);
        hair.sprite = hairs[hair_index];
    }
    public void ChangeBeard()
    {
        beard_index = Scroll(beard_index, beards);
        beard.sprite = beards[beard_index];
    }
    private void Save()
    {
        clothesData.HairHat = hair_index;
        clothesData.Beard = beard_index;
    }
    private void Load()
    {
        hair_index = clothesData.HairHat;
        beard_index = clothesData.Beard;
    }
    private int Scroll(int index, List<Sprite> sprites)
    {
        index++;
        if (index > sprites.Count - 1) index = 0;
        return index;
    }
}