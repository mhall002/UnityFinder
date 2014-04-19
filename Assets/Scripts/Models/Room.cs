using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static int Width = 26;
        public static int Height = 12;

        private int roomID;
        public int RoomID
        {
            get
            {
                return roomID;
            }
            set
            {
                roomID = value;
                OnPropertyChanged("RoomID");
            }
        }

        int xpWorth;
        public int XPWorth
        {
            get
            {
                return xpWorth;
            }
            set
            {
                xpWorth = value;
                OnPropertyChanged("XPWorth");
            }
        }

        private bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                OnPropertyChanged("Visible");
            }
        }

        private Ground[,] TerrainGrid;
        private Entity[,] EntityGrid;

        public Room ()
        {
            TerrainGrid = new Ground[Width, Height];
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

        public void SetTerrain(int x, int y, Ground terrain)
        {
            TerrainGrid[x, y] = terrain;
            OnPropertyChanged("Terrain[" + x + "," + y + "]");
        }

        public Ground GetTerrain(int x, int y)
        {
            return TerrainGrid[x, y];
        }

        public Ground[,] GetTerrain()
        {
            return TerrainGrid;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
