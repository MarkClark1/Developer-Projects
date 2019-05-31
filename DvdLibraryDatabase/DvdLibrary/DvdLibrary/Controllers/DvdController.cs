using DvdLibrary.Factories;
using DvdLibrary.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace DvdLibrary.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        private static IDvdRepository _repo;
        static DvdController()
        {
            _repo = DvdRepositoryFactory.Create();
        }

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Dvd> found = _repo.GetAll();
            IEnumerable<JObject> output = DvdMapper.ToJsonList(found);
            return Ok(output);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            var found = _repo.Get(id);

            if (found == null)
            {
                return NotFound();
            }
            JObject result = DvdMapper.ToJSON(found);
            return Ok(result);
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTitle(string title)
        {
            IEnumerable<Dvd> found = _repo.GetByTitle(title);

            if (found == null)
            {
                return NotFound();
            }
            IEnumerable<JObject> output = DvdMapper.ToJsonList(found);
            return Ok(output);
        }

        [Route("dvds/year/{realeaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDate(string releaseDate)
        {
            IEnumerable<Dvd> found = _repo.GetByYear(releaseDate);

            if (found == null)
            {
                return NotFound();
            }
            IEnumerable<JObject> output = DvdMapper.ToJsonList(found);
            return Ok(output);
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetRating(string rating)
        {
            IEnumerable<Dvd> found = _repo.GetByRating(rating);

            if (found == null)
            {
                return NotFound();
            }
            IEnumerable<JObject> output = DvdMapper.ToJsonList(found);
            return Ok(output);
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(JObject input)
        {
            Dvd dvd = DvdMapper.ToDVD(input);
            _repo.Create(dvd);

            JObject result = DvdMapper.ToJSON(dvd);
            return Created($"dvd/{dvd.DvdId}", result);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, JObject input)
        {
            Dvd dvd = DvdMapper.ToDVD(input);
            _repo.Update(dvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
