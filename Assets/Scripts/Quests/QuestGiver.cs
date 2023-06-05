using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        // 将任务序列化，使其在Unity编辑器中可视
        [SerializeField] Quest quest;

        // 给予任务的方法
        public void GiveQuest()
        {
            // 从标记为"Player"的游戏对象中获取任务列表组件
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            // 调用任务列表的AddQuest方法以添加任务
            questList.AddQuest(quest);
        }

    }

}