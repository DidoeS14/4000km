using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{

    [Header("UI")]
    private GameObject dragContainer;
    [SerializeField] private GameObject dropItemScreen;
    [SerializeField] private GameObject physicalItem;
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector]public Item item;
    //[HideInInspector]
    public int count = 1;//was 1 originally

    private void Start()
    {
        dropItemScreen.SetActive(false);
        dragContainer = GameObject.Find("DragContainer");
        countText.raycastTarget = false;
    }
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }
    public void RefreshCount(){
        countText.text = count.ToSafeString();
        bool textActive = count>1;
        countText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        Slot slot = parentAfterDrag.GetComponent<Slot>();
        transform.SetParent(dragContainer.transform);
        dragContainer.transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Debug.Log(eventData.pointerEnter.name);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        if (eventData.pointerEnter != null && eventData.pointerEnter.name.StartsWith("Slot"))
        {
            Slot slot = eventData.pointerEnter.GetComponent<Slot>();
            if (slot != null && !slot.isOccupied)
            {
                transform.SetParent(eventData.pointerEnter.transform);
                transform.localPosition = Vector3.zero;
                //slot.isOccupied = true;
            }
            else
            {
                transform.SetParent(parentAfterDrag);
            }
        }
        else
        {
            dropItemScreen.SetActive(true);        
        }
    }
    // private void DropItem(PointerEventData eventData)
    // {
    //     // Slot slot = eventData.pointerEnter.GetComponent<Slot>();
    //     //     if (slot != null && !slot.isOccupied)
    //     //     {
    //     //         transform.SetParent(eventData.pointerEnter.transform);
    //     //         transform.localPosition = Vector3.zero;
    //     //         //slot.isOccupied = true;
    //     //     }
    //     //     else
    //     //     {
    //     //         transform.SetParent(parentAfterDrag);
    //     //     }
    //     //     if(ContinueDrop())
    //                 Destroy(this.gameObject);
    // }
}
