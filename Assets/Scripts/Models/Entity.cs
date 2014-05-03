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
        private string name = "Bob";
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
        private string description = "";
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private string gmDescription = "";
        public string GMDescription
        {
            get
            {
                return gmDescription;
            }
            set
            {
                gmDescription = value;
                OnPropertyChanged("GMDescription");
            }
        }

        public bool Visible = true;

        private string image = "Characters/knight";
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        private int height = 1;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }
        private int width = 1;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }


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
            Debug.Log("Description: " + clone.Description);
            clone.GMDescription = entity.GMDescription;
            clone.Image = entity.Image;
            clone.Height = entity.Height;
            clone.Width = entity.Width;
            return clone;
        }

        public void SaveAs(Entity entity)
        {
            name = entity.Name;
            description = entity.Description;
            gmDescription = entity.GMDescription;
            image = entity.Image;
            height = entity.Height;
            width = entity.Width;
            OnPropertyChanged("All");
        }

        private Vector4 position = new Vector4(-1,-1,-1,-1);
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
