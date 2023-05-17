using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义RPG.Control命名空间，用于组织控制相关的代码
namespace RPG.Control
{
    // 定义一个名为PatrolPath的类，继承自MonoBehaviour，用于处理巡逻路线的逻辑
    public class PatrolPath : MonoBehaviour
    {
        // 定义一个常量用于表示在Unity编辑器中绘制路点的半径
        const float waypointGizmoRadius = 0.3f;

        // 当Unity编辑器绘制gizmos时调用此方法（用于在Unity编辑器中可视化巡逻路径）
        private void OnDrawGizmos()
        {
            // 遍历当前对象的所有子对象（每个子对象代表一个路点）
            for (int i = 0; i < transform.childCount; i++)
            {
                // 获取下一个路点的索引
                int j = GetNextIndex(i);
                // 在Unity编辑器中绘制一个球体用于表示当前路点
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                // 在Unity编辑器中绘制一条线用于表示从当前路点到下一个路点的路径
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        // 根据当前路点的索引获取下一个路点的索引
        public int GetNextIndex(int i)
        {
            // 如果当前路点是最后一个路点，则返回0（表示下一个路点是第一个路点，实现巡逻路线的循环）
            if (i + 1 == transform.childCount)
            {
                return 0;
            }

            // 否则，返回当前路点索引+1（表示下一个路点是当前路点的下一个）
            return i + 1;
        }

        // 根据路点的索引获取路点的位置
        public Vector3 GetWaypoint(int i)
        {
            // 获取子对象（路点）的位置
            return transform.GetChild(i).position;
        }
    }
}
