using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Quests
{
    // 创建一个新的Unity资源菜单项"Quest"
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG Project/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        // 创建一个任务目标的列表
        [SerializeField] List<Objective> objectives = new List<Objective>();
        // 创建一个任务奖励的列表
        [SerializeField] List<Reward> rewards = new List<Reward>();

        // 任务奖励类
        [System.Serializable]
        public class Reward
        {
            // 奖励的数量，最小值为1
            [Min(1)]
            public int number;
            // 奖励的物品
            public InventoryItem item;
        }

        // 任务目标类
        [System.Serializable]
        public class Objective
        {
            // 目标的引用
            public string reference;
            // 目标的描述
            public string description;
            // 目标是否使用条件
            public bool usesCondition = false;
            // 完成任务的条件
            public Condition completionCondition;
        }

        // 获取任务标题的方法
        public string GetTitle()
        {
            return name;
        }

        // 获取任务目标数量的方法
        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        // 获取任务目标的方法
        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        // 获取任务奖励的方法
        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        // 检查是否存在指定的任务目标
        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if (objective.reference == objectiveRef)
                {
                    return true;
                }
            }
            return false;
        }

        // 通过任务名获取任务对象的静态方法
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