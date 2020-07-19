﻿using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class DiceInteractor : Interactor
    {
        public static class DiceAttributes
        {
            public const string ROLL_DICE = "ROLL_DICE";
            public const string ENTITY = "ENTITY";
        }

        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override bool RequiresTick => false;

        #endregion

        #region Constructor

        public DiceInteractor(Item item) : base(item) { }

        #endregion

        /// <summary>
        /// Find closest sourrounding avaliable tile for player
        /// </summary>
        public override bool OnWalkRequest(IEntity entity, Position goal)
        {
            var closestTile = Item.Position.ClosestTile(Item.Room, entity.RoomEntity.Position, entity);

            if (closestTile != null)
            {
                entity.RoomEntity.Move(closestTile.X, closestTile.Y);
                return true;
            }

            return false;
        }

        /// <summary>
        /// On interact dice handler
        /// </summary>
        public override void OnInteract(IEntity entity)
        {
            if (!Item.Position.Touches(entity.RoomEntity.Position))
                return;

            int.TryParse(Item.Data.ExtraData, out int currentMode);

            if (currentMode > 0)
            {
                Item.UpdateStatus("0");
                Item.Save();
                return;
            }
            else
            {
                // Queue future task for rolling dice
                if (!EventQueue.ContainsKey(DiceAttributes.ROLL_DICE))
                {
                    Item.UpdateStatus("-1");
                    Item.Save();

                    // Queue dice result delay
                    QueueEvent(DiceAttributes.ROLL_DICE, 2.0, new Dictionary<object, object>()
                    {
                        [DiceAttributes.ENTITY] = entity
                    });
                }
            }
        }

        /// <summary>
        /// Process future state
        /// </summary>
        public override void ProcessQueuedEvent(QueuedEvent queuedEvent) 
        {
            if (!queuedEvent.HasAttribute(DiceAttributes.ENTITY))
                return;

            var entity = queuedEvent.GetAttribute<IEntity>(DiceAttributes.ENTITY);

            switch (queuedEvent.EventName)
            {
                case DiceAttributes.ROLL_DICE:
                    {
                        Item.UpdateStatus("3");
                        Item.Save();
                    }
                    break;
            }
        }
    }
}
