using UnityEngine;

public class PlayerController : MonoBehaviour {

    private void Update() {
        //当鼠标左键被按住时
        if(Input.GetMouseButton(0))
        {
           // 调用MoveToCursor方法，使游戏对象移动到鼠标点击的位置
            MoveToCursor();
        }
    }

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
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}