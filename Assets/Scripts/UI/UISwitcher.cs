using UnityEngine;

namespace RPG.UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] GameObject entryPoint;  // ��ڵ�

        private void Start()
        {
            SwitchTo(entryPoint);  // �л�����ڵ�
        }

        public void SwitchTo(GameObject toDisplay)
        {
            if (toDisplay.transform.parent != transform) return;  // ���Ҫ��ʾ�Ķ����Ǹö�����Ӷ��󣬷���

            foreach (Transform child in transform)  // �����ö�����Ӷ���
            {
                child.gameObject.SetActive(child.gameObject == toDisplay);  // �����Ӷ���Ļ�Ծ״̬��������Ҫ��ʾ�Ķ����Ծ
            }
        }
    }
}