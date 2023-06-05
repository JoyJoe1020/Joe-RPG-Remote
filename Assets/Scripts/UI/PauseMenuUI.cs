using RPG.Control;
using RPG.SceneManagement;
using UnityEngine;

namespace RPG.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        PlayerController playerController;  // 玩家控制器

        private void Start()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();  // 获取玩家控制器组件
        }

        private void OnEnable()
        {
            if (playerController == null) return;
            Time.timeScale = 0;  // 时间缩放为0，暂停游戏
            playerController.enabled = false;  // 禁用玩家控制器
        }

        private void OnDisable()
        {
            if (playerController == null) return;
            Time.timeScale = 1;  // 时间缩放为1，恢复游戏
            playerController.enabled = true;  // 启用玩家控制器
        }

        public void Save()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // 获取场景中的SavingWrapper对象
            savingWrapper.Save();  // 保存游戏
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // 获取场景中的SavingWrapper对象
            savingWrapper.Save();  // 保存游戏
            savingWrapper.LoadMenu();  // 返回主菜单
        }
    }
}