using System;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.Inventories
{
    // Ǯ���࣬ʵ����ISaveable��IItemStore�ӿ�
    public class Purse : MonoBehaviour, ISaveable, IItemStore
    {
        [SerializeField] float startingBalance = 400f;  // ��ʼ���

        float balance = 0;  // ��ǰ���

        public event Action onChange;

        private void Awake()
        {
            balance = startingBalance;  // ���õ�ǰ���Ϊ��ʼ���
        }

        public float GetBalance()
        {
            return balance;  // ���ص�ǰ���
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;  // ���µ�ǰ���
            if (onChange != null)
            {
                onChange();  // �������仯�¼�
            }
        }

        public object CaptureState()
        {
            return balance;  // ���ص�ǰ�����Ϊ״̬
        }

        public void RestoreState(object state)
        {
            balance = (float)state;  // �ָ���ǰ���Ϊ״ֵ̬
        }

        public int AddItems(InventoryItem item, int number)
        {
            if (item is CurrencyItem)  // �����Ʒ�ǻ�����Ʒ
            {
                UpdateBalance(item.GetPrice() * number);  // ������Ʒ�۸���������µ�ǰ���
                return number;  // ��������
            }
            return 0;  // ����0��ʾ�����Ʒʧ��
        }
    }
}