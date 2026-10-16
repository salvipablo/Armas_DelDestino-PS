using UnityEngine;
using ArmasDestinoPS.Inventory;

namespace ArmasDestinoPS.UI
{
  public class InventoryUI : MonoBehaviour
  {
    #region Properties
      [SerializeField] private GameObject[] cntSlots;
    #endregion

    #region Methods
      public void UpdateInventory(int numSection, InventoryGame inventoryGame)
      {
        Debug.Log("Updating Inventory");
        
        InventorySlot[] logicalSlots = inventoryGame.GetSection(numSection).GetSlots();
        
        Debug.Log($"Estoy en la seccion: {numSection}");
        Debug.Log($"Item en la posicion 1 del array: {logicalSlots[1].GetItemName()}");
        Debug.Log($"Cantidad: {logicalSlots[1].Quantity}");
        
        Debug.Log($"Estoy en la seccion: {numSection}");
        Debug.Log($"Item en la posicion 0 del array: {logicalSlots[0].GetItemName()}");
        Debug.Log($"Cantidad: {logicalSlots[0].Quantity}");
        Debug.Log($"Icono: {logicalSlots[0].GetIcon()}");

        
        // Obtengo los slots de la UI.
        SlotUI[] slotUis = cntSlots[numSection].GetComponentsInChildren<SlotUI>();
        
        // Recorro los slots de la UI.
        for (int i = 0; i < slotUis.Length; i++)
        {
          if (logicalSlots[i].IsItEmpty()) slotUis[i].SetEmpty(logicalSlots[i].GetItem());
          
          else slotUis[i].SetItem(logicalSlots[i].GetItem(), logicalSlots[i].GetQuantity());
          
          slotUis[i].SetItemInSlot(numSection + 1, i + 1, logicalSlots[i].GetItemName(), logicalSlots[i].GetQuantity());
        }
      }
    #endregion
  }
}
