using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        PlayerController playerController;  // ��ҿ�����

        private void Start()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();  // ��ȡ��ҿ��������
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Time.timeScale = 0;  // ʱ������Ϊ0����ͣ��Ϸ
            playerController.enabled = false;  // ������ҿ�����
        }

        private void OnDisable()
        {
            if (playerController == null) return;
            Time.timeScale = 1;  // ʱ������Ϊ1���ָ���Ϸ
            playerController.enabled = true;  // ������ҿ�����
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // ��ȡ�����е�SavingWrapper����
            savingWrapper.Save();  // ������Ϸ
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // ��ȡ�����е�SavingWrapper����
            savingWrapper.Save();  // ������Ϸ
            savingWrapper.LoadMenu();  // �������˵�
        }
    }
}