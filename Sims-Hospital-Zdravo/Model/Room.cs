/***********************************************************************
 * Module:  Room.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
using Enums;

public enum RoomType { OPERATION, EXAMINATION, MEETING, WAREHOUSE };

namespace Model
{
    public class Room
    {
        private int Floor;
        private int Id;

        private RoomType Type;


        public RoomType _Type 
        {
            get
            {
                return Type;
            }
            set
            {                 
                this.Type = value;
            }
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
               this.Floor = value;
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
               this.Id = value;
         }
      }

    }
}