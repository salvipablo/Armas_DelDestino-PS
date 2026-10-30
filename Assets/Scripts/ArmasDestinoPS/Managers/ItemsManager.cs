using UnityEngine;

namespace ArmasDestinoPS.Managers
{
  public class ItemsManager : MonoBehaviour
  {
    #region Properties
      [SerializeField] private Sprite emptyIcon;
      
      [SerializeField] private Sprite woodIcon;
      [SerializeField] private Sprite stoneIcon;
      [SerializeField] private Sprite peppermintIcon;
      
      [SerializeField] private Sprite bonfireIcon;
      
      [SerializeField] private Sprite copperIngotIcon;
      [SerializeField] private Sprite silverIngotIcon;
      
      [SerializeField] private Sprite copperOreIcon;
      [SerializeField] private Sprite silverOreIcon;
      
      public static ItemsManager Instance { get; set; }
    #endregion

    #region Constructor & Singleton
      private void Awake()
      {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
      }
    #endregion
    
    #region Methods
      public Sprite GetSprite(string itemName)
      {
        if (itemName.Equals("Wood"))  return woodIcon;
        if (itemName.Equals("Stone")) return stoneIcon;
        if (itemName.Equals("Peppermint")) return peppermintIcon;
        
        if (itemName.Equals("Bonfire")) return bonfireIcon;
        
        if (itemName.Equals("Copper Ingot")) return copperIngotIcon;
        if (itemName.Equals("Silver Ingot")) return silverIngotIcon;
        
        if (itemName.Equals("Copper Ore")) return copperOreIcon;
        if (itemName.Equals("Silver Ore")) return silverOreIcon;
        
        return emptyIcon;
      }
    #endregion
  }
}
