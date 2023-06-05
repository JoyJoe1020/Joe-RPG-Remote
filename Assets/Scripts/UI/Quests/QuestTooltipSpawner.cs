using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;  // 可以创建提示框
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();  // 获取任务项的任务状态
            tooltip.GetComponent<QuestTooltipUI>().Setup(status);  // 设置提示框的信息
        }
    }
}