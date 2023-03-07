using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    public interface IWorkTaskRetriever
    {
        WorkSession? GetLastest(string employeeID);
        WorkSession? GetUnfinished(string employeeID);
    }
}
