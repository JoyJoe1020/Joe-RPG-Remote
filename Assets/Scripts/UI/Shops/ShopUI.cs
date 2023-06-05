using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shopName;  // �̵�����
        [SerializeField] Transform listRoot;  // �б���ڵ�
        [SerializeField] RowUI rowPrefab;  // ��Ԥ����
        [SerializeField] TextMeshProUGUI totalField;  // �ܼ��ı�
        [SerializeField] Button confirmButton;  // ȷ�ϰ�ť
        [SerializeField] Button switchButton;  // �л���ť

        Shopper shopper = null;  // ������
        Shop currentShop = null;  // ��ǰ�̵�

        Color originalTotalTextColor;

        // Start is called before the first frame update
        void Start()
        {
            originalTotalTextColor = totalField.color;  // ��¼ԭʼ���ܼ��ı���ɫ

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();  // ��ȡ���������
            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;  // ע�Ṻ���ߵĻ�̵�ı��¼�
            confirmButton.onClick.AddListener(ConfirmTransaction);  // ע��ȷ�ϰ�ť�ĵ���¼�
            switchButton.onClick.AddListener(SwitchMode);  // ע���л���ť�ĵ���¼�

            ShopChanged();  // �̵�ı�ʱ����UIˢ��
        }

        private void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;  // ȡ��֮ǰ�̵��ˢ���¼�
            }
            currentShop = shopper.GetActiveShop();  // ��ȡ�����ߵĻ�̵�
            gameObject.SetActive(currentShop != null);  // ����UI����Ļ�Ծ״̬Ϊ�Ƿ��л�̵�

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);  // ���ù��˰�ť���̵�
            }

            if (currentShop == null) return;
            shopName.text = currentShop.GetShopName();  // �����̵�����Ϊ��ǰ�̵������

            currentShop.onChange += RefreshUI;  // ע���̵�ˢ���¼�

            RefreshUI();  // ˢ���̵�UI
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);  // ����б���ڵ��µ�������
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())  // ������ǰ�̵�Ĺ��˺����Ʒ
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);  // ʵ������UI������������Ϊ�б���ڵ��������
                row.Setup(currentShop, item);  // ������UI����Ϣ
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";  // �����ܼ��ı�Ϊ��ǰ���׵��ܼ�
            totalField.color = currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;  // �����̵���ʽ��Ƿ��㹻�����ܼ��ı�����ɫ
            confirmButton.interactable = currentShop.CanTransact();  // �����̵��Ƿ���Խ�������ȷ�ϰ�ť�Ŀɽ�����
            TextMeshProUGUI switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();  // ��ȡ�л���ť�������е��ı����
            TextMeshProUGUI confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();  // ��ȡȷ�ϰ�ť�������е��ı����
            if (currentShop.IsBuyingMode())
            {
                switchText.text = "Switch To Selling";  // �����ǰΪ����ģʽ�����л���ť���ı�����Ϊ"Switch To Selling"
                confirmText.text = "Buy";  // ��ȷ�ϰ�ť���ı�����Ϊ"Buy"
            }
            else
            {
                switchText.text = "Switch To Buying";  // �����ǰΪ����ģʽ�����л���ť���ı�����Ϊ"Switch To Buying"
                confirmText.text = "Sell";  // ��ȷ�ϰ�ť���ı�����Ϊ"Sell"
            }

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();  // ˢ�¹��˰�ť��UI
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);  // �ر��̵�
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();  // ȷ�Ͻ���
        }

        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());  // �л��̵�ģʽ
        }
    }
}