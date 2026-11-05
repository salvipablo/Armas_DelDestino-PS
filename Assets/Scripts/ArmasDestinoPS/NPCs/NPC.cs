using UnityEngine;
using ArmasDestinoPS.Managers;

namespace ArmasDestinoPS.NPCs
{
  public class NPC : MonoBehaviour
  {
    #region Properties
      [SerializeField] private string npcName;
      
      private static NPCManager _instance;
    #endregion

    #region Methods
      private void Awake() =>_instance = NPCManager.Instance;

      public string NPCName => npcName;

      public void StartConversation()
      {
        _instance.StartTalkNPC();
        
        Debug.Log("Estoy interactuando con un NPC.");
      }
    #endregion
  }
}
