using System;
using System.Collections.Generic;
using System.Text.Json;

namespace KvJson
{
    public class Serializer<T> where T : new()
    {
        public string Serialize(IEnumerable<T> input)
        {
            //Get all properties and create a new JSON object with these properties names as keys and
            //array of values of respective property in each element as values
            //e.g. [{"id": 1, "name": "Satish" }, {"id": 2, "name": "Yadav"}]
            //to: {"id": [1, 2], "name": ["Satish", "Yadav"]}
            var props = typeof(T).GetProperties();
            var headerAndValues = new Dictionary<string, List<dynamic>>();
            foreach (var prop in props)
            {
                headerAndValues.Add(prop.Name, new List<dynamic>());
            }
            foreach (var item in input)
            {
                foreach (var prop in props)
                {
                    headerAndValues[prop.Name].Add(prop.GetValue(item));
                }
            }
            return JsonSerializer.Serialize(headerAndValues);
        }
        public IEnumerable<T> Deserialize(string input)
        {
            var obj = JsonSerializer.Deserialize<JsonElement>(input);
            var returnEnumberable = new List<T>();
            foreach (var prop in typeof(T).GetProperties())
            {
                int i = 0;
                foreach (var item in obj.GetProperty(prop.Name).EnumerateArray())
                {
                    if (returnEnumberable.Count <= i)
                        returnEnumberable.Add(new T());
                    prop.SetValue(returnEnumberable[i], Convert.ChangeType(item.ToString(), prop.PropertyType));
                    i++;
                }
            }
            return returnEnumberable;
            //throw new NotImplementedException();
        }
    }
}
