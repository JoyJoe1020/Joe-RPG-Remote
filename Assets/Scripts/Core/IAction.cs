// 在RPG.Core命名空间下定义一个名为IAction的接口
namespace RPG.Core
{
    // 定义IAction接口，用于规定实现该接口的类必须包含的方法
    public interface IAction
    {
        // 定义一个名为Cancel的方法，用于取消执行的操作
        void Cancel();
    }
}