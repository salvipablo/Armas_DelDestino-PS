using UnityEngine;

namespace ArmasDestinoPS.Means
{
  public class InteractableObject : MonoBehaviour
  {
    #region Properties
      [SerializeField] private string objectName;
      [SerializeField] private bool isItStorable;
      [SerializeField] private int qty;
      [SerializeField] private int whichSection;
      [SerializeField] private int whichSlot;
    #endregion

    #region Methods
      public string ObjectName => objectName;
      public bool IsItStorable => isItStorable;
      public int Qty => qty;
      public int WhichSection => whichSection;
      public int WhichSlot => whichSlot;
    #endregion
  }
}
