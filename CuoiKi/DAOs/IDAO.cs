using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public interface IDAO<T>
    {
        void Add(T entry);
        void Delete(string id);
        void Modify(string id, T entry);
        List<T>? GetAll(string id);
        T? GetOne(string id);
    }
}
