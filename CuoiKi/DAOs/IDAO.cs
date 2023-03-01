namespace CuoiKi.DAOs
{
    public interface IDAO<T>
    {
        void Add(T entry);
        void Delete(T entry);
        void Modify(T entry);
    }
}
