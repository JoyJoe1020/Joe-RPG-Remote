using RPG.Inventories;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;  // ����ı�

        Purse playerPurse = null;

        private void Start()
        {
            playerPurse = GameObject.FindGameObjectWithTag("Player").GetComponent<Purse>();  // ��ȡ��ҵ�Ǯ�����

            if (playerPurse != null)
            {
                playerPurse.onChange += RefreshUI;  // ע��Ǯ���仯�¼�
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            balanceField.text = $"${playerPurse.GetBalance():N2}";  // ˢ������ı�
        }

    }
}
