using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action; // 要触发的动作
        [SerializeField] UnityEvent onTrigger; // 触发时调用的Unity事件

        public void Trigger(string actionToTrigger)
        {
            if (actionToTrigger == action)
            {
                onTrigger.Invoke(); // 如果触发的动作与指定的动作匹配，则调用Unity事件
            }
        }
    }
}
