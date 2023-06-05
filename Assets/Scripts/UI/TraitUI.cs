using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointsText;  // 未分配点数文本
        [SerializeField] Button commitButton;  // 提交按钮

        TraitStore playerTraitStore = null;

        private void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();  // 获取玩家的特征存储组件
            commitButton.onClick.AddListener(playerTraitStore.Commit);  // 注册提交按钮点击事件，提交特征分配
        }

        private void Update()
        {
            unassignedPointsText.text = playerTraitStore.GetUnassignedPoints().ToString();  // 根据特征存储组件获取未分配的点数，更新未分配点数文本
        }
    }
}