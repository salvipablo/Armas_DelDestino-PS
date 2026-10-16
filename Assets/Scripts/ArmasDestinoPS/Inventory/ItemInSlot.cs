using UnityEngine;

namespace ArmasDestinoPS.Inventory
{
  public class ItemInSlot : MonoBehaviour
  {
    #region Properties
      [SerializeField] private int inSection;
      [SerializeField] private int inSlot;
      [SerializeField] private string itemName;
      [SerializeField] private int qty;
    #endregion

    #region Methods
      public int InSection => inSection;
      public int InSlot => inSlot;
      public string ItemName => itemName;
      public int Qty => qty;

      public void SetSection(int section) => inSection = section;
      public void SetSlot(int slot) => inSlot = slot;
      public void UpdateItemName(string newItemName) => itemName = newItemName;
      public void UpdateQty(int newQty) => qty = newQty;
    #endregion
  }
}

