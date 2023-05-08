using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����һ����ΪRPG.Core�������ռ䣬�Ա���֯��صĴ���
namespace RPG.Core
{
    // ����һ����ΪFollowCamera���࣬�̳���MonoBehaviour�����ڴ������������Ŀ��Ĺ���
    public class FollowCamera : MonoBehaviour
    {
        // ����һ��Transform���͵ı���target�����ڴ洢�����Ŀ�����
        [SerializeField] Transform target;

        void Update()
        {
            // ���������λ������ΪĿ������λ�ã�ʵ�����������Ŀ��Ĺ���
            transform.position = target.position;
        }
    }
}
