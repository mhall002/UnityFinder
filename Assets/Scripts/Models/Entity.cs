﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Entity
    {
        public string Name;
        public string Description;
        public string HiddenDescription;
        public int ImageIdentifier;
        public int Height;
        public int Width;
        public EntityType Type;
        public int XPWorth;
    }

    enum EntityType
    {
        PlayerCharacter,
        NonPlayerCharacter
    }
}