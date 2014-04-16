using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Campaign
    {
        public List<Character> Characters;
        public Dictionary<int, int> RoomLinks;
        public List<Room> Rooms;
    }
}
