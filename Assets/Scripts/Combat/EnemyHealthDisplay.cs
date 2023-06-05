using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    // �з�����ֵ��ʾ�࣬������UI����ʾ�з�����ֵ
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter; // ��ҵ�ս�������

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>(); // �ڱ�ǩΪ"Player"����Ϸ�����в��Ҳ���ȡս�������
        }

        private void Update()
        {
            if (fighter.GetTarget() == null) // �����ҵ�Ŀ��Ϊ��
            {
                GetComponent<Text>().text = "N/A"; // ��ʾ"N/A"
                return;
            }
            Health health = fighter.GetTarget(); // ��ȡ���Ŀ�������ֵ���
            // ��ʽ���ı�����ʾ��ǰ����ֵ���������ֵ������ "100/200"
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
