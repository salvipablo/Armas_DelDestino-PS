using UnityEngine;
using ArmasDestinoPS.Managers;
using ArmasDestinoPS.Quests;

namespace ArmasDestinoPS.NPCs
{
  public class NPC : MonoBehaviour
  {
    #region Properties
      [SerializeField] private string npcName;
      [SerializeField] private string npcID;
      
      private static NPCManager _instance;
    #endregion

    #region Methods
      private void Start() =>_instance = NPCManager.Instance;

      public string NPCName => npcName;

      public void StartConversation()
      {
        var quests = QuestManager.Instance.GetQuestsByNPC("GT_Bree");        
        _instance.StartTalkNPC(quests);
      }
    #endregion
  }
}
