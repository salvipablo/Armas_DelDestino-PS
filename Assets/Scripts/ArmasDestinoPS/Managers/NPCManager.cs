using System;
using UnityEngine;

namespace ArmasDestinoPS.Managers
{
  public class NPCManager : MonoBehaviour
  {
    #region Properties
      public static NPCManager Instance { get; set; }
    
      private bool isTalkingNPC = false;
      
      [SerializeField] private GameObject o_UiMissions;
    #endregion

    #region Methods
      private void Awake()
      {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
      }

      private void Update()
      {
        if (isTalkingNPC) ShowMissionUI(true);
        else ShowMissionUI(false);
      }

      private void ShowMissionUI(bool showUI)
      {
        o_UiMissions.SetActive(showUI);
        Cursor.lockState = showUI ? CursorLockMode.None : CursorLockMode.Locked;
      }

      public bool IsTalkingNPC() => isTalkingNPC;
      public void StartTalkNPC() => isTalkingNPC = true;
    #endregion
  }
}
