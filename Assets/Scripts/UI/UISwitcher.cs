using UnityEngine;

namespace RPG.UI
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] GameObject entryPoint;  // 入口点

        private void Start()
        {
            SwitchTo(entryPoint);  // 切换到入口点
        }

        public void SwitchTo(GameObject toDisplay)
        {
            if (toDisplay.transform.parent != transform) return;  // 如果要显示的对象不是该对象的子对象，返回

            foreach (Transform child in transform)  // 遍历该对象的子对象
            {
                child.gameObject.SetActive(child.gameObject == toDisplay);  // 设置子对象的活跃状态，仅保持要显示的对象活跃
            }
        }
    }
}