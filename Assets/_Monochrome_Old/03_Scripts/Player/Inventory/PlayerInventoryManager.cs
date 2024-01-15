using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Instance { get; private set; }

    public List<PickupInteractable> inventoryItems = new List<PickupInteractable>();
    public List<Image> itemSlotImages = new List<Image>();
    public int inventoryCapacity = 2;
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Sprite emptySlotSprite;

    private void Awake()
    {
        Instance = this;
        CreateInventoryUI();
    }

    private void CreateInventoryUI()
    {
        //create new children game objects for the inventory canvas equal to the inventory capacity
        for(int i = 0; i < inventoryCapacity; i++)
        {
            GameObject newSlot = new GameObject("Slot " + i);
            newSlot.AddComponent<RectTransform>();
            newSlot.transform.SetParent(inventoryCanvas.transform);
            
            RectTransform newSlotTransform = newSlot.GetComponent<RectTransform>();
            //anchor the image to the top left corner of the canvas
            newSlotTransform.anchorMax = new Vector2(0, 1);
            newSlotTransform.anchorMin = new Vector2(0, 1);
            //move it to a new spot based on its index so that they are stacked on top of each other
            newSlotTransform.anchoredPosition = new Vector2(25, (-i * 50) - 30);
            newSlotTransform.sizeDelta = new Vector2(50, 50);
            //add an image component to the slot
            newSlot.AddComponent<Image>();
            itemSlotImages.Add(newSlot.GetComponent<Image>());
            //set the image to the empty slot sprite
            itemSlotImages[i].sprite = emptySlotSprite;
        } 
    }

    public void AddItem(PickupInteractable item)
    {
        inventoryItems.Add(item);
        
        foreach(Image image in itemSlotImages)
        {
            if(image.sprite == emptySlotSprite)
            {
                image.sprite = item.itemSprite;
                //set the item to inactive
                item.gameObject.SetActive(false);
                break;
            }
        }     
        
        UserSelectItemToReplace();
    }

    private void UserSelectItemToReplace()
    {
        return;
    }

    public void RemoveItem(PickupInteractable item)
    {
        inventoryItems.Remove(item);
        //remove the item slot image associated with the item
        foreach(Image image in itemSlotImages)
        {
            if(image.sprite == item.itemSprite)
            {
                image.sprite = emptySlotSprite;
                item.gameObject.SetActive(true);
            }
        }

    }

    public bool HasItem(PickupInteractable item)
    {
        return inventoryItems.Contains(item);
    }

}
