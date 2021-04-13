using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Infinium/Quests", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<string> objectives = new List<string>();

        public string GetTitle()
        {
            return name;
        }
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

        internal bool HasObjective(string objective)
        {
            return objective.Contains(objective);
        }
    }
}
