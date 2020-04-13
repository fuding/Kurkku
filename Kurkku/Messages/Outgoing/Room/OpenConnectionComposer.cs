﻿using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class OpenConnectionComposer : MessageComposer
    {
        private int roomId;
        private int categoryId;

        public OpenConnectionComposer(int roomId, int categoryId)
        {
            this.roomId = roomId;
            this.categoryId = categoryId;
        }

        public override short Header => 
            OutgoingEvents.OpenConnectionComposer;

        public override void Write()
        {
            m_Data.Add(roomId);
            m_Data.Add(categoryId);
        }
    }
}
