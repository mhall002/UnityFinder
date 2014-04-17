using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Room
    {
        public static int Width = 26;
        public static int Height = 12;

        public int RoomID;
        public int XPWorth;

        bool Visible;

        public static Ground NormalTerrain;
        public static Ground BlockedTerrain;

        public Ground[,] TerrainGrid;
        public Entity[,] EntityGrid;

        public Room ()
        {
            TerrainGrid = new Ground[Height, Width];
        }

        public int getTotalXPWorth()
        {
            int total = XPWorth;
            foreach (var entity in EntityGrid)
            {
                if (entity.Type == EntityType.NonPlayerCharacter)
                    total += entity.XPWorth;
            }
            return total;
        }
    }

}
