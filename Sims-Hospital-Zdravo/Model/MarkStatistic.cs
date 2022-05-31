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

        public override string ToString()
        {
            return "Mark : " + _mark + " Ratings: " + _quantity;
        }
    }
}