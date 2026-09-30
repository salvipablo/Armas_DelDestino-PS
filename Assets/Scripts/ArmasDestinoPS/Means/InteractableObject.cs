using UnityEngine;

namespace ArmasDestinoPS.Means
{
  public class InteractableObject : MonoBehaviour
  {
    #region Properties
      [SerializeField] private string objectName;
    #endregion

    #region Methods
      public string ObjectName => objectName;
    #endregion
  }
}
