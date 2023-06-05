using System;
using GameDevTV.Utils;
using RPG.SceneManagement;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyValue<SavingWrapper> savingWrapper;  // �ӳټ��ص�SavingWrapper����

        [SerializeField] TMP_InputField newGameNameField;  // ����Ϸ���������

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);  // ��ʼ���ӳټ��ص�SavingWrapper����
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();  // ��ȡ�����е�SavingWrapper����
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();  // ������Ϸ
        }

        public void NewGame()
        {
            savingWrapper.value.NewGame(newGameNameField.text);  // ��ʼ����Ϸ����������Ϸ������
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // ��Unity�༭���У�ֹͣ����ģʽ
#else
            Application.Quit();  // �ڷ�����Ӧ�ó����У��˳�Ӧ�ó���
#endif
        }
    }
}