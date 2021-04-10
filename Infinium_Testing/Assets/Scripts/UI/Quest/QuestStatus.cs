using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Infinium.Quests
{
    [System.Serializable]
    public class QuestStatus
    {
        [SerializeField] Quest quest;
        [SerializeField] List<string> completedObjectives;

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompleteedCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }
    }
}

