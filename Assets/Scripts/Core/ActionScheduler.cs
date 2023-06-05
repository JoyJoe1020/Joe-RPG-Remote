using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; // ��ǰ�Ķ���

        public void StartAction(IAction action)
        {
            if (currentAction == action) return; // ���Ҫ��ʼ�Ķ����뵱ǰ������ͬ��ֱ�ӷ���
            if (currentAction != null)
            {
                currentAction.Cancel(); // ȡ����ǰ����
            }
            currentAction = action; // ���õ�ǰ����ΪҪ��ʼ�Ķ���
        }

        public void CancelCurrentAction()
        {
            StartAction(null); // ȡ����ǰ����
        }
    }
}
