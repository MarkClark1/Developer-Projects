using System.Collections.Generic;

namespace DvdLibrary.Models
{
    public interface IDvdRepository
    {
        Dvd Get(int dvdId);
        void Create(Dvd newDvd);
        void Update(Dvd updatedDvd);
        void Delete(int dvdId);        
        IEnumerable<Dvd> GetAll();
        IEnumerable<Dvd> GetByTitle(string title);
        IEnumerable<Dvd> GetByYear(string realeaseYear);
        IEnumerable<Dvd> GetByDirector(string director);
        IEnumerable<Dvd> GetByRating(string rating);
    }
}