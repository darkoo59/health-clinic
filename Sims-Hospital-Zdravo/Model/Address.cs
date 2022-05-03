/***********************************************************************
 *Module:  Address.cs
* Author:  I
* Purpose: Definition of the Class Model.Address
 ***********************************************************************/

using System;

namespace Model
{
    public class Address
    {
        private City city;

        private string Street;
        private string Number;

        public City _City { get; set; }

        public string _Street { get; set; }
        public string _Number { get; set; }
    }
}
