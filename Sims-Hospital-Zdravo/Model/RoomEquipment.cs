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
        private Equipment Equipment;
        private int Quantity;


        public RoomEquipment(Equipment equipment, int quantity)
        {
            this.Equipment = equipment;
            this.Quantity = quantity;
        }


        public Equipment _Equip
        {
            get
            {
                return Equipment;
            }

            set
            {
                Equipment = value;
            }
        }

        public int _Quantity
        {
            get
            {
                return Quantity;
            }

            set
            {
                Quantity = value;
            }
        }

        public override string ToString()
        {
            return this.Equipment._Name + " " + Quantity;
        }
    }
}