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

        public Guid Uid;
        public string Name = "Bob";
        public string Description = "";
        public bool Visible = true;
        public string Image = "Characters/knight";
        public int Height = 1;
        public int Width = 1;
        public EntityType Type;

        public Entity(Guid uid)
        {
            Uid = uid;
        }

        public Entity()
        {
            Uid = Guid.NewGuid();
        }

        public static Entity Clone(Entity entity)
        {
            Entity clone = new Entity();
            clone.Name = entity.Name;
            clone.Description = entity.Description;
            clone.Image = entity.Image;
            clone.Height = entity.Height;
            clone.Width = entity.Width;
            return clone;
        }

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
        public string Owner = "";

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
