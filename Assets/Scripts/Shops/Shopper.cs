using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    // ��������
    public class Shopper : MonoBehaviour
    {
        // ��ǰ��Ծ�̵�
        Shop activeShop = null;

        // ��Ծ�̵�����¼�
        public event Action activeShopChange;

        // ���õ�ǰ��Ծ�̵�
        public void SetActiveShop(Shop shop)
        {
            // ������ڻ�Ծ�̵꣬������������Ϊnull
            if (activeShop != null)
            {
                activeShop.SetShopper(null);
            }
            // �����µĻ�Ծ�̵�
            activeShop = shop;
            // �����Ծ�̵���ڣ�������������Ϊ��ǰʵ��
            if (activeShop != null)
            {
                activeShop.SetShopper(this);
            }
            // ������Ծ�̵�����¼�
            if (activeShopChange != null)
            {
                activeShopChange();
            }
        }

        // ��ȡ��ǰ��Ծ�̵�
        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}
