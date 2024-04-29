using Api.Contracts;
using Api.Entities;

namespace Api.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext _repoContext;

        private IPersonRepository? _persons;

        public IPersonRepository Persons
        {
            get
            {
                if (_persons == null)
                {
                    _persons = new PersonRepository(_repoContext);
                }

                return _persons;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
