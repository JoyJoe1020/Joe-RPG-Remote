using UnityEngine;

// 定义一个名为PlayerController的类，继承自MonoBehaviour，用于处理玩家控制
public class PlayerController : MonoBehaviour
{

    // Update方法在每一帧中被调用
    private void Update()
    {
        // 当鼠标左键被按下时
        if (Input.GetMouseButton(0))
        {
            // 调用MoveToCursor方法，使游戏对象移动到鼠标点击的位置
            MoveToCursor();
        }
    }

    // 定义一个私有方法MoveToCursor，用于计算鼠标点击的位置并移动游戏对象
    private void MoveToCursor()
    {
        // 根据鼠标在屏幕上的位置计算一条射线，并将其存储在ray变量中
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 定义一个RaycastHit类型的变量hit，用于存储射线与物体的交点信息
        RaycastHit hit;
        // 使用Physics.Raycast方法检测射线是否与场景中的物体发生碰撞，并将结果存储在hasHit变量中
        bool hasHit = Physics.Raycast(ray, out hit);
        // 如果射线与物体发生碰撞（hasHit为true）
        if (hasHit)
        {
            // 获取当前游戏对象的Mover组件
            // 调用Mover组件的MoveTo方法，将目标位置设置为射线与物体的交点（hit.point）
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
