using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class HairPainter : MonoBehaviour
{
    [SerializeField] private Slider hairColor;
    [SerializeField] private Slider beardColor;
    private SpriteRenderer hair;
    private SpriteRenderer beard;

    [SerializeField] private ClothesData clothesData;
    private bool change = false;
    void Start()
    {
        //LoadColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            hair.color = new Color(hairColor.value, hairColor.value, hairColor.value);
            beard.color = new Color(beardColor.value, beardColor.value, beardColor.value);
        }
    }
    public void SaveColor()
    {
        if(hair != null && beard != null)
        {
            clothesData.HairColor = hair.color;
            clothesData.BeardColor = beard.color;
        }        
    }
    public void LoadColor()
    {
        if(hair != null && beard != null) 
        {
            hair.color = clothesData.HairColor;
            beard.color = clothesData.BeardColor;
        }
        else
        {
            Debug.LogError("Can't find hair and beard sprite renderer components on player or player does not exist!");
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveColor();
            change = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                hair = player.transform.Find("Hair-Hat").gameObject.GetComponent<SpriteRenderer>(); // getting the child for hair  from player and getting its sprite renderer
                beard = player.transform.Find("Beard").gameObject.GetComponent<SpriteRenderer>(); 
                LoadColor();
                change = true;
            }

        }
    }
}
