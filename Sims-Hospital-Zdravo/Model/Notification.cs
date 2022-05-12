namespace Sims_Hospital_Zdravo.Model
{
    public class Notification
    {
        public string Content { get; set; }
        public int Id { get; set; }

        public Notification(string content, int id)
        {
            this.Content = content;
            this.Id = id;
        }
    }
}