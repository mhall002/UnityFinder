using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    class SimpleCharacter : Entity, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int hp;
        public int HP
        {
            set
            {
                hp = HP;
                OnPropertyChanged("HP");
            }
            get
            {
                return hp;
            }
        }
        private int xp;
        public int XP
        {
            set
            {
                xp = XP;
                OnPropertyChanged("XP");
            }
            get
            {
                return xp;
            }
        }
        private int level;
        public int Level
        {
            set
            {
                xp = XP;
                OnPropertyChanged("Level");
            }
            get
            {
                return level;
            }
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
