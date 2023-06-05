using System;
using GameDevTV.Inventories;
using RPG.Shops;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None;  // ��Ʒ���

        Button button;
        Shop currentShop;

        private void Awake()
        {
            button = GetComponent<Button>();  // ��ȡ��ť���
            button.onClick.AddListener(SelectFilter);  // ע�ᰴť����¼�
        }

        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;  // ���õ�ǰ�̵�
        }

        public void RefreshUI()
        {
            button.interactable = currentShop.GetFilter() != category;  // ���ݵ�ǰ�̵�Ĺ��������ð�ť�Ŀɽ�����
        }

        private void SelectFilter()
        {
            currentShop.SelectFilter(category);  // ѡ�������
        }
    }
}