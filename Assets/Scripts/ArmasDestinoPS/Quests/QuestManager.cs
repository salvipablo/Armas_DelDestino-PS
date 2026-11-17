using System.Collections.Generic;
using System.Linq;
using ArmasDestinoPs.Quests;
using UnityEngine;

namespace ArmasDestinoPS.Quests
{
  public class QuestManager : MonoBehaviour
  {
    #region Properties
      public static QuestManager Instance { get; private set; }
      public List<QuestData> allQuests;
      private List<QuestInstance> playerQuests = new List<QuestInstance>();
    #endregion

    #region Constructors & Instance
      private void Awake()
      {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        foreach (QuestData quest in allQuests) { playerQuests.Add(new QuestInstance(quest)); }
        
        playerQuests[0].status = QuestStatus.Available;
        playerQuests[1].status = QuestStatus.Available;
      }
    #endregion
    
    #region Methods
      public void ActivateQuest(string questID)
      {
        QuestInstance quest = playerQuests.Find(q => q.questData.questID == questID);

        if (quest != null && quest.status == QuestStatus.Available)
        {
          quest.status = QuestStatus.Active;
        }
      }

      public void CompleteQuest(string questID)
      {
        QuestInstance quest = playerQuests.Find(
          q => q.questData.questID == questID
        );

        if (quest != null &&
            quest.status == QuestStatus.Active)
        {
          quest.status = QuestStatus.Completed;
        }
      }

      public List<QuestInstance> GetQuestsByNPC(string npcID)
      {
        return playerQuests
          .Where(q => q.questData.npcID == npcID && q.status == QuestStatus.Available)
          .ToList();
      }
    #endregion
  }
}
