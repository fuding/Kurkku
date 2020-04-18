﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public enum ItemBehaviour
    {
        SOLID,
        SOLID_SINGLE_TILE,
        CAN_STACK_ON_TOP,
        CAN_NOT_STACK_ON_TOP,
        CAN_SIT_ON_TOP,
        CAN_STAND_ON_TOP,
        CAN_LAY_ON_TOP,
        CUSTOM_DATA_NUMERIC_ON_OFF,
        REQUIRES_TOUCHING_FOR_INTERACTION,
        CUSTOM_DATA_TRUE_FALSE,
        PUBLIC_SPACE_OBJECT,
        EXTRA_PARAMETER,
        DICE,
        CUSTOM_DATA_ON_OFF,
        CUSTOM_DATA_NUMERIC_STATE,
        TELEPORTER,
        DOOR_TELEPORTER,
        REQUIRES_RIGHTS_FOR_INTERACTION,
        GATE,
        ONE_WAY_GATE,
        PRIZE_TROPHY,
        ROLLER,
        REDEEMABLE,
        SOUND_MACHINE,
        SOUND_MACHINE_SAMPLE_SET,
        JUKEBOX,
        WALL_ITEM,
        POST_IT,
        DECORATION,
        WHEEL_OF_FORTUNE,
        ROOMDIMMER,
        PRESENT,
        PHOTO,
        PLACE_ROLLER_ON_TOP,
        INVISIBLE,
        EFFECT,
        SONG_DISK,
        PRIVATE_FURNITURE,
        NO_HEAD_TURN,
        ECO_BOX,
        PET_WATER_BOWL,
        PET_FOOD,
        PET_CAT_FOOD,
        PET_DOG_FOOD,
        PET_CROC_FOOD
    }
}
