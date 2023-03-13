using System;

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
        public static String mapToString(WorkSessionStatus status)
        {
            return status == WorkSessionStatus.CheckedIn
                    ? nameof(WorkSessionStatus.CheckedIn)
                    : nameof(WorkSessionStatus.CheckedOut);
        }

        public static String mapToString(WorkStatus status)
        {
            return status == WorkStatus.WIP
                ? nameof(WorkStatus.WIP)
                : status == WorkStatus.NeedsReview
                ? nameof(WorkStatus.NeedsReview)
                : nameof(WorkStatus.Done);
        }
    }
}
