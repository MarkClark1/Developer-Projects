using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace DvdLibrary.Models
{
    public class DvdRepositoryEF : IDvdRepository
    {
        private static DvdLibraryEntities _data = new DvdLibraryEntities();

        public void Create(Dvd newDvd)
        {
                _data.Dvds.Add(newDvd);
                _data.SaveChanges();
        }

        public void Delete(int dvdId)
        {
            var dvd = _data.Dvds.FirstOrDefault(d => d.DvdId == dvdId);
            if (dvd != null)
            {
                _data.Dvds.Remove(dvd);
                _data.SaveChanges();
            }
        }

        public Dvd Get(int dvdId)
        {
            return _data.Dvds.FirstOrDefault(d => d.DvdId == dvdId);
        }

        public IEnumerable<Dvd> GetAll()
        {
            return _data.Dvds;
        }

        public IEnumerable<Dvd> GetByDirector(string director)
        {
            return _data.Dvds.Where(d => d.Director == director);
        }

        public IEnumerable<Dvd> GetByRating(string rating)
        {
            return _data.Dvds.Where(r => r.Rating == rating);
        }

        public IEnumerable<Dvd> GetByTitle(string title)
        {
            return _data.Dvds.Where(t => t.Title == title);
        }

        public IEnumerable<Dvd> GetByYear(string releaseDate)
        {
            return _data.Dvds.Where(r => r.ReleaseDate == releaseDate);
        }

        public void Update(Dvd updatedDvd)
        {
            Delete(updatedDvd.DvdId);
            Create(updatedDvd);
            //_data.Entry(updatedDvd).State = EntityState.Modified;
            _data.SaveChanges();
        }
    }
}