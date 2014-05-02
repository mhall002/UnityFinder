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
        public static int Width = 17;
        public static int Height = 9;

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

        public List<Entity> Characters = new List<Entity>();
        public List<Entity> Entities = new List<Entity>();
        private List<Pair<Pair<int, int>, Pair<int, int>>> RoomLinks = new List<Pair<Pair<int, int>, Pair<int, int>>>();
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

        public void CharacterChanged(string uid) // HACK - no observable collection??
        {
            OnPropertyChanged("Characters[" + uid + "]");
        }

        public void EntityChanged(string uid)
        {
            OnPropertyChanged("Entities[" + uid + "]");
        }

        public bool HasLink(Pair<int, int> room1, Pair<int, int> room2)
        {
            foreach (Pair<Pair<int, int>, Pair<int, int>> connection in RoomLinks)
            {
                Pair<int, int> first = connection.First;
                Pair<int, int> second = connection.Second;
                if ((first.Equals(room1) && second.Equals(room2)) || (first.Equals(room2) && second.Equals(room1)))
                    return true;
            }
            return false;
        }

        public void AddLink(Pair<int, int> room1, Pair<int, int> room2)
        {
            RoomLinks.Add(new Pair<Pair<int, int>, Pair<int, int>>(room1, room2));
            OnPropertyChanged("AddLink["+room1.First +","+room1.Second+","+room2.First+","+room2.Second+"]");
        }

        public void RemoveLink(Pair<int, int> room1, Pair<int, int> room2)
        {
            RoomLinks.Remove(new Pair<Pair<int, int>, Pair<int, int>>(room1, room2));
            OnPropertyChanged("RemoveLink[" + room1.First + "," + room1.Second + "," + room2.First + "," + room2.Second + "]");
        }

        public IEnumerable<Pair<Pair<int, int>, Pair<int, int>>> GetLinks()
        {
            return RoomLinks;
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
