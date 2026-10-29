using UnityEngine;
using ArmasDestinoPS.Inventory;
using ArmasDestinoPS.UI;

namespace ArmasDestinoPS.Managers
{
  public class InventoryManager : MonoBehaviour
  {
    #region Properties
      public static InventoryManager Instance { get; set; }
      [SerializeField] private GameObject inventory;
      public bool TheInventoryOpen;
      private InventoryGame _inventoryGame;
      [SerializeField] private InventoryUI inventoryUI;
      public int actualSection = 0;

      [SerializeField] private GameObject[] cntsSlots;
    #endregion

    #region Methods
      private void Awake()
      {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
      }

      private void Start()
      {
        TheInventoryOpen = false;
        _inventoryGame = InventoryGame.GetInventory();
        string statusFillInventory = FillDeveloperInventorySlots();
        
        if (statusFillInventory == "Inventario recargado") inventoryUI.UpdateInventory(actualSection);
      }

      void Update()
      {
        if (Input.GetButtonDown("Inventory") && !TheInventoryOpen)
        {
          inventory.SetActive(true);
          TheInventoryOpen = true;
          Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetButtonDown("Inventory") && TheInventoryOpen)
        {
          inventory.SetActive(false);
          TheInventoryOpen = false;
          Cursor.lockState = CursorLockMode.Locked;
        }
      }

      public void ShowCntSlots(int section)
      {
        cntsSlots[0].SetActive(false);
        cntsSlots[1].SetActive(false);
        cntsSlots[2].SetActive(false);
        cntsSlots[3].SetActive(false);
        
        cntsSlots[section - 1].SetActive(true);
      }
      
      public void MoveItemFromSlot(int fromWhichSectionDrag, int fromWhichSlotDrag, int whichDragSection,
                                                                            int whichDragSlot, Item item, int quantity)
      {
        string statusOp = _inventoryGame.PlaceItemInSlot(fromWhichSectionDrag - 1, fromWhichSlotDrag - 1,
                                                              whichDragSection - 1, whichDragSlot - 1, item, quantity);
        Debug.Log(statusOp);
      }
      
      public string AddItemToSlot(Item item, int amount)
      {
        // Envio el item a agregar al inventario logico
        string statusOp = _inventoryGame.AddItem(item, amount);
        
        // Si se almaceno el item llamo a la actualizacion del inventario.
        if (statusOp == "Item agregado al inventario") inventoryUI.UpdateInventory(actualSection);
        
        // Retorno el estado para que el metodo CheckInteraction(), sepa que paso y en caso de ser agregado pueda
        // eliminarse el  item del mundo.
        return statusOp;
      }

      #region Developer
        public string AddItemToSlotDeveloper(Item item, int amount, int section, int numberSlot)
        {
          // Envio el item a agregar al inventario logico, al metodo developer.
          string statusOp = _inventoryGame.AddItemForDeveloper(item, amount, section, numberSlot);

          // Si se almaceno el item llamo a la actualizacion del inventario.
          if (statusOp == "Item agregado al inventario") inventoryUI.UpdateInventory(actualSection);
          
          // Retorno el estado para que el metodo CheckInteraction(), sepa que paso y en caso de ser agregado pueda
          // eliminarse el  item del mundo.
          return statusOp;
        }

        public string FillDeveloperInventorySlots()
        {
          InventorySlot[] slots = _inventoryGame.GetSection(0).GetSlots();
          
          Sprite iconItemEmpty = ItemsManager.Instance.GetSprite("");
          Sprite iconItemWood = ItemsManager.Instance.GetSprite("Wood");
          Sprite iconItemStone = ItemsManager.Instance.GetSprite("Stone");
          Sprite iconItemPeppermint = ItemsManager.Instance.GetSprite("Peppermint");
          
          Item itemWood = new Item(1, "Wood", true, "Description", iconItemWood);
          Item itemStone = new Item(2, "Stone", true, "Description", iconItemStone);
          Item itemPeppermint = new Item(3, "Peppermint", true, "Description", iconItemPeppermint);
          Item itemEmpty = new Item(4, "Empty", true, "Item Empty", iconItemEmpty);

          for (int i = 0; i < slots.Length; i++)
          {
            
            if (i == 0)
            {
              slots[i].SaveItem(itemWood, 5);
              continue;
            }

            if (i == 3)
            {
              slots[i].SaveItem(itemStone, 50);
              continue;
            }

            if (i == 7)
            {
              slots[i].SaveItem(itemPeppermint, 2);
              continue;
            }
            
            slots[i].SaveItem(itemEmpty, 0);
          }

          return "Inventario recargado";
        }
      #endregion
    #endregion
  }
}

