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
using sts1to2card.src.BlueDefect.character;
using sts1to2card.src.GreenSilent.character;
using sts1to2card.src.GreenSilentAwakened.character;
using sts1to2card.src.RedIronclad.character;
using sts1to2card.src.RedIroncladAwakened.character;

namespace sts1to2card.Scripts;

// 卡牌排除
public static class CardExclusionConfig
{
    // 觉醒牌池排除的红卡
    public static HashSet<string> RedExcludedFromAwakened = new HashSet<string>
    {
        "RedBludgeon",      // 1代重锤
        "RedHemokinesis",   // 1代御血
        "RedBloodletting",  // 1代放血
    };

    // 觉醒牌池排除的绿卡
    public static HashSet<string> GreenExcludedFromAwakened = new HashSet<string>
    {
        "GreenCalculatedGamble",   // 1代计算下注
        "GreenBladeDance",         // 1代小刀
        "GreenPhantasmalKiller",   // 1代幻影杀手
    };
}

[ModInitializer("Init")]
public class Entry
{
    public static void Init()
    {
        RegisterCards();
        RegisterRelics();
        ApplyHarmonyPatches();
    }

    private static void ApplyHarmonyPatches()
    {
        try
        {
            var harmony = new Harmony("sts1to2card.patches");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch (Exception)
        {
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
                ModHelper.AddModelToPool(typeof(GreenSilentCardPool), item);

                if (!CardExclusionConfig.GreenExcludedFromAwakened.Contains(name))
                {
                    ModHelper.AddModelToPool(typeof(GreenSilentAwakenedCardPool), item);
                }
            }
            else if (name.StartsWith("Blue"))
            {
                ModHelper.AddModelToPool(typeof(BlueDefectCardPool), item);
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