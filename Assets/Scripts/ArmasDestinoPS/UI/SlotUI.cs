using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ArmasDestinoPS.Inventory;

namespace ArmasDestinoPS.UI
{
  public class SlotUI : MonoBehaviour
  {
    #region Properties
      [Header("Visual")]
      [SerializeField] private Image itemImage;

      [Header("Slot")]
      [SerializeField] private TMP_Text quantityText;
      [SerializeField] private Button deleteButton;

      [Header("Item")]
      [SerializeField] private ItemInSlot itemInSlot;
      [SerializeField] private DraggableItem draggableItem;
      [SerializeField] private ItemDropSlot itemDropSlot;
    #endregion

    #region Methods
      public void SetEmpty(Item item)
      {
        itemImage.sprite = item.Icon;
        
        quantityText.text = 0.ToString();
        quantityText.gameObject.SetActive(false);
        
        deleteButton.gameObject.SetActive(false);
        
        draggableItem.enabled = false;
      }

      public void SetItem(Item item, int quantity)
      {
        itemImage.sprite = item.Icon;

        quantityText.text = quantity.ToString();
        quantityText.gameObject.SetActive(true);
        
        deleteButton.gameObject.SetActive(true);

        draggableItem.enabled = true;
      }

      public void SetItemInSlot(int numSection, int numSlot, string itemName, int quantity)
      {
        itemInSlot.SetSection(numSection);
        itemInSlot.SetSlot(numSlot);
        itemInSlot.UpdateItemName(itemName);
        itemInSlot.UpdateQty(quantity);
      }
    #endregion
  }
}
