﻿using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public interface IDAO<T>
    {
        void Add(T entry);
        void Delete(string id);
        void Modify(T entry);
        List<T>? GetAll();
        T? GetOne(string id);
    }
}
