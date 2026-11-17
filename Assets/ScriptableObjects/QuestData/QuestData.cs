using UnityEngine;

[CreateAssetMenu(
  fileName = "NewQuest",
  menuName = "Game/Quest"
)]
public class QuestData : ScriptableObject
{
  public string questID;

  public string questName;
  
  [TextArea(3, 6)]
  public string description;

  public string npcID;

  public QuestData[] prerequisites;

  public int reward;
}