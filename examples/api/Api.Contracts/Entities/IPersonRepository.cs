using Api.Entities;

namespace Api.Contracts
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        IEnumerable<Person> GetAllPersons();
        PageList<Person> GetPagePersons(PersonParameters parameters);
        Person? GetPersonById(int personsId);
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
