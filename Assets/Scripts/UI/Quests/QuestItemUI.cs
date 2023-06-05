using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;  // 标题文本
    [SerializeField] TextMeshProUGUI progress;  // 进度文本

    QuestStatus status;

    public void Setup(QuestStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();  // 设置标题文本为任务的标题
        progress.text = status.GetCompletedCount() + "/" + status.GetQuest().GetObjectiveCount();  // 设置进度文本为任务已完成的数量/任务的目标数量
    }

    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}