using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Entity
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name;
        public string Description;
        public bool Visible;
        public string Image;
        public int Height;
        public int Width;
        public EntityType Type;

        private Vector4 position;
        public Vector4 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public string Owner;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public enum EntityType
    {
        PlayerCharacter,
        NonPlayerCharacter
    }
}
