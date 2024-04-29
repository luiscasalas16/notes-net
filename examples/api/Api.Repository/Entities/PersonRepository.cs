using Api.Contracts;
using Api.Entities;

namespace Api.Repository
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return FindAll()
                .OrderBy(o => o.FullName)
                .ToList();
        }

        public PageList<Person> GetPagePersons(PersonParameters parameters)
        {
            var persons = FindAll();

            SearchByName(ref persons, parameters);

            var sortedPersons = SortHelper<Person>.ApplySort(persons, parameters.OrderBy);

            return PageList<Person>.ToPagedList(sortedPersons,
                parameters.PageNumber, 
                parameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Person> persons, PersonParameters parameters)
        {
            if (!persons.Any() || string.IsNullOrWhiteSpace(parameters.SearchName))
                return;

            persons = persons.Where(o => o.SearchName != null && o.SearchName.ToLower().Contains(parameters.SearchName.Trim().ToLower()));
        }

        public Person? GetPersonById(int personId)
        {
            return FindByCondition(o => o.Id.Equals(personId))
                .FirstOrDefault();
        }

        public void CreatePerson(Person person)
        {
            Create(person);
        }

        public void UpdatePerson(Person person)
        {
            Update(person);
        }

        public void DeletePerson(Person person)
        {
            Delete(person);
        }
    }
}
