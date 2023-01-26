using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeSprite : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool in_ui = false; //same script will be used multiple times on multiple ocations, this will help to use it in case it's in character creator or while live playing
    [Header("Sprite holders")]
    public List<Sprite> number_of_hair_sprites = new List<Sprite>();
    public List<Sprite> number_of_beard_sprites = new List<Sprite>();
    public List<Sprite> number_of_top_sprites = new List<Sprite>();
    public List<Sprite> number_of_bottom_sprites = new List<Sprite>();
    public List<Sprite> number_of_shoes_sprites = new List<Sprite>();

    [Header("Sprite renderers")]
    [SerializeField] private SpriteRenderer hair;
    [SerializeField] private SpriteRenderer beard;
    [SerializeField] private SpriteRenderer top;
    [SerializeField] private SpriteRenderer bottom;
    [SerializeField] private SpriteRenderer shoes;

    [Header("SpriteData")]
    public ClothesData clothesData;
    [SerializeField] private bool usedInCharacterCreator;

    [Header("Animators")]
    public List<Animator> animators= new List<Animator>(); // add in so script object
    [Header("Animation controllers")] 
    public List<RuntimeAnimatorController> controllers = new List<RuntimeAnimatorController>();
    private RuntimeAnimatorController chosenTop, chosenBottom, chosenShoes;


    private int hairspriteIndex = 0;
    private int beardspriteIndex = 0;
    private int topspriteIndex = 0;
    private int bottomspriteIndex = 0;
    private int shoesspriteIndex = 0;

    private Sprite hair_sprite = null;
    private Sprite beard_sprite = null;
    private Sprite top_sprite = null;
    private Sprite bottom_sprite = null;
    private Sprite shoes_sprite = null;

    private void Awake()
    {
        if(!usedInCharacterCreator)
        LoadClothesData();
    }
    private void Start()
    {
        top.sprite = null;
        bottom.sprite = null;
        shoes.sprite = null;
    }
    // Update is called once per frame
    void Update()
    {
       if(!in_ui) OutOfUI();
    }
    public void ChangeHairHat()
    {
        hairspriteIndex = Scroll(hairspriteIndex, number_of_hair_sprites);
        hair_sprite = number_of_hair_sprites[hairspriteIndex];
        hair.sprite = hair_sprite;
    }
    public void ChangeBeard()
    {
        beardspriteIndex = Scroll(beardspriteIndex, number_of_beard_sprites);
        beard_sprite = number_of_beard_sprites[beardspriteIndex];
        beard.sprite = beard_sprite;

    }
    public void ChangeTop()
    {
        topspriteIndex = Scroll(topspriteIndex, number_of_top_sprites);
        top_sprite = number_of_top_sprites[topspriteIndex];
        top.sprite = top_sprite;
    }
    public void ChangeBottom()
    {
        bottomspriteIndex = Scroll(bottomspriteIndex, number_of_bottom_sprites);
        bottom_sprite = number_of_bottom_sprites[bottomspriteIndex];
        bottom.sprite = bottom_sprite;
    }
    public void ChangeShoes()
    {
        shoesspriteIndex = Scroll(shoesspriteIndex, number_of_shoes_sprites);
        shoes_sprite = number_of_shoes_sprites[shoesspriteIndex];
        shoes.sprite = shoes_sprite;
    }
    private int Scroll(int index, List<Sprite> sprites)
    {
        index++;
        if (index > sprites.Count - 1) index = 0;
        return index;
    }
    public void SaveClothesData()
    {
        clothesData.HairHat = hairspriteIndex;
        clothesData.Beard = beardspriteIndex;
        clothesData.Top = topspriteIndex;
        clothesData.Bottom = bottomspriteIndex;
        clothesData.Shoes = shoesspriteIndex;
        Debug.Log($"{clothesData.HairHat}, {clothesData.Beard},{ clothesData.Top}, {clothesData.Bottom}, {clothesData.Shoes}");

        clothesData.HairColor = hair.color;
        clothesData.BeardColor = beard.color;
        clothesData.TopColor = top.color;
        clothesData.BottomColor = bottom.color;
        clothesData.ShoesColor = shoes.color;

        foreach (RuntimeAnimatorController controler in controllers)
        {
            try
            {
                if (controler.name == top.sprite.name) { chosenTop = controler; }
            }
            catch
            {
                chosenTop = null;
            }
        }
        foreach (RuntimeAnimatorController controler in controllers)
        {
            try
            {
                if (controler.name == bottom.sprite.name) chosenBottom = controler;
            }
            catch
            {
                chosenBottom = null;
            }
        }
        foreach (RuntimeAnimatorController controler in controllers)
        {
            try
            {
                if (controler.name == shoes.sprite.name) chosenShoes = controler;
            }
            catch
            {
                chosenShoes= null;
            }
        }
        clothesData.ChosenTop = chosenTop;
        clothesData.ChosenBottom = chosenBottom;
        clothesData.ChosenShoes = chosenShoes;
    }
    public void LoadClothesData()
    {
        hair.sprite = number_of_hair_sprites[clothesData.HairHat];
        beard.sprite = number_of_beard_sprites[clothesData.Beard];
        top.sprite = number_of_top_sprites[clothesData.Top];
        bottom.sprite = number_of_bottom_sprites[clothesData.Bottom];
        shoes.sprite = number_of_shoes_sprites[clothesData.Shoes];

        hair.color = clothesData.HairColor;
        beard.color = clothesData.BeardColor;
        top.color = clothesData.TopColor;
        bottom.color = clothesData.BottomColor;
        shoes.color = clothesData.ShoesColor;

        chosenTop= clothesData.ChosenTop;
        chosenBottom = clothesData.ChosenBottom;
        chosenShoes = clothesData.ChosenShoes;

        animators[0].runtimeAnimatorController = chosenTop;
        animators[1].runtimeAnimatorController = chosenBottom;
        animators[2].runtimeAnimatorController = chosenShoes;
    }
    private void OutOfUI()
    {
        //for now:   
        //todo change this when you get to using this script out of character creator
        //generally finish it, for now changes only hair, and hair won't even be able to be changed everywhere
        if (Input.GetKeyDown(KeyCode.E))
        {
            hairspriteIndex++;
            if (hairspriteIndex > number_of_hair_sprites.Count - 1) hairspriteIndex = 0;
            hair_sprite = number_of_hair_sprites[hairspriteIndex];
            GetComponent<SpriteRenderer>().sprite = hair_sprite;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hairspriteIndex--;
            if (hairspriteIndex < 0) hairspriteIndex = number_of_hair_sprites.Count - 1;
            hair_sprite = number_of_hair_sprites[hairspriteIndex];
            GetComponent<SpriteRenderer>().sprite = hair_sprite;
        }
        //Debug.Log(hairspriteIndex);
    }
}
