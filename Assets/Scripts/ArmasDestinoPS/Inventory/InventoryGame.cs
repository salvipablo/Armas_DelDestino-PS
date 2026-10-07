namespace ArmasDestinoPS.Inventory
{
  public class InventoryGame
  {
    #region Properties
      private static InventoryGame _inventory;

      private InventorySection[] Sections;
    #endregion

    #region ConstructorAndSingleton
      private InventoryGame(int numSections = 4)
      {
        Sections = new InventorySection[numSections];

        // Inicializo cada InventorySection, porque la sentencia de arriba solo las declara.
        for (int i = 0; i < Sections.Length; i++) Sections[i] = new InventorySection(20);
      }

      public static InventoryGame GetInventory(int numSections = 4)
      {
        if (_inventory == null) return new InventoryGame(numSections);
        return _inventory;
      }
    #endregion
    
    #region Methods
      #region Developer
        /*
         * Este metodo es para ser utilizado por los desarrolladores para agregar items en la seccion que uno desee
         * y en el slot que uno desee. Este metodo es para testing
         * TODO: Este metodo en el futuro podria ser eliminado.
         */
        public string AddItemForDeveloper(Item item, int amount, int section, int numberSlot)
        {
          // Obtengo de una seccion, el slot que peticiono y envio a guardar el item.
          InventorySlot slot = Sections[section].GetSlot(numberSlot);
          slot.SaveItem(item);
          slot.Quantity = amount;

          return "Item agregado al inventario";
        }
      #endregion

      #region Add item to inventory, "game officer"
        public string AddItem(Item item, int amount)
        {
          int totalQuantityToStore = amount;

          // Recorro las secciones, buscando si el item existe y se intenta ir sumandole.
          int amountToAdd = SearchForItemAndAddQuantity(item, amount);

          if (amountToAdd == 0) return "Item agregado al inventario";

          // Si no existe el item en el inventario se coloca en uno vacio.
          amountToAdd = AddItemToEmptySlot(item, amountToAdd);

          if (amountToAdd > 0 && amountToAdd < totalQuantityToStore) return "No se pudieron almacenar todos los items";
          if (amountToAdd > 0 && amountToAdd == totalQuantityToStore) return "Inventario lleno";

          return "Item agregado al inventario";
        }

        private int SearchForItemAndAddQuantity(Item item, int amountAdd)
        {
          int amountToAdd = amountAdd;
          int slotNumberToSearch = 0;

          for (int i = 0; i < Sections.Length; i++)
          {
            while (slotNumberToSearch != -1 && amountToAdd > 0)
            {
              slotNumberToSearch = Sections[i].FindItem(item, slotNumberToSearch);
              if (slotNumberToSearch != -1) amountToAdd = Sections[i].addItemToExistingOne(slotNumberToSearch, amountToAdd);
              if (slotNumberToSearch != -1) slotNumberToSearch++;
            }

            if (amountToAdd == 0) break;
            slotNumberToSearch = 0;
          }

          return amountToAdd;
        }

        private int AddItemToEmptySlot(Item item, int amount)
        {
          int amountToAdd = amount;
          int emptySlotNumber = 0;

          for (int i = 0; i < Sections.Length; i++)
          {
            while (emptySlotNumber != -1 && amountToAdd != 0)
            {
              emptySlotNumber = Sections[i].FindFirstEmpty();

              if (emptySlotNumber == -1) break;
              else
              {
                if (amountToAdd > 50)
                {
                  Sections[i].AddItemEmptySlot(item, emptySlotNumber, 50);
                  amountToAdd -= 50;
                }
                else
                {
                  Sections[i].AddItemEmptySlot(item, emptySlotNumber, amountToAdd);
                  amountToAdd -= amountToAdd;
                }

                if (amountToAdd == 0) break;
              }
            }

            emptySlotNumber = 0;
          }

          return amountToAdd;
        }
      #endregion

      public InventorySection GetSection(int numSection) => Sections[numSection];

      public string RemoveItem(int numberSection, int numberslot)
      {
        InventorySlot slot = Sections[numberSection].GetSlot(numberslot);

        slot.RemoveItem();

        return "Item eliminado correctamente del inventario.";
      }

      public string PlaceItemInSlot(int fromWhichSectionDrag, int fromWhichSlotDrag, int whichDragSection, int whichDragSlot, Item item, int quantity)
      {
        // Slot hacia donde arrastre el item.
        InventorySlot slotWhereDrag = Sections[whichDragSection].GetSlot(whichDragSlot);

        // Si el slot esta vacio, colocarlo.
        if (slotWhereDrag.Quantity == 0)
        {
          // Elimino el item en el slot, desde donde arrastre.
          RemoveItem(fromWhichSectionDrag, fromWhichSlotDrag);

          // Envio a almacenar al slot que arrastre
          return Sections[whichDragSection].AddItemEmptySlot(item, whichDragSlot, quantity);
        }
        else
        {
          // Intentar colocar item y su cantidad en el slot
          int whatQuantityRemained = AddItemInSlotWithItem(whichDragSlot, whichDragSection, item, quantity);

          // Si la cantidad que quedo es la misma a la inicial, el slot no recibio ninguna cantidad porque esta lleno.
          if (whatQuantityRemained == quantity) return "Slot lleno, o item diferente.";

          // Si la cantidad que quedo es 0, el item fue desplazado al slot donde fue arrastrado.
          if (whatQuantityRemained == 0)
          {
            RemoveItem(fromWhichSectionDrag, fromWhichSlotDrag);
            return "Item desplazado al slot.";
          }

          // Si llego hasta aqui, la cantidad no es 0, pero es distinta a la cantidad inicial, osea se desplazo una cierta cantidad.
          int howMuchToSubtract = quantity - whatQuantityRemained;
          SubtractAmountInSlot(fromWhichSectionDrag, fromWhichSlotDrag, howMuchToSubtract);

          return $"Se almacenaron: {howMuchToSubtract} del item.";  
        }
      }

      public void SubtractAmountInSlot(int numberSection, int numberslot, int amount)
      {
        InventorySlot slot = Sections[numberSection].GetSlot(numberslot);
        slot.SubtractAmount(amount);
      }

      /*
       * ** Español **
       * Esta funcion se encarga de sumar cantidad, al slot donde el item fue arrastrado.
       * Retorno de la funcion: La cantidad que sobro al sumar en el slot. Podria ser 0 si se almacena todo.
       *
       * ** English **
       * This function adds quantity to the slot where the item was dragged.
       * Function return: The amount remaining after adding in the slot. It could be 0 if everything is stored.
      */
      private int AddItemInSlotWithItem(int whichSlotToAdd, int whichSection, Item item, int quantity)
      {
        InventorySlot slot = Sections[whichSection].GetSlot(whichSlotToAdd);

        if (item.Name != slot.GetItemName()) return quantity;
        else return slot.AddQuantity(quantity);
      }
    #endregion
  }
}