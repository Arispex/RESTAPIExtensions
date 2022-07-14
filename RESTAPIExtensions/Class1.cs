using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Rests;

namespace RESTAPIExtensions
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        public override string Author => "千亦";

        public override string Description => "REST Extensions";

        public override string Name => "REST Extensions";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public Plugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            TShock.RestApi.Register(new SecureRestCommand("/player/inventory", Inventory, "rest.player.invsee"));//列出玩家物品
        }

        private object Inventory(RestRequestArgs args)//查看玩家背包
        {
            string player = args.Request.Parameters["player"];//args.Parameters["player"];
            if (player == null)
            {
                return new RestObject("403")
                {
                    {
                        "response",
                        "缺少必要参数"
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
    }
}