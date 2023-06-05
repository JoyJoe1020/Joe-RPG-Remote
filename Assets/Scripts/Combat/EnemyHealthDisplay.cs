using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    // �з�����ֵ��ʾ�࣬������UI����ʾ�з�����ֵ
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter; // ս�������

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>(); // �ڱ�ǩΪ"Player"����Ϸ�����в��Ҳ���ȡս�������
        }

        private void Update()
        {
            if (fighter.GetTarget() == null) // ���ս���ߵ�Ŀ��Ϊ��
            {
                GetComponent<Text>().text = "N/A"; // ��ʾ"N/A"
                return;
            }
            Health health = fighter.GetTarget(); // ��ȡĿ�������ֵ���
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints()); // ��ʽ���ı�����ʾ��ǰ����ֵ���������ֵ
        }
    }
}