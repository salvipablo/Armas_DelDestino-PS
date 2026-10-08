using UnityEngine;

namespace ArmasDestinoPS.Inventory
{
  public class InventorySlot
  {
    #region Properties
      private Item ItemInSlot;

      public int Quantity { get; set; }

      public int MaximumQuantity { get; set; }
    #endregion

    #region Constructor
      public InventorySlot() { MaximumQuantity = 50; }

      public InventorySlot(Item item, int amount = 1)
      {
        ItemInSlot = item;
        Quantity = amount;
        MaximumQuantity = 50;
      }
    #endregion
    
    #region Methods
      public bool IsItEmpty() => ItemInSlot == null;

      public string GetItemName()
      {
        if (ItemInSlot == null) return string.Empty;
        return ItemInSlot.Name;
      }

      public void SaveItem(Item item) => ItemInSlot = item;

      public void SaveItem(Item item, int amount)
      {
        ItemInSlot = item;
        Quantity = amount;
      }

      public int AddQuantity(int amount)
      {
        int amountToAdd = amount;
        int howMuchCanI = MaximumQuantity - Quantity;

        if (amountToAdd > howMuchCanI)
        {
          Quantity += howMuchCanI;
          amountToAdd -= howMuchCanI;
        }
        else
        {
          Quantity += amountToAdd;
          amountToAdd = 0;
        }

        return amountToAdd;
      }

      public void RemoveItem()
      {
        ItemInSlot = null;
        Quantity = 0;
      }

      public void SubtractAmount(int amount) => Quantity -= amount;

      public Sprite GetIcon() => ItemInSlot.Icon;
    #endregion
  }
}
