using Infinium.Core;
using Infinium.Resources;
using Infinium.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    public class QuestList : MonoBehaviour, IPredicateEvaluator
    {
        List<QuestStatus> statuses = new List<QuestStatus>();
        public event Action onUpdate;

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            {
            }
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            if(onUpdate != null)
            {
                onUpdate();
            }
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (status.isComplete())
            {
                GiveRewards(quest);
            }
            if (onUpdate != null)
            {
                onUpdate();
            }
        }


        private bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }
        private void GiveRewards(Quest quest)
        {
            foreach (var reward in quest.GetRewards())
            {
                bool success = true; // = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                if (!success)
                {
                    //GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate != "HasQuest") return null;

            return HasQuest(Quest.GetByName(parameters[0]));
        }
    }
}
