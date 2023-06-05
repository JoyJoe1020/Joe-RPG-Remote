using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);  // 获取加法修正器列表
        IEnumerable<float> GetPercentageModifiers(Stat stat);  // 获取百分比修正器列表
    }
}
