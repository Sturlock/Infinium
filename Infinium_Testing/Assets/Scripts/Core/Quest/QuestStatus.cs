using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Infinium.Quests
{
    
    public class QuestStatus
    {
        Quest quest;
        List<string> completedObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

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

        public void CompleteObjective(string objective)
        {
           if (quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
            
        }
    }
}

