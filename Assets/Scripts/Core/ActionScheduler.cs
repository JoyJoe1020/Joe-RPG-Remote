using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; // 当前的动作

        public void StartAction(IAction action)
        {
            if (currentAction == action) return; // 如果要开始的动作与当前动作相同，直接返回
            if (currentAction != null)
            {
                currentAction.Cancel(); // 取消当前动作
            }
            currentAction = action; // 设置当前动作为要开始的动作
        }

        public void CancelCurrentAction()
        {
            StartAction(null); // 取消当前动作
        }
    }
}
