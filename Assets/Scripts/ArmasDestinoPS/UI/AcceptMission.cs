using ArmasDestinoPS.Managers;
using UnityEngine;

namespace ArmasDestinoPS.UI
{
  public class AcceptMission : MonoBehaviour
  {
    #region Properties
      [SerializeField] private string idMission;
      private NPCManager _npcManager;
    #endregion

    #region Constructors & Instance 
      private void Start() => _npcManager = NPCManager.Instance;
    #endregion

    #region Methods
      public void SetMissionID(string id) => idMission = id;

      public void MissionConfirmed() => _npcManager.AcceptMission(idMission);
    #endregion
  }
}