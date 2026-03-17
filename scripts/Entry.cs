using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;
using HarmonyLib;
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
        "RedBludgeon",      // 1代重锤
        "RedHemokinesis",   // 1代御血
        "BLOODLETTING",     // 1代放血
        // 示例: "RedClothesline", "RedThunderclap" 等
    };
    
    // 需要从觉醒牌池排除的绿色卡牌列表（如果需要）
    public static HashSet<string> GreenExcludedFromAwakened = new HashSet<string>
    {
		"GreenCalculatedGamble",   // 1代计算下注
        "GreenBladeDance",         // 1代小刀
        "GreenPhantasmalKiller",   // 1代幻影杀手
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
        // 应用 Harmony 补丁
        ApplyHarmonyPatches();
    }

    private static void ApplyHarmonyPatches()
    {
        try
        {
            var harmony = new Harmony("sts1to2card.patches");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            GD.Print("[MyMod] Harmony patches applied successfully.");

            // 可选：输出补丁数量
            var patchedMethods = harmony.GetPatchedMethods().ToList();
            GD.Print($"[MyMod] Total patched methods: {patchedMethods.Count}");
        }
        catch (Exception ex)
        {
            GD.PrintErr($"[MyMod] Failed to apply Harmony patches: {ex}");
        }
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