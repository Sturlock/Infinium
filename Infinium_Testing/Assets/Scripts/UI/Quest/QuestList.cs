using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Quests
{
    public class QuestList : MonoBehaviour
    {
        [SerializeField] QuestStatus[] statuses;
        // Start is called before the first frame update

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }
    }
}
