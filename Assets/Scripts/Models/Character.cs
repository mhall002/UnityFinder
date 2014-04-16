using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class Character : Entity
    {
        public int HP;
        public int Level;
        public Dictionary<string, int> Stats;
        public List<State> States;
        public List<Ability> Abilities;
    }
}
