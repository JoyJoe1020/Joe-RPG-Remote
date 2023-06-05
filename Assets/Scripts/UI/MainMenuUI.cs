using System;
using GameDevTV.Utils;
using RPG.SceneManagement;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        LazyValue<SavingWrapper> savingWrapper;  // 延迟加载的SavingWrapper对象

        [SerializeField] TMP_InputField newGameNameField;  // 新游戏名称输入框

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);  // 初始化延迟加载的SavingWrapper对象
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();  // 获取场景中的SavingWrapper对象
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();  // 继续游戏
        }

        public void NewGame()
        {
            savingWrapper.value.NewGame(newGameNameField.text);  // 开始新游戏，传入新游戏的名称
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // 在Unity编辑器中，停止播放模式
#else
            Application.Quit();  // 在发布的应用程序中，退出应用程序
#endif
        }
    }
}