using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalItem : MonoBehaviour
{
    public Item itemData;
    private SpriteRenderer image;
    void Start()
    {
        image = this.GetComponent<SpriteRenderer>();
        image.sprite = itemData.image;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player"){
            var player_inv = other.GetComponentInChildren<InventoryManager>();
            if(!player_inv.allSlotsOcupied){
                bool result = player_inv.AddItem(itemData);
                //if(result) Debug.Log("Item collected");
                Destroy(this.gameObject);
            }
            
        }
    }
}
