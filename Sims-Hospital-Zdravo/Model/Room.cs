/***********************************************************************
 * Module:  Room.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
public enum RoomType { OPERATION, EXAMINATION, MEETING, WAREHOUSE };

namespace Model
{
    public class Room : INotifyPropertyChanged, IUpdateFilesObservable
    {
        private int Floor;
        private int Id;
        private RoomType Type;
        private List<RoomEquipment> roomEquipment;
        public Room() { }
        private List<IUpdateFilesObserver> observers;
        public Room(int floor, int id, RoomType type)
        {
            this.Floor = floor;
            this.Id = id;
            this.Type = type;
            this.roomEquipment = new List<RoomEquipment>();
            this.observers = new List<IUpdateFilesObserver>();
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

        public List<RoomEquipment> _RoomEquipment
        {
            get
            {
                return roomEquipment;

            }

            set
            {
                roomEquipment = value;
            }
        }

        public void AddEquipment(RoomEquipment re)
        {
            foreach (RoomEquipment eq in roomEquipment)
            {
                if (eq._Equip._Id == re._Equip._Id)
                {
                    eq._Quantity += re._Quantity;
                    NotifyUpdated();
                    return;
                }
            }

            roomEquipment.Add(re);
            NotifyUpdated();

        }

        public void RemoveEquipment(RoomEquipment re)
        {
            foreach (RoomEquipment eq in roomEquipment)
            {
                if (eq._Equip._Id == re._Equip._Id)
                {
                    eq._Quantity -= re._Quantity;
                    NotifyUpdated();
                    return;
                }
            }

            roomEquipment.Add(re);
            NotifyUpdated();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return this.Id + " " + this.Type; 

        }

        public void NotifyUpdated()
        {
           foreach(IUpdateFilesObserver observer in observers)
            {
                observer.NotifyUpdated();
            }
        }

        public void AddObserver(IUpdateFilesObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IUpdateFilesObserver observer)
        {
            observers.Remove(observer);
        }
    }
}