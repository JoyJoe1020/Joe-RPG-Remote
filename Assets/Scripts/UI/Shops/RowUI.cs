using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField]
        Image iconField;  // ͼ��
        [SerializeField]
        TextMeshProUGUI nameField;  // ����
        [SerializeField]
        TextMeshProUGUI availabilityField;  // ������
        [SerializeField]
        TextMeshProUGUI priceField;  // �۸�
        [SerializeField]
        TextMeshProUGUI quantityField;  // ����

        Shop currentShop = null;  // ��ǰ�̵�
        ShopItem item = null;  // ��Ʒ

        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            iconField.sprite = item.GetIcon();  // ����ͼ��Ϊ��Ʒ��ͼ��
            nameField.text = item.GetName();  // ��������Ϊ��Ʒ������
            availabilityField.text = $"{item.GetAvailability()}";  // ���ÿ�����Ϊ��Ʒ�Ŀ�����
            priceField.text = $"${item.GetPrice():N2}";  // ���ü۸�Ϊ��Ʒ�ļ۸�
            quantityField.text = $"{item.GetQuantityInTransaction()}";  // ��������Ϊ��Ʒ�Ľ�������
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);  // ����Ʒ��ӵ�������
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);  // �ӽ������Ƴ���Ʒ
        }
    }
}