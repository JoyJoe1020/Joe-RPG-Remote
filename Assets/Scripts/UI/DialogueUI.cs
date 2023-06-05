using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;  // 对话AI的文本
        [SerializeField] Button nextButton;  // 下一个按钮
        [SerializeField] GameObject AIResponse;  // AI的回应
        [SerializeField] Transform choiceRoot;  // 选择根节点
        [SerializeField] GameObject choicePrefab;  // 选择预制体
        [SerializeField] Button quitButton;  // 退出按钮
        [SerializeField] TextMeshProUGUI conversantName;  // 对话者名称

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();  // 获取玩家对话者组件
            playerConversant.onConversationUpdated += UpdateUI;  // 注册对话更新事件
            nextButton.onClick.AddListener(() => playerConversant.Next());  // 注册下一个按钮点击事件
            quitButton.onClick.AddListener(() => playerConversant.Quit());  // 注册退出按钮点击事件

            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());  // 根据对话者的活跃状态设置UI的活跃状态
            if (!playerConversant.IsActive())
            {
                return;
            }
            conversantName.text = playerConversant.GetCurrentConversantName();  // 设置对话者名称文本
            AIResponse.SetActive(!playerConversant.IsChoosing());  // 设置AI回应的活跃状态
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());  // 设置选择根节点的活跃状态
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();  // 构建选择列表
            }
            else
            {
                AIText.text = playerConversant.GetText();  // 设置AI文本
                nextButton.gameObject.SetActive(playerConversant.HasNext());  // 设置下一个按钮的活跃状态
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)  // 清空选择根节点下的子物体
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())  // 遍历选择节点列表
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);  // 实例化选择预制体并将其设置为选择根节点的子物体
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();  // 获取选择预制体的文本组件
                textComp.text = choice.GetText();  // 设置选择文本
                Button button = choiceInstance.GetComponentInChildren<Button>();  // 获取选择预制体的按钮组件
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);  // 注册按钮点击事件，选择对应的选择节点
                });
            }
        }
    }
}