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
        if (itemName.Equals("Stone")) return stoneIcon;
        if (itemName.Equals("Wood"))  return woodIcon;
        if (itemName.Equals("Peppermint")) return peppermintIcon;
        
        return emptyIcon;
      }
    #endregion
  }
}
