using System;
using UnityEngine;

namespace ArmasDestinoPS.Managers
{
  public class NPCManager : MonoBehaviour
  {
    #region Properties
      public static NPCManager Instance { get; set; }
    
      private bool isTalkingNPC = false;
    #endregion

    #region Methods
      private void Awake()
      {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
      }

      private void Update()
      {
        if (isTalkingNPC) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
      }

      public bool IsTalkingNPC() => isTalkingNPC;
      public void StartTalkNPC() => isTalkingNPC = true;
    #endregion
  }
}
