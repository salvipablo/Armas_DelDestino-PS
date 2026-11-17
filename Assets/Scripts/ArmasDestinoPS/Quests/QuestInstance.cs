using ArmasDestinoPS.Quests;

namespace ArmasDestinoPs.Quests
{
  [System.Serializable]
  public class QuestInstance
  {
    #region Properties
      public QuestData questData;
      public QuestStatus status;
    #endregion

    #region Constructors
      public QuestInstance(QuestData data)
      {
        questData = data;
        status = QuestStatus.Locked;
      }
    #endregion
  }
}
