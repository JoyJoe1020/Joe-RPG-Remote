using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    // ����ֵ��ʾ�࣬������UI����ʾ����ֵ
    public class ManaDisplay : MonoBehaviour
    {
        Mana mana; // ����ֵ���

        private void Awake()
        {
            mana = GameObject.FindWithTag("Player").GetComponent<Mana>(); // �ڱ�ǩΪ"Player"����Ϸ�����в��Ҳ���ȡ����ֵ���
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", mana.GetMana(), mana.GetMaxMana()); // ��ʽ���ı�����ʾ��ǰ����ֵ�������ֵ
        }
    }
}