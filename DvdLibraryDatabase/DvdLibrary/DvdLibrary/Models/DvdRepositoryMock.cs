using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary.Models
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<Dvd> _dvd;

        static DvdRepositoryMock()
        {
            _dvd = new List<Dvd>()
            {
                new Dvd { DvdId=1, Title="Star Wars: Empire Strikes Back", ReleaseDate="1980", Director="Irvin Kerschner", Rating="PG" },
                new Dvd { DvdId=2, Title="Back to the Future", ReleaseDate="1985", Director="Robert Zemeckis", Rating="PG" },
                new Dvd { DvdId=3, Title="The Lord of the Rings", ReleaseDate="2001", Director="Peter Jackson", Rating="PG-13" },
                new Dvd { DvdId=4, Title="Raiders of the Lost Ark", ReleaseDate="1981", Director="Steven Spielberg", Rating="PG" },
                new Dvd { DvdId=5, Title="Jurassic Park", ReleaseDate="1993", Director="Steven Spielberg", Rating="PG-13" },
            };
        }

        public IEnumerable<Dvd> GetAll()
        {
            return _dvd;
        }

        public Dvd Get(int dvdId)
        {
            return _dvd.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public void Create(Dvd newDvd)
        {
            _dvd.Add(newDvd);
        }

        public void Update(Dvd updatedDvd)
        {
            _dvd.RemoveAll(d => d.DvdId == updatedDvd.DvdId);
            _dvd.Add(updatedDvd);
        }

        public void Delete(int dvdId)
        {
            _dvd.RemoveAll(d => d.DvdId == dvdId);
        }

        public IEnumerable<Dvd> GetByTitle(string title)
        {
            return _dvd.Where(t => t.Title == title);
        }

        public IEnumerable<Dvd> GetByYear(string releaseDate)
        {
            return _dvd.Where(r => r.ReleaseDate == releaseDate);
        }

        public IEnumerable<Dvd> GetByDirector(string director)
        {
            return _dvd.Where(d => d.Director == director);
        }

        public IEnumerable<Dvd> GetByRating(string rating)
        {
            return _dvd.Where(r => r.Rating == rating);
        }
    }
}