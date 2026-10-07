using UnityEngine;

namespace ArmasDestinoPS.Inventory
{
  public class ItemInSlot : MonoBehaviour
  {
    [SerializeField] private int inSection;
    [SerializeField] private int inSlot;
    [SerializeField] private string itemName;
    [SerializeField] private int qty;
    
    public int InSection => inSection;
    public int InSlot => inSlot;
    public string ItemName => itemName;
    public int Qty => qty;
  }
}

