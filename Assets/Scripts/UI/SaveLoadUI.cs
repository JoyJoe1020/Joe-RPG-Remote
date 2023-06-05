using RPG.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;  // ���ݸ��ڵ�
        [SerializeField] GameObject buttonPrefab;  // ��ťԤ����

        private void OnEnable()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // ��ȡSavingWrapper���
            if (savingWrapper == null) return;
            foreach (Transform child in contentRoot)  // ������ݸ��ڵ��µ�������
            {
                Destroy(child.gameObject);
            }
            foreach (string save in savingWrapper.ListSaves())  // �����浵�б�
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);  // ʵ������ťԤ���岢��������Ϊ���ݸ��ڵ��������
                TMP_Text textComp = buttonInstance.GetComponentInChildren<TMP_Text>();  // ��ȡ��ťԤ������ı����
                textComp.text = save;  // ���ð�ť�ı�
                Button button = buttonInstance.GetComponentInChildren<Button>();  // ��ȡ��ťԤ����İ�ť���
                button.onClick.AddListener(() =>
                {
                    savingWrapper.LoadGame(save);  // ע�ᰴť����¼������ض�Ӧ�Ĵ浵
                });
            }
        }
    }
}