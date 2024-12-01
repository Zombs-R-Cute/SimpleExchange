using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Items;
using Rocket.Unturned.Player;
using Steamworks;
using UnityEngine;

namespace Zombs_R_Cute_SimpleExchange
{
    public class ExchangeCommand : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var exchanges = ExchangePlugin.Instance.Configuration.Instance.Exchanges;

            if (command.Length == 0 || command.Length > 1)
            {
                UnturnedChat.Say(caller, "You have the following exchanges available:\n", Color.yellow);
                for (int i = 0; i < exchanges.Count; i++)
                {
                    string newItemString = "";
                    var newItem = UnturnedItems.GetItemAssetById(exchanges[i].NewItemId);
                    if (newItem != null)
                        newItemString = newItem.itemName;

                    string tradedItemString = "";
                    var tradedItem = UnturnedItems.GetItemAssetById(exchanges[i].TradedItemId);
                    if (tradedItem != null)
                        tradedItemString = tradedItem.itemName;

                    UnturnedChat.Say(caller,
                        $"{i + 1}) {newItemString}({exchanges[i].NewItemId}) for" +
                        $" {exchanges[i].Quantity}x {tradedItemString}({exchanges[i].TradedItemId})", Color.yellow);
                }

                return;
            }

            UnturnedPlayer player = UnturnedPlayer.FromCSteamID((CSteamID)ulong.Parse(caller.Id));

            var index = UInt16.Parse(command[0]);
            
            UnturnedChat.Say(caller, $"Exchange #{index}?", Color.yellow);
            if (index > exchanges.Count || index == 0)
            {
                UnturnedChat.Say(caller, "Sorry, that exchange is not offered.", Color.red);
                return;
            }

            var exchange = exchanges[index - 1];
            var inventorySearchResult = player.Inventory.search(exchange.TradedItemId, true, true);

            if (inventorySearchResult.Count >= exchange.Quantity)
            {
                for (int i = 0; i < exchange.Quantity; i++)
                {
                    var result = inventorySearchResult[i];
                    player.Inventory.removeItem(result.page,
                        player.Inventory.getIndex(result.page, result.jar.x, result.jar.y));
                }

                player.GiveItem(exchange.NewItemId, 1);
                UnturnedChat.Say(caller, "Pleasure doing business with you.", Color.yellow);
                return;
            }

            UnturnedChat.Say(caller,
                $"You have {inventorySearchResult.Count} of {exchange.Quantity} {UnturnedItems.GetItemAssetById(exchange.TradedItemId)?.itemName}(s)",
                Color.red);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "exchange";
        public string Help => "Shows available exchanges";
        public string Syntax => "/exchange [number]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string>() { "zombs.exchange" };
    }
}