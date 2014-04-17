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
            if (NormalTerrain == null)
            {
                NormalTerrain = new Ground()
                {
                    TypeID = 1,
                    TextureFile = "Tiles/NormalTile",
                    States = new List<State>()
                };
            }
            if (BlockedTerrain == null)
            {
                BlockedTerrain = new Ground()
                {
                    TypeID = 1,
                    TextureFile = "Tiles/BlackTile",
                    States = new List<State>()
                };
                BlockedTerrain.States.Add(new State()
                    {
                        Name = "Inaccessible",
                        Description = "You cannot move here"
                    });
            }

            TerrainGrid = new Ground[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if ((y >= Height-2 || y <= 1) && Math.Abs(x - Width / 2) > 1)
                    {
                        TerrainGrid[y, x] = BlockedTerrain;
                    }
                    else if ((x >= Width-2 || x <= 1) && Math.Abs(y - Height/2) > 1)
                    {
                        TerrainGrid[y, x] = BlockedTerrain;
                    }
                    else
                    {
                        TerrainGrid[y, x] = NormalTerrain;
                    }
                }
            }

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
