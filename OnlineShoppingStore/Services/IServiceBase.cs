namespace OnlineShoppingStore.Services
{
    public interface IServiceBase<T>
    {
       List<T> GetAll();
        int Add(T Model);
        T GetDetails(int id);
        int Update(int id, T Model);
        int Delete(int id);
        T Search(string name);

    }
}
