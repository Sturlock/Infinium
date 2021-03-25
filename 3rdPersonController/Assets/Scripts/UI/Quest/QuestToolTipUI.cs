using System.Collections;
using System.Collections.Generic;
using Infinium.Quests;
using UnityEngine;
using TMPro;

namespace Infinium.UI.Quests
{
    public class QuestToolTipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;

        public void Setup(Quest quest)
        {
            title.text = quest.GetTitle();
            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }
            foreach (string objective in quest.GetObjectives())
            {
                GameObject objectiveInstance = Instantiate(objectivePrefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective;
            }
        }
    }
}
