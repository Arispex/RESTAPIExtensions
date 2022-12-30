using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Rests;
using Terraria.GameContent.Skies;
using Newtonsoft.Json;

namespace RESTAPIExtensions
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        public override string Author => "千亦";

        public override string Description => "REST API Extensions";

        public override string Name => "REST API Extensions";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public Plugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            TShock.RestApi.Register(new SecureRestCommand("/player/inventory", Inventory, "rest.player.inventory"));//列出玩家物品
            TShock.RestApi.Register(new SecureRestCommand("/world/progress", Progress, "rest.world.progress"));//世界进度
        }

        private object Inventory(RestRequestArgs args)//查看玩家背包
        {
            string player = args.Request.Parameters["player"];//args.Parameters["player"];
            if (player == null)
            {
                return new RestObject("400")
                {
                    {
                        "error",
                        "缺少必要参数player"
                    }
                };
            }
            var inventory = Player.Inventory(player);
            return new RestObject()
            {
                {
                    "response",
                    inventory
                }
            };
        }
        private object Progress(RestRequestArgs args)//获取进度详情
        {
            Dictionary<string, bool> progress = new Dictionary<string, bool>()
            {
                {"King Slime", NPC.downedSlimeKing}, //史莱姆王
                {"Eye of Cthulhu", NPC.downedBoss1}, //克苏鲁之眼
                {"Eater of Worlds / Brain of Cthulhu", NPC.downedBoss2}, //世界吞噬者 或 克苏鲁之脑
                {"Queen Bee", NPC.downedQueenBee}, //蜂后
                {"Skeletron", NPC.downedBoss3}, //骷髅王
                {"Deerclops", NPC.downedDeerclops}, //巨鹿
                {"Wall of Flesh", Main.hardMode}, //肉山
                {"Queen Slime", NPC.downedQueenSlime}, //史莱姆皇后
                {"The Twins", NPC.downedMechBoss2}, //双子魔眼
                {"The Destroyer", NPC.downedMechBoss1}, //毁灭者
                {"Skeletron Prime", NPC.downedMechBoss3}, //机械骷髅王
                {"Plantera", NPC.downedPlantBoss}, //世纪之花
                {"Golem", NPC.downedGolemBoss}, //石巨人
                {"Duke Fishron", NPC.downedFishron}, // 朱鲨
                {"Empress of Light", NPC.downedEmpressOfLight}, //光女
                {"Lunatic Cultist", NPC.downedAncientCultist}, //教徒
                {"Moon Lord", NPC.downedMoonlord}, //月总
                {"Solar Pillar", NPC.downedTowerSolar}, //太阳能柱
                {"Nebula Pillar", NPC.downedTowerNebula}, //星云柱
                {"Vortex Pillar", NPC.downedTowerVortex}, //涡柱
                {"Stardust Pillar", NPC.downedTowerStardust}, //星尘柱
            };
            return new RestObject()
            {
                {
                    "response",
                     progress
                }
            };
        }
    }
}
