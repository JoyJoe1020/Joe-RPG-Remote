// ʹ��System�����ռ䣬�������ռ䶨����һЩ��������ͻ�Ԫ����
using System;
// ʹ��RPG.Attributes�����ռ䣬�������ռ䶨����RPG��Ϸ��һЩ����
using RPG.Attributes;
// ʹ��UnityEngine�����ռ䣬�������ռ���Unity����ĺ���API
using UnityEngine;
// ʹ��UnityEngine.UI�����ռ䣬�������ռ���Unity�����UIϵͳ��API
using UnityEngine.UI;

// ����RPG.Combat�����ռ�
namespace RPG.Combat
{
    // ����һ��������EnemyHealthDisplay���̳���Unity��MonoBehaviour����
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;  // ����һ��Fighter�������ڴ洢��ҵ�ս����Ϣ

        // MonoBehaviour��Awake�����������е�Start����֮ǰ���ã����ű�ʵ��������ʱ����
        private void Awake()
        {
            // �ڳ����в��ұ�ǩΪ"Player"��GameObject��Ȼ���ȡ��Fighter���
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        // MonoBehaviour��Update��������ÿһ֡�ж��ᱻUnity����
        private void Update()
        {
            // �����ҵ�ս��Ŀ��Ϊ��
            if (fighter.GetTarget() == null)
            {
                // ��ȡ��ǰGameObject��Text��������������ı�Ϊ"N/A"
                GetComponent<Text>().text = "N/A";
                return;  // ����Update������ִ��
            }
            // ��ȡ��ҵ�ս��Ŀ���Health���
            Health health = fighter.GetTarget();
            // ��ȡ��ǰGameObject��Text��������������ı�ΪĿ��ĵ�ǰ����ֵ����󽡿�ֵ
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
