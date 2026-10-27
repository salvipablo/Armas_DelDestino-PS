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
      public void UpdateInventory(int numSection)
      {
        InventoryGame inventoryGame = InventoryGame.GetInventory();
        InventorySlot[] logicalSlots = inventoryGame.GetSection(numSection).GetSlots();
        
        SlotUI[] slotUis = cntSlots[numSection].GetComponentsInChildren<SlotUI>();
        
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
