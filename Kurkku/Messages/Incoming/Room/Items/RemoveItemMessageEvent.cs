﻿using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class RemoveItemMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            request.ReadInt();
            int itemId = request.ReadInt();

            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null && item.Data.OwnerId != player.Details.Id && !room.IsOwner(player.Details.Id)) // TODO: Staff check
                return;

            room.FurnitureManager.RemoveItem(item, player);

            var owner = PlayerManager.Instance.GetPlayerById(item.Data.OwnerId);

            if (owner != null)
            {
                owner.Inventory.AddItem(item);
                owner.Send(new FurniListAddComposer(item));
            }
        }
    }
}
