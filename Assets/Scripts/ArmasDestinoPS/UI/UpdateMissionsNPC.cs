using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ArmasDestinoPs.Quests;

namespace ArmasDestinoPS.UI
{
  public class UpdateMissionsNPC : MonoBehaviour
  {
    [SerializeField] private GameObject cntSlots;

    public void UpdateMissions(List<QuestInstance> quests)
    {
      TextMeshProUGUI[] txtMissions = cntSlots
        .GetComponentsInChildren<TextMeshProUGUI>(true)
        .Where(t => t.gameObject.name == "TxtDescription")
        .ToArray();

      for (int i = 0; i < txtMissions.Length; i++)
      {
        Button btnAcept = txtMissions[i]
          .transform.parent
          .GetComponentInChildren<Button>(true);

        bool hasMission = quests != null &&
                          i < quests.Count &&
                          quests[i] != null &&
                          quests[i].questData != null;

        txtMissions[i].gameObject.SetActive(hasMission);

        if (btnAcept != null)
        {
          btnAcept.gameObject.SetActive(hasMission);

          if (hasMission)
          {
            AcceptMission acceptMission =
              btnAcept.GetComponent<AcceptMission>();

            if (acceptMission != null)
            {
              acceptMission.SetMissionID(
                quests[i].questData.questID
              );
            }
          }
        }

        if (hasMission)
        {
          txtMissions[i].text = quests[i].questData.questName;
        }
      }

      gameObject.SetActive(true);
    }
  }
}