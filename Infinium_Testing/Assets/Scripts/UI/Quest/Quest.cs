using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Infinium/Quests", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives;

        public string GetTitle()
        {
            return name;
        }
        public int GetObjectiveCount()
        {
            return objectives.Length;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

    }
}
