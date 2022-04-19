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
        public int EquipmentId;
        private int Quantity;
        public RoomEquipment(int equipmentId) {
            this.EquipmentId = equipmentId;
        }



        public int _EquipmentId
        {
            get
            {
                return EquipmentId;
            }
            set
            {
                this.EquipmentId = value;
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
                this.Quantity = value;
            }
        }





    }
}