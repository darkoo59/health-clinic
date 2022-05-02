/***********************************************************************
 * Module:  RoomEquipment.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.RoomEquipment
 ***********************************************************************/

using System;

namespace Model
{
    public class RoomEquipment
    {
        private Equipment _equipment;
        private int _quantity;


        public RoomEquipment(Equipment equipment, int quantity)
        {
            this._equipment = equipment;
            this._quantity = quantity;
        }


        public Equipment Equipment
        {
            get { return _equipment; }

            set { _equipment = value; }
        }

        public int Quantity
        {
            get { return _quantity; }

            set { _quantity = value; }
        }

        public override string ToString()
        {
            return this._equipment.Name + " " + _quantity;
        }
    }
}