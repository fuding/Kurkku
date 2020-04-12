﻿using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Room
    {
        #region Properties

        public RoomData Data { get; }

        #endregion

        #region Constructors

        public Room(RoomData data)
        {
            Data = data;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Wrap the retrieved database data with a room instance >:)
        /// </summary>
        internal static Room Wrap(RoomData roomData)
        {
            return new Room(roomData);
        }

        #endregion
    }
}
