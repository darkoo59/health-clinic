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
        private int EquipmentId;
        private int Quantity;


        public RoomEquipment(int equipmentId, int quantity)
        {
            this.EquipmentId = equipmentId;
            this.Quantity = quantity;
        }


        public int _EquipmentId
        {
            get
            {
                return EquipmentId;
            }

            set
            {
                EquipmentId = value;
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

    }
}