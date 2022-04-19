/***********************************************************************
 * Module:  Equipment.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

using System;

public enum EquipmentType { Static, Consumable }

namespace Model
{
    public class Equipment
    {
        private int Id;
        private String Name { get; set; }
        private int Quantity { get; set; }
        private EquipmentType Type { get; set; }

        public Equipment(int id, String name, int quantity, EquipmentType type)
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.Type = type;
        }


        public int _Id
        {
            get { return Id; }
            set { this.Id = value; }
        }
        public String _Name
        {
            get { return Name; }
            set { this.Name = value; }
        }
        public int _Quantity
        {
            get { return Quantity; }
            set { this.Quantity = value; }
        }
        public EquipmentType _Type
        {
            get { return Type; }
            set { this.Type = value; }
        }




    }
}