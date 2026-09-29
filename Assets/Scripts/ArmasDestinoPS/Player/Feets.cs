using UnityEngine;

namespace ArmasDestinoPS.Player
{
  public class Feets : MonoBehaviour
  {
    #region MyRegion
      private PlayerMovements _playerMovements;
    #endregion
    
    #region Methods
      private void Awake()
      {
        _playerMovements = GetComponentInParent<PlayerMovements>();
        if (_playerMovements == null) Debug.LogError("PlayerMovements were not found on Feet's parent.");
      }

      private void OnTriggerEnter(Collider collidedObject)
      {
        if (collidedObject.gameObject.tag.Equals("Land")) _playerMovements.itCanJump = true;
      }
    #endregion
  }
}
