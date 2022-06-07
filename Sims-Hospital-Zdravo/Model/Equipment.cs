/***********************************************************************
 * Module:  Equipment.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

using System;

public enum EquipmentType
{
    Static,
    Consumable
}

namespace Model
{
    public class Equipment
    {
        private int _id;
        private String _name { get; set; }
        private EquipmentType _type { get; set; }

        public Equipment(int id, String name, EquipmentType type)
        {
            this._id = id;
            this._name = name;
            this._type = type;
        }


        public int Id
        {
            get { return _id; }
            set { this._id = value; }
        }

        public String Name
        {
            get { return _name; }
            set { this._name = value; }
        }

        public EquipmentType Type
        {
            get { return _type; }
            set { this._type = value; }
        }


        public override string ToString()
        {
            return Name;
        }

        public void Update(Equipment equipment)
        {
            Name = equipment.Name;
            Type = equipment.Type;
        }
    }
}