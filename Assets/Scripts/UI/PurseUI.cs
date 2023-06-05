using RPG.Inventories;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;  // 余额文本

        Purse playerPurse = null;

        private void Start()
        {
            playerPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();  // 获取玩家的钱包组件

            if (playerPurse != null)
            {
                playerPurse.onChange += RefreshUI;  // 注册钱包变化事件
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            balanceField.text = $"${playerPurse.GetBalance():N2}";  // 刷新余额文本
        }

    }
}
