using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Infinium/Quests", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objectives> objectives = new List<Objectives>();
        [SerializeField] List<Reward> rewards = new List<Reward>();

        [System.Serializable]
        class QuestStatusRecord
        {
            //Return to Savable Quest Progression to finish
            //Also build Save System
            string questName;
            List<string> completedObjectives;
        }
        
        [System.Serializable]
        public class Reward
        {
            [Min(1)]
            public int number;
            //public InventoryItem item;
            public int gold;
        }
        [System.Serializable]
        public class Objectives
        {
            public string reference;
            public string description;
        }
        public string GetTitle()
        {
            return name;
        }
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objectives> GetObjectives()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var objectives in objectives)
            {
                if (objectives.reference == objectiveRef)
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }
            return null;

        }
    }
}
