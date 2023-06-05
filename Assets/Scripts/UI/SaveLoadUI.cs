using RPG.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;  // 内容根节点
        [SerializeField] GameObject buttonPrefab;  // 按钮预制体

        private void OnEnable()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();  // 获取SavingWrapper组件
            if (savingWrapper == null) return;
            foreach (Transform child in contentRoot)  // 清空内容根节点下的子物体
            {
                Destroy(child.gameObject);
            }
            foreach (string save in savingWrapper.ListSaves())  // 遍历存档列表
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);  // 实例化按钮预制体并将其设置为内容根节点的子物体
                TMP_Text textComp = buttonInstance.GetComponentInChildren<TMP_Text>();  // 获取按钮预制体的文本组件
                textComp.text = save;  // 设置按钮文本
                Button button = buttonInstance.GetComponentInChildren<Button>();  // 获取按钮预制体的按钮组件
                button.onClick.AddListener(() =>
                {
                    savingWrapper.LoadGame(save);  // 注册按钮点击事件，加载对应的存档
                });
            }
        }
    }
}