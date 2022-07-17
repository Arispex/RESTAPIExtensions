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
            List<Dictionary<string, bool>> progress = new List<Dictionary<string, bool>>(
                    new Dictionary<string, bool>[]
                    {
                        new Dictionary<string, bool>()
                        {
                            {"King Slime", NPC.downedSlimeKing} //史莱姆王
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Eye of Cthulhu", NPC.downedBoss1} //克苏鲁之眼
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Eater of Worlds / Brain of Cthulhu", NPC.downedBoss2} //世界吞噬者 或 克苏鲁之脑
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Queen Bee", NPC.downedQueenBee} //蜂后
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Skeletron", NPC.downedBoss3} //骷髅王
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Deerclops", NPC.downedDeerclops} //巨鹿
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Wall of Flesh", Main.hardMode} //肉山
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Queen Slime", NPC.downedQueenSlime} //史莱姆皇后
                        },
                        new Dictionary<string, bool>()
                        {
                            {"The Twins", NPC.downedMechBoss2} //双子魔眼
                        },
                        new Dictionary<string, bool>()
                        {
                            {"The Destroyer", NPC.downedMechBoss1} //毁灭者
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Skeletron Prime", NPC.downedMechBoss3} //机械骷髅王
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Plantera", NPC.downedPlantBoss} //世纪之花
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Golem", NPC.downedGolemBoss} //石巨人
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Duke Fishron", NPC.downedFishron} // 朱鲨
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Empress of Light", NPC.downedEmpressOfLight} //光女
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Lunatic Cultist", NPC.downedAncientCultist} //教徒
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Moon Lord", NPC.downedMoonlord} //月总
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Solar Pillar", NPC.downedTowerSolar} //太阳能柱
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Nebula Pillar", NPC.downedTowerNebula} //星云柱
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Vortex Pillar", NPC.downedTowerVortex} //涡柱
                        },
                        new Dictionary<string, bool>()
                        {
                            {"Stardust Pillar", NPC.downedTowerStardust} //星尘柱
                        },
                    }
                );
            return new RestObject()
            {
                {
                    "response",
                    JsonConvert.SerializeObject(progress)
                }
            };
        }
    }
}