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
      
      // Usando el ItemName, si los dos son de distino tipo, cancelar cualquier operacion ya que no se pueden juntar
      //if (draggedItem.ItemName == _itemInSlot.ItemName) return;
      
      // Usando el ItemName, si los dos son del mismo tipo, llamar al evento mover y que este verifique si se puede.
      if (_itemInSlot.ItemName.Equals(draggedItem.ItemName)) InventoryManager.Instance.MoveItemFromSlot(
        draggedItem.InSection, draggedItem.InSlot, _itemInSlot.InSection, _itemInSlot.InSlot,
                                                                            itemToBeStored, draggedItem.Qty);
      
      
      // Usando el ItemName, si el item que recibe esta vacio, llamar al evento mover.
      if (_itemInSlot.ItemName.Equals("Empty")) InventoryManager.Instance.MoveItemFromSlot(draggedItem.InSection,
              draggedItem.InSlot, _itemInSlot.InSection, _itemInSlot.InSlot, itemToBeStored, draggedItem.Qty);


      
      // Debug.Log($"Item recibido: {draggedItem.ItemName}");
      // Debug.Log($"De la Sección: {draggedItem.InSection}");
      // Debug.Log($"Del Slot: {draggedItem.InSlot}");
      // Debug.Log($"Y con una cantidad de: {draggedItem.Qty}");
      //
      // Debug.Log($"--------------------------------------------------------");
      //
      // Debug.Log($"El que recibe es un item de nombre: {_itemInSlot.ItemName}");
      // Debug.Log($"Soy de la Sección: {_itemInSlot.InSection}");
      // Debug.Log($"Estoy en el Slot: {_itemInSlot.InSlot}");
      // Debug.Log($"Y tengo la Cantidad de: {_itemInSlot.Qty}");
    }
  }
}


