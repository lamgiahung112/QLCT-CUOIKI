using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Constants
{
    public class EnumMapper
    {
        public static String mapToString(EmployeeStatus status)
        {
            return status == EmployeeStatus.Active
                    ? nameof(EmployeeStatus.Active)
                    : nameof(EmployeeStatus.Disabled);
        }
        public static String mapToString(Gender gender)
        {
            return gender == Gender.Male
                    ? nameof(Gender.Male)
                    : nameof(Gender.Female);
        }
        public static String mapToString(Role role)
        {
            return role == Role.Staff
                    ? nameof(Role.Staff)
                    : role == Role.Hr
                    ? nameof(Role.Hr)
                    : nameof(Role.Manager);
        }
    }
}
