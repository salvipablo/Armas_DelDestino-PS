using System;
using ArmasDestinoPS.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArmasDestinoPS.Inventory
{
  public class ItemDraggedInvBtn : MonoBehaviour, IPointerEnterHandler
  {
    private BtnInventory _btnInventory;

    private void Awake() { _btnInventory = GetComponentInParent<BtnInventory>(); }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (eventData.pointerDrag == null) return;
    
      ItemInSlot draggedItem = eventData.pointerDrag.GetComponent<ItemInSlot>();

      if (draggedItem == null)
      {
        Debug.LogWarning($"El objeto {eventData.pointerDrag.name} no tiene un componente ItemInSlot.");
        return;
      }
      
      Debug.Log(_btnInventory.inventoryBtnNumber);
      InventoryManager.Instance.ShowCntSlots(_btnInventory.inventoryBtnNumber);
    }
  }
}
