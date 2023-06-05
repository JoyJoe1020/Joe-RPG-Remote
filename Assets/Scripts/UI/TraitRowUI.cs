using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] Trait trait;  // 特征
        [SerializeField] TextMeshProUGUI valueText;  // 值文本
        [SerializeField] Button minusButton;  // 减少按钮
        [SerializeField] Button plusButton;  // 增加按钮

        TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();  // 获取玩家的特征存储组件
            minusButton.onClick.AddListener(() => Allocate(-1));  // 注册减少按钮点击事件，分配负值
            plusButton.onClick.AddListener(() => Allocate(+1));  // 注册增加按钮点击事件，分配正值
        }

        private void Update()
        {
            minusButton.interactable = playerTraitStore.CanAssignPoints(trait, -1);  // 根据特征存储组件判断是否可以分配负值，设置减少按钮的可交互性
            plusButton.interactable = playerTraitStore.CanAssignPoints(trait, +1);  // 根据特征存储组件判断是否可以分配正值，设置增加按钮的可交互性

            valueText.text = playerTraitStore.GetProposedPoints(trait).ToString();  // 根据特征存储组件获取特定特征的预计分配点数，更新值文本
        }

        public void Allocate(int points)
        {
            playerTraitStore.AssignPoints(trait, points);  // 根据特征存储组件分配特定特征的点数
        }

    }
}