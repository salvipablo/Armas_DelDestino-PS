namespace ArmasDestinoPS.Inventory
{
  public class InventorySection
  {
    #region Properties
      private InventorySlot[] Slots;
    #endregion

    #region Constructor
      public InventorySection(int size = 20)
      {
        Slots = new InventorySlot[size];

        // Inicializo cada InventorySection, porque la sentencia de arriba solo las declara.
        for (int i = 0; i < Slots.Length; i++) Slots[i] = new InventorySlot();
      }
    #endregion
    
    #region Methods
      public InventorySlot GetSlot(int index) => Slots[index];
      public InventorySlot[] GetSlots() => Slots;

      public int FindFirstEmpty()
      {
        for (int i = 0; i < Slots.Length; i++) if (Slots[i].IsItEmpty()) return i;
        return -1;
      }

      public int FindItem(Item item, int slotNumberFromSearch)
      {
        for (int i = slotNumberFromSearch; i < Slots.Length; i++) if (Slots[i].GetItemName() == item.Name) return i;
        return -1;
      }

      public int addItemToExistingOne(int slotNumber, int amount)
      {
        int amountToAdd = amount;
        int whatQuantityHave = Slots[slotNumber].Quantity;
      
        if (whatQuantityHave < Slots[slotNumber].MaximumQuantity)
        {
          if ((whatQuantityHave + amountToAdd) > Slots[slotNumber].MaximumQuantity)
          {
            Slots[slotNumber].Quantity = 50;
            int HowMuchDoIStore = 50 - whatQuantityHave;
            amountToAdd -= HowMuchDoIStore;
          }
          else
          {
            Slots[slotNumber].Quantity += amountToAdd;
            amountToAdd = 0; 
          }
        }

        return amountToAdd;
      }

      public string AddItemEmptySlot(Item item, int slotNumber, int amount)
      {
        Slots[slotNumber].SaveItem(item, amount);

        return "Item desplazado al slot.";
      }
    #endregion
  }
}
