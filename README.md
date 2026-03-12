复刻1代的部分卡牌目前完成
 战士
 -进化
 -硬撑
 -无谋冲锋
 -活动肌肉
 -观察弱点
 -死亡收割
 -突破极限

 猎人
 -全神贯注

待处理
 -飞身踢
 -尸爆术


说明：
图片格式为 cards文件名英文小写单词_文件名英文小写单词 
 如GreenConcentrate为green_concentrate

本地化描述则为cards文件名大写
 如GreenConcentrate为GREEN_CONCENTRATE

添加卡牌需要注册，power不需要
 如ModHelper.AddModelToPool<角色CardPool, 卡牌路径>();

四个角色和无色卡分别为ironclad,silent,regrent,necrobinder,colorless
 如ModHelper.AddModelToPool<SilentCardPool, src.green.cards.GreenConcentrate>();

卡如果施加power数值，本地化写法为{Power文件名:diff()}
 如 每当你抽到一张状态牌时，抽{RedEvolvePower:diff()}张牌。

power里数值的本地化写法为[blue]{Amount}[/blue]
 如 每当你抽到一张[gold]状态牌[/gold]时，抽[blue]{Amount}[/blue]张牌。



引用

参考文献 
https://github.com/Alchyr/BaseLib-StS2 
https://github.com/GlitchedReme/SlayTheSpire2ModdingTutorials 
https://github.com/rayinls/STS2_Learner