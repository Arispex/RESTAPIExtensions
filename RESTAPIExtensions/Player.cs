using TShockAPI;
using System;

namespace RESTAPIExtensions
{
    public class Player
    {
        public static NetItem[] Inventory(string player)
        {
            int id = -1;
            var account = TShock.UserAccounts.GetUserAccountByName(player);
            if (account == null) 
            {
                throw new Exception("找不到玩家");
            }
            id = account.ID;
            var data = TShock.CharacterDB.GetPlayerData(null, id);
            return data.inventory;
        }
    }
}