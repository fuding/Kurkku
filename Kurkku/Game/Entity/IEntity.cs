﻿using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    interface IEntity<T> where T : IEntityData
    {
        T Data { get; }
    }
}
