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

        // ���һ��Vector3���͵ı���offset�����ڴ洢����������Ŀ���λ��ƫ��
        [SerializeField] Vector3 offset = new Vector3(0, 10, -10); 

        void Update()
        {
            // ���������λ������ΪĿ������λ�ã�ʵ�����������Ŀ��Ĺ���
            transform.position = target.position;

            // ����������ĽǶȣ�ʹ�丩��Ŀ��
            transform.LookAt(target);
        }
    }
}
