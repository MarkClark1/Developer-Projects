using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models
{
    public class DvdMapper
    {
        public static Dvd ToDVD(JObject input)
        {
            int parsedId;
            if (input["dvdId"] == null)
            {
                parsedId = 0;
            }
            else
            {
                parsedId = (int)input["dvdId"];
            }
            return new Dvd
            {
                //dvdId = (int?)input["dvdId"] ?? 0,
                DvdId = parsedId,
                Title = input["title"].ToString(),
                Rating = input["rating"].ToString(),
                ReleaseDate = input["realeaseYear"].ToString(),
                Director = input["director"].ToString(),
                Notes = input["notes"]?.ToString()
            };
        }

        public static JObject ToJSON(Dvd input)
        {
            JObject result = new JObject();
            result.Add("dvdId", input.DvdId);
            result.Add("title", input.Title);
            result.Add("rating", input.Rating);
            result.Add("realeaseYear", input.ReleaseDate);
            result.Add("director", input.Director);
            result.Add("notes", input.Notes);
            return result;
        }

        public static IEnumerable<JObject> ToJsonList(IEnumerable<Dvd> inputDvds)
        {
            foreach (var dvd in inputDvds)
            {
                yield return ToJSON(dvd);
            }
            yield break;
        }
    }
}