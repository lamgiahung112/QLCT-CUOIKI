using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public interface IWorkTaskRetriever
    {
        WorkSession? GetLastest(string employeeID);
        List<WorkSession>? GetUnfinished(string employeeID);
    }
}
