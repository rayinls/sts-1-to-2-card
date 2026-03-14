using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
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

                // 红卡
                if (name.StartsWith("Red"))
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
    }
}