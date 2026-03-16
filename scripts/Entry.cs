using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using sts1to2card.src.GreenSilent;
using sts1to2card.src.GreenSilentAwakened;
using sts1to2card.src.RedIronclad;
using sts1to2card.src.RedIroncladAwakened;

namespace sts1to2card.Scripts;

// 卡牌排除配置类
public static class CardExclusionConfig
{
    // 需要从觉醒牌池排除的红色卡牌列表
    public static HashSet<string> RedExcludedFromAwakened = new HashSet<string>
    {
        "RedBludgeon",      // 排除刚才的重击卡
        // 示例: "RedClothesline", "RedThunderclap" 等
    };
    
    // 需要从觉醒牌池排除的绿色卡牌列表（如果需要）
    public static HashSet<string> GreenExcludedFromAwakened = new HashSet<string>
    {
		"GreenCalculatedGamble",
        // "GreenSomeCard",  // 可以添加要排除的绿色卡牌
    };
}

[ModInitializer("Init")]
public class Entry
{
    public static void Init()
    {
        RegisterCards();
        RegisterRelics();
    }

    private static void RegisterCards()
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> enumerable = from t in executingAssembly.GetTypes()
            where !t.IsAbstract && typeof(CardModel).IsAssignableFrom(t)
            select t;
        
        foreach (Type item in enumerable)
        {
            string name = item.Name;
            if (name.StartsWith("Colorless"))
            {
                ModHelper.AddModelToPool(typeof(ColorlessCardPool), item);
            }
            else if (name.StartsWith("Red"))
            {
                // 添加到基础牌池（总是添加）
                ModHelper.AddModelToPool(typeof(RedIroncladCardPool), item);
                
                // 检查是否在红色觉醒牌池排除列表中
                if (!CardExclusionConfig.RedExcludedFromAwakened.Contains(name))
                {
                    // 只有不在排除列表中的才添加到觉醒牌池
                    ModHelper.AddModelToPool(typeof(RedIroncladAwakenedCardPool), item);
                }
            }
            else if (name.StartsWith("Green"))
            {
                // 添加到基础牌池（总是添加）
                ModHelper.AddModelToPool(typeof(GreenSilentCardPool), item);
                
                // 检查是否在绿色觉醒牌池排除列表中
                if (!CardExclusionConfig.GreenExcludedFromAwakened.Contains(name))
                {
                    // 只有不在排除列表中的才添加到觉醒牌池
                    ModHelper.AddModelToPool(typeof(GreenSilentAwakenedCardPool), item);
                }
            }
        }
    }

    private static void RegisterRelics()
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> enumerable = from t in executingAssembly.GetTypes()
            where !t.IsAbstract && typeof(RelicModel).IsAssignableFrom(t)
            select t;
        
        foreach (Type item in enumerable)
        {
            string text = item.Namespace ?? "";
            if (text.Contains(".relics.shared"))
            {
                ModHelper.AddModelToPool(typeof(SharedRelicPool), item);
            }
            else if (text.Contains(".relics.red"))
            {
                ModHelper.AddModelToPool(typeof(IroncladRelicPool), item);
            }
            else if (text.Contains(".relics.green"))
            {
                ModHelper.AddModelToPool(typeof(SilentRelicPool), item);
            }
            else if (text.Contains(".relics.blue"))
            {
                ModHelper.AddModelToPool(typeof(DefectRelicPool), item);
            }
            else if (text.Contains(".relics.pink"))
            {
                ModHelper.AddModelToPool(typeof(NecrobinderRelicPool), item);
            }
            else if (text.Contains(".relics.orange"))
            {
                ModHelper.AddModelToPool(typeof(RegentRelicPool), item);
            }
        }
    }
}