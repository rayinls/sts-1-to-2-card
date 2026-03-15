using HarmonyLib;
using System.Linq;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using System.Reflection;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.Scripts
{
    [ModInitializer("Init")]
    public class Entry
    {
        public static void Init()
        {
            // 自动注册卡牌
            RegisterCards();

            // 自动注册遗物
            RegisterRelics();
        }

        private static void RegisterCards()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var cardTypes = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    typeof(CardModel).IsAssignableFrom(t)
                );

            foreach (var type in cardTypes)
            {
                var name = type.Name;

                                 // 无色卡
                if (name.StartsWith("Colorless"))
                {
                    ModHelper.AddModelToPool(typeof(ColorlessCardPool), type);
                }

                // 红卡
                else if (name.StartsWith("Red"))
                {
                    ModHelper.AddModelToPool(typeof(IroncladCardPool), type);
                }

                // 绿卡
                else if (name.StartsWith("Green"))
                {
                    ModHelper.AddModelToPool(typeof(SilentCardPool), type);
                }
            }
        }

        private static void RegisterRelics()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var relicTypes = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract &&
                    typeof(RelicModel).IsAssignableFrom(t)
                );

            foreach (var type in relicTypes)
            {
                var ns = type.Namespace ?? "";

                // relics.shared → 通用遗物
                if (ns.Contains(".relics.shared"))
                {
                    ModHelper.AddModelToPool(typeof(SharedRelicPool), type);
                }

                // relics.red → 战士遗物
                else if (ns.Contains(".relics.red"))
                {
                    ModHelper.AddModelToPool(typeof(IroncladRelicPool), type);
                }

                // relics.green → 猎人遗物
                else if (ns.Contains(".relics.green"))
                {
                    ModHelper.AddModelToPool(typeof(SilentRelicPool), type);
                }

                else if (ns.Contains(".relics.blue"))
                {
                    ModHelper.AddModelToPool(typeof(DefectRelicPool), type);
                }
                
                else if (ns.Contains(".relics.pink"))
                {
                    ModHelper.AddModelToPool(typeof(NecrobinderRelicPool), type);
                }

                else if (ns.Contains(".relics.orange"))
                {
                    ModHelper.AddModelToPool(typeof(RegentRelicPool), type);
                }

                // ===== 预留扩展接口 =====
                // 未来新增角色时只需要在这里补充
                //
                // else if (ns.Contains(".relics.defect"))
                // {
                //     ModHelper.AddModelToPool(typeof(DefectRelicPool), type);
                // }
                //
                // else if (ns.Contains(".relics.necrobinder"))
                // {
                //     ModHelper.AddModelToPool(typeof(NecrobinderRelicPool), type);
                // }
                //
                // ========================
            }
        }
    }
}