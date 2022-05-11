using System.Collections.Generic;
using Newtonsoft.Json;
using Sims_Hospital_Zdravo.Model;
using Newtonsoft.Json;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class NotificationDataHandler
    {
        private string _path = @"..\..\Resources\notifications.txt";


        public List<Notification> ReadAll()
        {
            string notificationsSerialized = System.IO.File.ReadAllText(_path);
            List<Notification> notifications = JsonConvert.DeserializeObject<List<Notification>>(notificationsSerialized,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
                });
            return notifications;
        }

        public void Write(List<Notification> notifications)
        {
            string serialized = JsonConvert.SerializeObject(notifications,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
                });
            System.IO.File.WriteAllText(_path, serialized);
        }
    }
}