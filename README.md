# sts-1-to-2-card
图片格式为 cards文件名英文小写单词_文件名英文小写单词 
 如GreenConcentrate为green_concentrate

本地化描述则为cards文件名大写
 如GreenConcentrate为GREEN_CONCENTRATE

添加卡牌需要注册，power不需要
 如ModHelper.AddModelToPool<角色CardPool, 卡牌路径>();
 四个角色和无色卡分别为ironclad,silent,regrent,necrobinder,colorless
        ModHelper.AddModelToPool<SilentCardPool, src.green.cards.GreenConcentrate>();

卡如果施加power数值，本地化写法为{Power文件名:diff()}
 如 每当你抽到一张状态牌时，抽{RedEvolvePower:diff()}张牌。

power里数值的本地化写法为[blue]{Amount}[/blue]
 如 每当你抽到一张[gold]状态牌[/gold]时，抽[blue]{Amount}[/blue]张牌。
