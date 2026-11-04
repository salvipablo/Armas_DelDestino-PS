using System;
using ArmasDestinoPS.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArmasDestinoPS.Inventory
{
  public class ItemDropSlot : MonoBehaviour, IDropHandler
  {
    private ItemInSlot _itemInSlot;

    private void Awake()
    {
      _itemInSlot = GetComponentInParent<ItemInSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
      if (eventData.pointerDrag == null) return;

      ItemInSlot draggedItem = eventData.pointerDrag.GetComponent<ItemInSlot>();

      if (draggedItem == null)
      {
        Debug.LogWarning($"El objeto {eventData.pointerDrag.name} no tiene un componente ItemInSlot.");
        return;
      }
      
      Sprite iconItem = ItemsManager.Instance.GetSprite(draggedItem.ItemName);
      Item itemToBeStored = new Item(1, draggedItem.ItemName, true, "Description", iconItem);

      // Toda las condiciones, segun los items comparados, ejecutaran los enventos que estaran en InventoryManager.
      
      if (_itemInSlot.ItemName.Equals(draggedItem.ItemName) && draggedItem.Qty == 50 || _itemInSlot.Qty == 50) return;
      
      // Usando el ItemName, si el item que recibe esta vacio o los dos son del mismo tipo, llamar al evento mover.
      if (_itemInSlot.ItemName.Equals("Empty") || _itemInSlot.ItemName.Equals(draggedItem.ItemName))
      {
        InventoryManager.Instance.MoveItemFromSlot(draggedItem.InSection, draggedItem.InSlot, _itemInSlot.InSection,
                                                          _itemInSlot.InSlot, itemToBeStored, draggedItem.Qty);
      }
    }
  }
}


