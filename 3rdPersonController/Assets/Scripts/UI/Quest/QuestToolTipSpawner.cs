using Infinium.Core.UI.Tooltips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infinium.Quests;

namespace Infinium.UI.Quests
{
    public class QuestToolTipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            Quest quest = GetComponent<QuestItemUI>().GetQuest();
            tooltip.GetComponent<QuestToolTipUI>().Setup(quest);
        }
    }
}
