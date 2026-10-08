using UnityEngine;
using ArmasDestinoPS.Inventory;

namespace ArmasDestinoPS.Managers
{
  public class ItemsManager : MonoBehaviour
  {
    #region Properties
      [SerializeField] private Sprite emptyIcon;
      [SerializeField] private Sprite woodIcon;
      [SerializeField] private Sprite stoneIcon;
      [SerializeField] private Sprite peppermintIcon;
      
      public static ItemsManager Instance { get; set; }
    #endregion

    #region Constructor & Singleton
      private void Awake()
      {
        Wood = new Item( 1, "Wood", true, "Description Wood", woodIcon);
        Stone = new Item( 2, "Stone", true, "Description Stone", stoneIcon);
        Peppermint = new Item( 3, "Peppermint", true, "Description Peppermint", peppermintIcon);
        
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
      }
    #endregion
    
    #region Methods
      public Item Wood { get; private set; }
      public Item Stone { get; private set; }
      public Item Peppermint { get; private set; }

      public Sprite GetSprite(string itemName)
      {
        if (itemName.Equals("Stone")) return Stone.Icon;
        if (itemName.Equals("Wood"))  return Wood.Icon;
        if (itemName.Equals("Peppermint")) return Peppermint.Icon;
        
        return emptyIcon;
      }
    #endregion
  }
}
