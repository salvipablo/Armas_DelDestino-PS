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

      ItemInSlot itemInSlot = eventData.pointerDrag.GetComponent<ItemInSlot>();

      if (itemInSlot == null)
      {
        Debug.LogWarning($"El objeto {eventData.pointerDrag.name} no tiene un componente ItemInSlot.");
        return;
      }
      
      // Toda las condiciones, segun los items comparados, ejecutaran los enventos que estaran en InventoryManager.
      
      // Usando el ItemName, si los dos son de distino tipo, cancelar cualquier operacion ya que no se pueden juntar
      if (itemInSlot.ItemName == _itemInSlot.ItemName) return;
      
      // Usando el ItemName, si los dos son del mismo tipo, llamar al evento agregar enviando datos para verificiar cantidades y demas
      
      // Usando el ItemName, si el item que recibe esta vacio, llamar al evento mover.
      Item itemToBeStored = new Item(itemInSlot.ItemName);
      if (_itemInSlot.ItemName.Equals("Empty")) InventoryManager.Instance.MoveItemFromSlot(itemInSlot.InSection,
                  itemInSlot.InSlot, _itemInSlot.InSection, _itemInSlot.InSlot, itemToBeStored, itemInSlot.Qty);

      // Debug.Log($"Item recibido: {itemInSlot.ItemName}");
      // Debug.Log($"Sección: {itemInSlot.InSection}");
      // Debug.Log($"Slot: {itemInSlot.InSlot}");
      // Debug.Log($"Cantidad: {itemInSlot.Qty}");
      //
      // Debug.Log($"--------------------------------------------------------");
      //
      // Debug.Log($"Yo so un item de nombre: {_itemInSlot.ItemName}");
      // Debug.Log($"Soy de la Sección: {_itemInSlot.InSection}");
      // Debug.Log($"Estoy en el Slot: {_itemInSlot.InSlot}");
      // Debug.Log($"Y tengo la Cantidad: {_itemInSlot.Qty}");
    }
  }
}


