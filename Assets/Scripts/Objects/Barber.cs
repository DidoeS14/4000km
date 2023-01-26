using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barber : MonoBehaviour
{
    [SerializeField] private List<Sprite> hairs = new List<Sprite>();
    [SerializeField] private List<Sprite> beards = new List<Sprite>();
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer beard;
    private int hair_index;
    private int beard_index;
    [SerializeField] private ClothesData clothesData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            hair = player.transform.Find("Hair-Hat").gameObject.GetComponent<SpriteRenderer>(); // getting the child for hair  from player and getting its sprite renderer
            beard = player.transform.Find("Beard").gameObject.GetComponent<SpriteRenderer>();
            Load();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Save();
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
        if(hair != null && beard != null)
        {
            clothesData.HairHat = hair_index;
            clothesData.Beard = beard_index;
        }
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
