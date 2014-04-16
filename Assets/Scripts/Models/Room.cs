using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Room
    {
        public int RoomID;
        public int XPWorth;

        bool Visible;

        public Terrain[][] TerrainGrid;
        public Entity[][] EntityGrid;

        public int getTotalXPWorth()
        {
            int total = XPWorth;
            foreach (var row in EntityGrid)
            {
                foreach (var entity in row)
                {
                    if (entity.Type == EntityType.NonPlayerCharacter)
                        total += entity.XPWorth;
                }
            }
            return total;
        }
    }
}
