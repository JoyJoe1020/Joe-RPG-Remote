using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;  // 标题文本
        [SerializeField] Transform objectiveContainer;  // 目标容器
        [SerializeField] GameObject objectivePrefab;  // 目标预制体
        [SerializeField] GameObject objectiveIncompletePrefab;  // 未完成目标预制体
        [SerializeField] TextMeshProUGUI rewardText;  // 奖励文本

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();  // 获取任务
            title.text = quest.GetTitle();  // 设置标题文本为任务的标题
            foreach (Transform item in objectiveContainer)  // 清空目标容器下的子物体
            {
                Destroy(item.gameObject);
            }
            foreach (var objective in quest.GetObjectives())  // 遍历任务的目标
            {
                GameObject prefab = objectiveIncompletePrefab;  // 默认使用未完成目标预制体
                if (status.IsObjectiveComplete(objective.reference))  // 判断目标是否已完成
                {
                    prefab = objectivePrefab;  // 如果目标已完成，则使用目标预制体
                }
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);  // 实例化目标预制体，并将其设置为目标容器的子物体
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();  // 获取目标预制体的文本组件
                objectiveText.text = objective.description;  // 设置目标文本为目标的描述
            }
            rewardText.text = GetRewardText(quest);  // 设置奖励文本
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())  // 遍历任务的奖励
            {
                if (rewardText != "")
                {
                    rewardText += ", ";
                }
                if (reward.number > 1)
                {
                    rewardText += reward.number + " ";
                }
                rewardText += reward.item.GetDisplayName();  // 获取奖励物品的显示名称
            }
            if (rewardText == "")
            {
                rewardText = "No reward";  // 如果没有奖励，设置为"No reward"
            }
            rewardText += ".";  // 添加句号
            return rewardText;
        }
    }
}