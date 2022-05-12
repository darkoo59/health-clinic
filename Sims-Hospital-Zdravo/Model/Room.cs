/***********************************************************************
 * Module:  Room.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public enum RoomType
{
    OPERATION,
    EXAMINATION,
    MEETING,
    WAREHOUSE
};

namespace Model
{
    public class Room : INotifyPropertyChanged, IUpdateFilesObservable
    {
        private int _floor;
        private int _id;
        private string _roomNumber;
        private RoomType _type;
        private List<RoomEquipment> _roomEquipment;
        private int _quadrature;

        public Room()
        {
        }

        private List<IUpdateFilesObserver> _observers;

        public Room(int floor, int id, RoomType type, string roomNumber, int quadrature)
        {
            this._observers = new List<IUpdateFilesObserver>();
            this._floor = floor;
            this._id = id;
            this._type = type;
            this._roomEquipment = new List<RoomEquipment>();
            this._quadrature = quadrature;
            this._roomNumber = roomNumber;
        }


        public RoomType Type
        {
            get { return _type; }
            set
            {
                this._type = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int Floor
        {
            get { return _floor; }
            set
            {
                if (this._floor != value)
                {
                    this._floor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (this._id != value)
                {
                    this._id = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<RoomEquipment> RoomEquipment
        {
            get { return _roomEquipment; }

            set { _roomEquipment = value; }
        }

        public string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }

        public int Quadrature
        {
            get { return _quadrature; }
            set { _quadrature = value; }
        }

        public void AddEquipment(RoomEquipment re)
        {
            foreach (RoomEquipment eq in _roomEquipment)
            {
                if (eq.Equipment.Id == re.Equipment.Id)
                {
                    eq.Quantity += re.Quantity;
                    NotifyUpdated();
                    return;
                }
            }

            _roomEquipment.Add(re);
            NotifyUpdated();
        }

        public void RemoveEquipment(RoomEquipment re)
        {
            foreach (RoomEquipment eq in _roomEquipment)
            {
                if (eq.Equipment.Id == re.Equipment.Id)
                {
                    eq.Quantity -= re.Quantity;
                    NotifyUpdated();
                    return;
                }
            }

            _roomEquipment.Add(re);
            NotifyUpdated();
        }

        public bool HasEquipment(string equipmentName)
        {
            if (GetRoomEquipmentByName(equipmentName) != null)
                return true;
            return false;
        }

        public RoomEquipment GetRoomEquipmentByName(string equipmentName)
        {
            foreach (RoomEquipment roomEquipment in _roomEquipment)
            {
                if (roomEquipment.Equipment.Name.Equals(equipmentName))
                {
                    return roomEquipment;
                }
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return this._roomNumber + " " + this._type;
        }

        public void NotifyUpdated()
        {
            foreach (IUpdateFilesObserver observer in _observers)
            {
                observer.NotifyUpdated();
            }
        }

        public void AddObserver(IUpdateFilesObserver observer)
        {
            if (_observers == null)
            {
                _observers = new List<IUpdateFilesObserver>();
            }

            _observers.Add(observer);
        }

        public void RemoveObserver(IUpdateFilesObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}