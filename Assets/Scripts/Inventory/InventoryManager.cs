using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Slot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    [SerializeField] private int maxStackedItems = 10;

    [HideInInspector]public bool allSlotsOcupied;
    private void Update() {
        allSlotsOcupied = Array.TrueForAll(inventorySlots, x => x.isOccupied == true);
    }
    public bool AddItem(Item item)
    {
        //add count to existing item in inventory
        for(int i = 0; i< inventorySlots.Length; i++)
        {
            Slot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && itemInSlot.item == item && 
            itemInSlot.count <= maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count += 1;
                itemInSlot.RefreshCount();
                Debug.Log("Added!");
                return true;
            }
        }
        //find emmpty slot
        for(int i = 0; i< inventorySlots.Length; i++)
        {
            Slot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null && slot.isOccupied == false)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    void SpawnNewItem(Item item, Slot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        slot.isOccupied = true;
    }
    public void ThrowItem(){
        var item = GameObject.Find("DragContainer").transform.GetChild(0).gameObject;
        Destroy(item);
    }
}
