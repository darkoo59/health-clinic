using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums
{
    public class Enums
    {

        public enum BloodType { APOSITIVE, ANEGATIVE, ABPOSITIVE, ABNEGATIVE, BPOSITIVE, BNEGATIVE, OPOSITIVE, ONEGATIVE };
        public enum GenderType { MALE, FEMALE };
        public enum MaritalType { MARRIED, UNMARRIED, DIVORCED, WIDOVER };
        public enum RoomType { OPERATION, EXAMINATION, MEETING, WAREHOUSE };
        
        public enum Specialty { NEUROLOGIST, DERMATOLOGIST, UROLOGIST, CARDIOLOGIST, ENDOCRINOLOGIST, GASTROENTEROLOGIST, SURGEON, ONCOLOGIST}
    }
}
