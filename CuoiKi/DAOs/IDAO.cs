using System.Collections.Generic;
using System.Windows.Documents;

namespace CuoiKi.DAOs
{
    public interface IDAO<T>
    {
        void Add(T entry);
        void Delete(T entry);
        void Modify(T entry);
        List<T> GetAll();
        T? GetOne(string id);
    }
}
