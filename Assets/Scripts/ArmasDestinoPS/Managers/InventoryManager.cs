using UnityEngine;
using ArmasDestinoPS.Inventory;

namespace ArmasDestinoPS.Managers
{
  public class InventoryManager : MonoBehaviour
  {
    #region Properties
      public static InventoryManager Instance { get; set; }
      [SerializeField] private GameObject inventory;
      public bool TheInventoryOpen;
      private InventoryGame _inventoryGame;
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

      public void MoveItemFromSlot(int fromWhichSectionDrag, int fromWhichSlotDrag, int whichDragSection,
                                                                            int whichDragSlot, Item item, int quantity)
      {
        string statusOp = _inventoryGame.PlaceItemInSlot(fromWhichSectionDrag, fromWhichSlotDrag, whichDragSection,
                                                                                        whichDragSlot, item, quantity);
        Debug.Log(statusOp);
      }
      
      public string AddItemToSlot(Item item, int amount)
      {
        string statusOp = _inventoryGame.AddItem(item, amount);
        return statusOp;
      }

      #region Developer
        public string AddItemToSlotDeveloper(Item item, int amount, int section, int numberSlot)
        {
          string statusOp = _inventoryGame.AddItemForDeveloper(item, amount, section, numberSlot);
          return statusOp;
        }
      #endregion
    #endregion
  }
}

