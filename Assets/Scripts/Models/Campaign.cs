using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Campaign
    {
        public string Name;
        public List<Character> Characters;
        public Dictionary<int, int> RoomLinks;
        public Room[,] Rooms; // 17 * 9

        public Campaign ()
        {
            Rooms = new Room[9, 17];
            List<Character> Characters = new List<Character>();
            Dictionary<int, int> RoomLinks = new Dictionary<int, int>();
        }
    }
}
