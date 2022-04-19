/***********************************************************************
 * Module:  Room.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
public enum RoomType { OPERATION, EXAMINATION, MEETING, WAREHOUSE };

namespace Model
{
    public class Room : INotifyPropertyChanged
    {
        private int Floor;
        private int Id;
        private RoomType Type;
        private List<RoomEquipment> roomEquipment;

        public Room(int floor, int id, RoomType type)
        {
            this.Floor = floor;
            this.Id = id;
            this.Type = type;
            this.roomEquipment = new List<RoomEquipment>();
        }


        public RoomType _Type
        {
            get
            {
                return Type;
            }
            set
            {
                this.Type = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int _Floor
        {
            get
            {
                return Floor;
            }
            set
            {
                if (this.Floor != value)
                {
                    this.Floor = value;
                    OnPropertyChanged();
                }

            }
        }

        public int _Id
        {
            get
            {
                return Id;
            }
            set
            {
                if (this.Id != value)
                {
                    this.Id = value;
                    OnPropertyChanged();
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}