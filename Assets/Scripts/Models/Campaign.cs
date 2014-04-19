using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Pair<T, U>
    {
        public T First;
        public U Second;
        public Pair() { }
        public Pair(T first, U second)
        {
            First = first;
            Second = second;
        }
        public override bool Equals(object obj)
        {
            if (obj is Pair<T, U>)
            {
                Pair<T,U> pair = obj as Pair<T, U>;
                return (First.Equals(pair.First)) && (Second.Equals(pair.Second)) || (First.Equals(pair.Second)) && (Second.Equals(pair.First));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }
    }

    public class Campaign
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Character> Characters;
        private List<Pair<int, int>> RoomLinks;
        private Room[,] Rooms; // 17 * 9

        private Room activeRoom;
        public Room ActiveRoom
        {
            get
            {
                return activeRoom;
            }
            set
            {
                activeRoom = value;
                OnPropertyChanged("ActiveRoom");
            }
        }

        public Campaign ()
        {
            Rooms = new Room[17, 9];
            List<Character> Characters = new List<Character>();
            Dictionary<int, int> RoomLinks = new Dictionary<int, int>();
        }

        public void CharacterChanged() // HACK - no observable collection??
        {
            OnPropertyChanged("Characters");
        }

        public bool HasLink(int room1, int room2)
        {
            foreach (Pair<int, int> connection in RoomLinks)
            {
                int first = connection.First;
                int second = connection.Second;
                if ((first == room1 && second == room2) || (first == room2 && second == room1))
                    return true;
            }
            return false;
        }

        public void AddLink(int room1, int room2)
        {
            RoomLinks.Add(new Pair<int, int>(room1, room2));
            OnPropertyChanged("RoomLinks");
        }

        public void RemoveLink(int room1, int room2)
        {
            RoomLinks.Remove(new Pair<int, int>(room1, room2));
            OnPropertyChanged("RoomLinks");
        }

        public Room GetRoom(int x, int y)
        {
            return Rooms[x, y];
        }

        public Room[,] GetRooms()
        {
            return Rooms;
        }

        public void SetRoom(int x, int y, Room room)
        {
            Rooms[x, y] = room;
            OnPropertyChanged("Rooms[" + x + "," + y + "]");
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
