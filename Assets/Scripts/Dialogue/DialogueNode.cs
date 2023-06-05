using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false; // 是否为玩家对白
        [SerializeField]
        string text; // 对白文本
        [SerializeField]
        List<string> children = new List<string>(); // 子节点的名称列表
        [SerializeField]
        Rect rect = new Rect(0, 0, 200, 100); // 对话节点的矩形区域
        [SerializeField]
        string onEnterAction; // 进入对话时触发的动作
        [SerializeField]
        string onExitAction; // 退出对话时触发的动作
        [SerializeField]
        Condition condition; // 对话条件

        public Rect GetRect()
        {
            return rect; // 返回对话节点的矩形区域
        }

        public string GetText()
        {
            return text; // 返回对白文本
        }

        public List<string> GetChildren()
        {
            return children; // 返回子节点的名称列表
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking; // 返回是否为玩家对白
        }

        public string GetOnEnterAction()
        {
            return onEnterAction; // 返回进入对话时触发的动作
        }

        public string GetOnExitAction()
        {
            return onExitAction; // 返回退出对话时触发的动作
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators); // 检查对话条件是否满足
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition; // 设置对话节点的位置
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText; // 更新对白文本
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID); // 添加子节点
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID); // 移除子节点
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking; // 更改玩家对白标识
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
