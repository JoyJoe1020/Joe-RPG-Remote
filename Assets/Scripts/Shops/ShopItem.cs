using System;
using GameDevTV.Inventories;
using UnityEngine;

// RPG.Shops�����ռ�
namespace RPG.Shops
{
    // �̵���Ʒ��
    public class ShopItem
    {
        // ��Ʒ
        InventoryItem item;
        // ��������
        int availability;
        // �۸�
        float price;
        // ��������
        int quantityInTransaction;

        // ���캯��
        public ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            // ��ʼ����Ա����
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        // ��ȡ��Ʒͼ��
        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        // ��ȡ��������
        public int GetAvailability()
        {
            return availability;
        }

        // ��ȡ��Ʒ��
        public string GetName()
        {
            return item.GetDisplayName();
        }

        // ��ȡ�۸�
        public float GetPrice()
        {
            return price;
        }

        // ��ȡ�����Ʒ
        public InventoryItem GetInventoryItem()
        {
            return item;
        }

        // ��ȡ��������
        public int GetQuantityInTransaction()
        {
            return quantityInTransaction;
        }
    }
}
