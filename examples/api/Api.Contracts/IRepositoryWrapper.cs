using Api.Contracts;

namespace Api.Contracts
{
    public interface IRepositoryWrapper
    {
        IPersonRepository Persons { get; }

        void Save();
    }
}
