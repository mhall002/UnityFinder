using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Entity
    {
        public string Name;
        public string Description;
        public string HiddenDescription;
        public string ImageIdentifier;
        public int Height;
        public int Width;
        public EntityType Type;
        public int XPWorth;
    }

    public enum EntityType
    {
        PlayerCharacter,
        NonPlayerCharacter
    }
}
