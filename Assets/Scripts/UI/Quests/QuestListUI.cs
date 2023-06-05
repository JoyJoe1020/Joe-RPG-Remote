using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] QuestItemUI questPrefab;  // 任务项预制体
    QuestList questList;

    // Start is called before the first frame update
    void Start()
    {
        questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();  // 获取玩家的任务列表组件
        questList.onUpdate += Redraw;  // 注册任务列表更新事件
        Redraw();  // 绘制任务列表UI
    }

    private void Redraw()
    {
        foreach (Transform item in transform)  // 清空UI容器下的子物体
        {
            Destroy(item.gameObject);
        }
        foreach (QuestStatus status in questList.GetStatuses())  // 遍历任务列表中的任务状态
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);  // 实例化任务项预制体，并将其设置为UI容器的子物体
            uiInstance.Setup(status);  // 设置任务项的信息
        }
    }
}