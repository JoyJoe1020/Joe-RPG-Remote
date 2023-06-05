using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        // 存储 QuestStatus 对象的列表
        List<QuestStatus> statuses = new List<QuestStatus>();

        // 委托事件，用于在任务更新时触发
        public event Action onUpdate;

        // 在每个 Update() 周期检查是否有满足条件的任务目标需要完成
        private void Update()
        {
            CompleteObjectivesByPredicates();
        }

        // 添加任务到任务列表，如果任务已经在列表中则不添加
        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            onUpdate?.Invoke();
        }

        // 完成任务的特定目标，并检查任务是否全部完成，如果全部完成则给予奖励
        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);
            status.CompleteObjective(objective);
            if (status.IsComplete())
            {
                GiveReward(quest);
            }
            onUpdate?.Invoke();
        }

        // 检查任务列表是否包含某个任务
        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        // 返回任务状态列表
        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        // 根据任务获取任务状态
        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        // 给予任务奖励，如果奖励无法添加到背包则将其掉落
        private void GiveReward(Quest quest)
        {
            foreach (var reward in quest.GetRewards())
            {
                bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                if (!success)
                {
                    GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }

        // 检查任务目标的完成条件，如果满足则完成该目标
        private void CompleteObjectivesByPredicates()
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.IsComplete()) continue;
                Quest quest = status.GetQuest();
                foreach (var objective in quest.GetObjectives())
                {
                    if (status.IsObjectiveComplete(objective.reference)) continue;
                    if (!objective.usesCondition) continue;
                    if (objective.completionCondition.Check(GetComponents<IPredicateEvaluator>()))
                    {
                        CompleteObjective(quest, objective.reference);
                    }
                }
            }
        }

        // 保存当前任务状态
        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        // 恢复任务状态
        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            statuses.Clear();
            foreach (object objectState in stateList)
            {
                statuses.Add(new QuestStatus(objectState));
            }
        }

        // 根据谓词和参数判断任务状态
        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
            }

            return null;
        }
    }
}