namespace RPG.Control
{
    // 射线检测接口
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}
