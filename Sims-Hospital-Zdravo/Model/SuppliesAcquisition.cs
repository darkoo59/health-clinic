using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class SuppliesAcquisition
    {
        private int _id;
        private List<RoomEquipment> _roomEquipments;
        private TimeInterval _time;
        private Boolean _acquistionFlag;

        public SuppliesAcquisition(int id, List<RoomEquipment> equipments,TimeInterval time)
        {
            this._time = time;
            this._roomEquipments = equipments;
            this._id = id;
            this._acquistionFlag = true;
        }


        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }

        public Boolean AcquistionFlag
        {
            get { return _acquistionFlag; }

            set { _acquistionFlag = value;  }
        }

        public TimeInterval Time
        {
            get { return _time; }
            set { _time = value; }
        }


        public List<RoomEquipment> RoomEquipments
        {
            get { return _roomEquipments; }

            set { _roomEquipments = value; }
        }
    }
}
