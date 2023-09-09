using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    [SerializeField] Image[] slotImages;
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] int numberOfSlots;

    private void Start()
    {
        slotImages = numberOfSlots > 0 ? new Image[numberOfSlots] : new Image[0];
        PlayerInventoryManager.Instance.OnInventoryChanged += ReplaceSlotImages;
    }

    private void Update()
    {
        PlayerInventoryManager.Instance.OnInventoryChanged += ReplaceSlotImages;
    }

    private void ReplaceSlotImages()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < PlayerInventoryManager.Instance.inventoryItems.Count)
            {
                slotImages[i].sprite = PlayerInventoryManager.Instance.inventoryItems[i].itemSprite.sprite;
            }
        }
    }


}
