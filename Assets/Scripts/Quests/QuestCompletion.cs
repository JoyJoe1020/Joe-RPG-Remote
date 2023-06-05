using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        // 将任务序列化，使其在Unity编辑器中可视
        [SerializeField] Quest quest;
        // 将任务目标序列化，使其在Unity编辑器中可视
        [SerializeField] string objective;

        // 完成任务目标的方法
        public void CompleteObjective()
        {
            // 从标记为"Player"的游戏对象中获取任务列表组件
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            // 调用任务列表的CompleteObjective方法以完成任务目标
            questList.CompleteObjective(quest, objective);
        }
    }
}