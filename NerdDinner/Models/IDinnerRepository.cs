using System.Linq;
using NerdDinner.Models;

public interface IDinnerRepository
{

    IQueryable<Dinner> FindAllDinners();
    IQueryable<Dinner> FindUpcomingDinners();
    Dinner GetDinner(int id);

    void Add(Dinner dinner);
    void Delete(Dinner dinner);

    IQueryable<Dinner> FindByLocation(decimal latitude, decimal longitude);

    void ModifyState(Dinner dinner);

    void Save();
}