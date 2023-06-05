using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestStatus
    {
        // 任务实例
        Quest quest;
        // 已完成任务目标列表
        List<string> completedObjectives = new List<string>();

        // 用于保存和加载的内部类
        [System.Serializable]
        class QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;
        }

        // 从任务创建新的 QuestStatus
        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        // 从保存的状态恢复 QuestStatus
        public QuestStatus(object objectState)
        {
            QuestStatusRecord state = objectState as QuestStatusRecord;
            quest = Quest.GetByName(state.questName);
            completedObjectives = state.completedObjectives;
        }

        // 获取任务
        public Quest GetQuest()
        {
            return quest;
        }

        // 检查任务是否全部完成
        public bool IsComplete()
        {
            foreach (var objective in quest.GetObjectives())
            {
                if (!completedObjectives.Contains(objective.reference))
                {
                    return false;
                }
            }
            return true;
        }

        // 获取已完成的任务目标数量
        public int GetCompletedCount()
        {
            return completedObjectives.Count;
        }

        // 检查任务目标是否已完成
        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        // 完成任务目标
        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
        }

        // 捕获当前任务状态
        public object CaptureState()
        {
            QuestStatusRecord state = new QuestStatusRecord();
            state.questName = quest.name;
            state.completedObjectives = completedObjectives;
            return state;
        }
    }
}