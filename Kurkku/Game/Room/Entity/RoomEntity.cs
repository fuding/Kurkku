﻿using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public abstract class RoomEntity {
        public IEntity Entity { get; set; }
        public Room Room { get; set; }

        public RoomEntity(IEntity entity)
        {
            Entity = entity;
        }
    }
}
