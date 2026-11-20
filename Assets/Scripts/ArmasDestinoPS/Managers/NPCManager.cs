using System.Collections.Generic;
using ArmasDestinoPs.Quests;
using ArmasDestinoPS.UI;
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
        if (!isTalkingNPC)
        {
          o_UiMissions.SetActive(isTalkingNPC);
          Cursor.lockState = isTalkingNPC ? CursorLockMode.None : CursorLockMode.Locked;
        }
      }

      public bool IsTalkingNPC() => isTalkingNPC;
      public void StopTalkingNPC() => isTalkingNPC = false;

      public void StartTalkNPC(List<QuestInstance> quests)
      {
        isTalkingNPC = true;
        Cursor.lockState = CursorLockMode.None;
        
        UpdateMissionsNPC updateMissionsNpc = o_UiMissions.GetComponent<UpdateMissionsNPC>();
        updateMissionsNpc.UpdateMissions(quests);
      }
    #endregion
  }
}
