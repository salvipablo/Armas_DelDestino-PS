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
          // Tomo los sprites para cada item.
          Sprite iconItemEmpty = ItemsManager.Instance.GetSprite("");
          Sprite iconItemWood = ItemsManager.Instance.GetSprite("Wood");
          Sprite iconItemStone = ItemsManager.Instance.GetSprite("Stone");
          Sprite iconItemPeppermint = ItemsManager.Instance.GetSprite("Peppermint");
          Sprite iconItemBonfire = ItemsManager.Instance.GetSprite("Bonfire");
          Sprite iconItemCopperIngot = ItemsManager.Instance.GetSprite("Copper Ingot");
          Sprite iconItemSilverIngot = ItemsManager.Instance.GetSprite("Silver Ingot");
          Sprite iconItemCopperOre = ItemsManager.Instance.GetSprite("Copper Ore");
          Sprite iconItemSilverOre = ItemsManager.Instance.GetSprite("Silver Ore");
          
          // Creo los items a usar.
          Item itemEmpty = new Item(4, "Empty", true, "Item Empty", iconItemEmpty);
          Item itemWood = new Item(1, "Wood", true, "Description", iconItemWood);
          Item itemStone = new Item(2, "Stone", true, "Description", iconItemStone);
          Item itemPeppermint = new Item(3, "Peppermint", true, "Description", iconItemPeppermint);
          Item itemBonfire = new Item(3, "Bonfire", true, "Description", iconItemPeppermint);
          Item itemCopperIngot = new Item(3, "Copper Ingot", true, "Description", iconItemPeppermint);
          Item itemSilverIngot = new Item(3, "Silver Ingot", true, "Description", iconItemPeppermint);
          Item itemCopperOre = new Item(3, "Copper Ore", true, "Description", iconItemPeppermint);
          Item itemSilverOre = new Item(3, "Silver Ore", true, "Description", iconItemPeppermint);
          
          // Slots de la seccion 0.
          InventorySlot[] slots = _inventoryGame.GetSection(0).GetSlots();

          slots[0].SaveItem(itemWood, 5);
          slots[1].SaveItem(itemEmpty, 5);
          slots[2].SaveItem(itemEmpty, 5);
          slots[3].SaveItem(itemStone, 50);
          slots[4].SaveItem(itemEmpty, 5);
          slots[5].SaveItem(itemEmpty, 5);
          slots[6].SaveItem(itemEmpty, 5);
          slots[7].SaveItem(itemPeppermint, 2);
          slots[8].SaveItem(itemEmpty, 0);
          slots[9].SaveItem(itemEmpty, 0);
          slots[10].SaveItem(itemEmpty, 0);
          slots[11].SaveItem(itemEmpty, 0);
          slots[12].SaveItem(itemEmpty, 0);
          slots[13].SaveItem(itemEmpty, 0);
          slots[14].SaveItem(itemEmpty, 0);
          slots[15].SaveItem(itemEmpty, 0);
          slots[16].SaveItem(itemEmpty, 0);
          slots[17].SaveItem(itemEmpty, 0);
          slots[18].SaveItem(itemEmpty, 0);
          slots[19].SaveItem(itemEmpty, 0);

          // Slots de la seccion 1.
          slots = _inventoryGame.GetSection(1).GetSlots();

          slots[0].SaveItem(itemWood, 5);
          slots[1].SaveItem(itemEmpty, 5);
          slots[2].SaveItem(itemEmpty, 5);
          slots[3].SaveItem(itemStone, 50);
          slots[4].SaveItem(itemEmpty, 5);
          slots[5].SaveItem(itemEmpty, 5);
          slots[6].SaveItem(itemEmpty, 5);
          slots[7].SaveItem(itemPeppermint, 2);
          slots[8].SaveItem(itemEmpty, 0);
          slots[9].SaveItem(itemEmpty, 0);
          slots[10].SaveItem(itemEmpty, 0);
          slots[11].SaveItem(itemEmpty, 0);
          slots[12].SaveItem(itemEmpty, 0);
          slots[13].SaveItem(itemEmpty, 0);
          slots[14].SaveItem(itemEmpty, 0);
          slots[15].SaveItem(itemEmpty, 0);
          slots[16].SaveItem(itemEmpty, 0);
          slots[17].SaveItem(itemEmpty, 0);
          slots[18].SaveItem(itemEmpty, 0);
          slots[19].SaveItem(itemEmpty, 0);


          // Slots de la seccion 2.
          slots = _inventoryGame.GetSection(2).GetSlots();

          slots[0].SaveItem(itemWood, 5);
          slots[1].SaveItem(itemEmpty, 5);
          slots[2].SaveItem(itemEmpty, 5);
          slots[3].SaveItem(itemStone, 50);
          slots[4].SaveItem(itemEmpty, 5);
          slots[5].SaveItem(itemEmpty, 5);
          slots[6].SaveItem(itemEmpty, 5);
          slots[7].SaveItem(itemPeppermint, 2);
          slots[8].SaveItem(itemEmpty, 0);
          slots[9].SaveItem(itemEmpty, 0);
          slots[10].SaveItem(itemEmpty, 0);
          slots[11].SaveItem(itemEmpty, 0);
          slots[12].SaveItem(itemEmpty, 0);
          slots[13].SaveItem(itemEmpty, 0);
          slots[14].SaveItem(itemEmpty, 0);
          slots[15].SaveItem(itemEmpty, 0);
          slots[16].SaveItem(itemEmpty, 0);
          slots[17].SaveItem(itemEmpty, 0);
          slots[18].SaveItem(itemEmpty, 0);
          slots[19].SaveItem(itemEmpty, 0);
          
          // Slots de la seccion 3.
          slots = _inventoryGame.GetSection(3).GetSlots();

          slots[0].SaveItem(itemWood, 5);
          slots[1].SaveItem(itemEmpty, 5);
          slots[2].SaveItem(itemEmpty, 5);
          slots[3].SaveItem(itemStone, 50);
          slots[4].SaveItem(itemEmpty, 5);
          slots[5].SaveItem(itemEmpty, 5);
          slots[6].SaveItem(itemEmpty, 5);
          slots[7].SaveItem(itemPeppermint, 2);
          slots[8].SaveItem(itemEmpty, 0);
          slots[9].SaveItem(itemEmpty, 0);
          slots[10].SaveItem(itemEmpty, 0);
          slots[11].SaveItem(itemEmpty, 0);
          slots[12].SaveItem(itemEmpty, 0);
          slots[13].SaveItem(itemEmpty, 0);
          slots[14].SaveItem(itemEmpty, 0);
          slots[15].SaveItem(itemEmpty, 0);
          slots[16].SaveItem(itemEmpty, 0);
          slots[17].SaveItem(itemEmpty, 0);
          slots[18].SaveItem(itemEmpty, 0);
          slots[19].SaveItem(itemEmpty, 0);
          
          return "Inventario recargado";
        }
      #endregion
    #endregion
  }
}

