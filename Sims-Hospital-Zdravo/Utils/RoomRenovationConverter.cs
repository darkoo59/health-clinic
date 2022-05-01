using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Utils
{
    public class RoomRenovationConverter : JsonConverter<RenovationAppointment>
    {
        private string path = "ref";

        public override void WriteJson(JsonWriter writer, RenovationAppointment value, JsonSerializer serializer)
        {
            var model = value;
            string path = "data.json#";
            if (model != null)
            {
                path += model.Room.Id;
            }
            else
            {
                path += -1;
            }

            string json =
                $"{{\r\n\t\"$ref\" : \"{path}\",\r\n\t\"id\" : \"{model.Id}\"\r\n," +
                $"\r\n\t\"Description\" : \"{model.Description}\"\r\n," +
                $"\r\n\t\"Type\" : \"{model.Type}\"\r\n," +
                $"\r\n\t\"Time\" : \"{model.Time}\"\r\n," +
                $"\r\n\t\"Room\" : \"{model.Room}\"\r\n}}";
            Console.WriteLine(json);
            File.WriteAllText(path, json);

            writer.WriteStartObject();
            writer.WritePropertyName("$ref");
            writer.WritePropertyName("Id");
            writer.WritePropertyName("Description");
            writer.WritePropertyName("Type");
            writer.WritePropertyName("Time");
            writer.WritePropertyName("Room");
            serializer.Serialize(writer, path);
            writer.WriteEndObject();
        }

        public override RenovationAppointment ReadJson(JsonReader reader, Type objectType, RenovationAppointment existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jobj = JObject.Load(reader);
            Room room = null;

            if (jobj["$ref"] != null)
            {
                string path = jobj["$ref"].Value<string>();
                string[] pathChunks = path.Split('#');
                string file = pathChunks[0];
                int id = Int32.Parse(pathChunks[1]);
                string json = File.ReadAllText(@"..\..\Resources\" + file);
                List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(json);
                foreach (Room rm in rooms)
                {
                    if (rm.Id == id)
                    {
                        room = rm;
                        break;
                    }
                }
            }

            RenovationAppointment renApp = jobj.ToObject<RenovationAppointment>();
            if (renApp != null)
                renApp.Room = room;

            return renApp;
        }
    }
}