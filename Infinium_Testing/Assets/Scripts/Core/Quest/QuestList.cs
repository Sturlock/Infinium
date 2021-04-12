using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    public class QuestList : MonoBehaviour
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

        private bool HasQuest(Quest quest)
        {
            foreach(QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }
    }
}
