// ����RPG.Core�����ռ䣬��ʹ�����е�Health��
using RPG.Core;
using UnityEngine;

// ��RPG.combat�����ռ��¶���һ����ΪCombatTarget���࣬�̳���MonoBehaviour
// �������ڱ�ʾ��Ϸ�е�ս��Ŀ�꣬����ͨ�������ű�������й����Ȳ���
namespace RPG.Combat
{
    // ΪCombatTarget�����RequireComponent���ԣ�ȷ����ʹ�ø����GameObject��ͬʱ���Health���
    // �����GameObject�����CombatTarget���ʱû��Health�����Unity���Զ����һ��
    [RequireComponent(typeof(Health))]

    public class CombatTarget : MonoBehaviour
    {
        // �����Ŀ����Ϊ�˱�ʶ��Ϸ����Ϊս��Ŀ�꣬��˲���Ҫ��������ͷ���
        // �����ű�����ͨ�������Ϸ�������Ƿ����CombatTarget������ж��Ƿ�Ϊ�Ϸ�ս��Ŀ��
    }
}