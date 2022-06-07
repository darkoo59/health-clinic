namespace Sims_Hospital_Zdravo.Model
{
    public class MarkStatistic
    {
        private int _mark;
        private int _quantity;

        public MarkStatistic(int mark, int quantity)
        {
            _mark = mark;
            _quantity = quantity;
        }

        public int Mark
        {
            get => _mark;
            set => _mark = value;
        }

        public int Quantity
        {
            get => _quantity;
            set => _quantity = value;
        }

        private string GetStarsByMark()
        {
            switch (_mark)
            {
                case 1: return "☆            ";
                case 2: return "☆☆         ";
                case 3: return "☆☆☆      ";
                case 4: return "☆☆☆☆   ";
                case 5: return "☆☆☆☆☆";
                default: return "";
            }
        }

        public override string ToString()
        {
            return GetStarsByMark() + " " + _quantity;
        }
    }
}