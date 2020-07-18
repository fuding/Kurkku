﻿using log4net;
using System;
using System.Collections.Generic;
using Kurkku.Util.Extensions;
using Kurkku.Messages.Outgoing;
using System.Linq;
using System.Collections.Concurrent;

namespace Kurkku.Game
{
    public class ItemTask : IRoomTask
    {
        private Room room;
        private ConcurrentQueue<Item> tickedItems;// = new ConcurrentQueue<Item>();

        /// <summary>
        /// Set task interval, which is 500ms
        /// </summary>
        public override int Interval => 500;

        /// <summary>
        /// Constructor for the item task
        /// </summary>
        public ItemTask(Room room)
        {
            this.room = room;
            this.tickedItems = new ConcurrentQueue<Item>(); 
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        public override void Run(object state)
        {
            foreach (Item item in room.ItemManager.Items.Values)
            {
                if (item.Interactor.RequiresTick())
                {
                    if (item.Interactor.CanTick())
                    {
                        item.Interactor.OnTick();
                        tickedItems.Enqueue(item);
                    }
                }
            }

            foreach (var item in tickedItems.Dequeue())
            {
                item.Interactor.OnTickComplete();
            }
        }
    }
}
