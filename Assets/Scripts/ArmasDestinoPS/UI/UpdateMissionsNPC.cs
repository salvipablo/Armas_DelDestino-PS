using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using ArmasDestinoPs.Quests;

namespace ArmasDestinoPS.UI
{
  public class UpdateMissionsNPC : MonoBehaviour
  {
    [SerializeField] private GameObject cntSlots;
    
    public void UpdateMissions(List<QuestInstance> quests)
    {
      TextMeshProUGUI[] txtMissions = cntSlots.GetComponentsInChildren<TextMeshProUGUI>(true)
                                    .Where(t => t.gameObject.name == "TxtDescription") .ToArray();
      
      txtMissions[0].text = quests[0].questData.questName;
      txtMissions[1].text = quests[1].questData.questName;

      gameObject.SetActive(true);

      Debug.Log(quests[0].questData.questName);
      Debug.Log(quests[1].questData.questName);

      //Debug.Log("Updating Missions");
      //SlotUI[] slotUis = cntSlots[numSection].GetComponentsInChildren<SlotUI>();
    }
  }
}
