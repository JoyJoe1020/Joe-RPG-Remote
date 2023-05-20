using UnityEngine;

// 定义一个名为RPG.Core的命名空间，以便组织相关的代码
namespace RPG.Core
{
    // 定义一个名为ActionScheduler的类，继承自MonoBehaviour，用于调度游戏对象的行为
    public class ActionScheduler : MonoBehaviour
    {
        // 定义一个IAction类型的变量currentAction，用于存储当前正在执行的行为
        IAction currentAction;

        // 定义一个公共方法StartAction，用于开始执行一个新的行为
        public void StartAction(IAction action)
        {
            // 如果当前正在执行的行为与传入的action相同，则不进行任何操作
            if (currentAction == action) return;

            // 如果当前正在执行的行为不为空
            if (currentAction != null)
            {
                // 调用currentAction的Cancel方法，取消当前正在执行的行为
                currentAction.Cancel();
            }

            // 将传入的action赋值给currentAction，表示开始执行新的行为
            currentAction = action;
        }

        // 定义一个公共方法CancelCurrentAction，用于取消当前正在执行的行为
        public void CancelCurrentAction()
        {
            // 调用StartAction方法，传入null作为参数，表示没有新的行为需要开始，从而取消当前行为
            StartAction(null);
        }
    }
}
