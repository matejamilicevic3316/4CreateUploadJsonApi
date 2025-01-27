using System.ComponentModel;

namespace Domain
{
    public enum Status
    {
        [Description("Not Started")]
        NotStarted,
        Ongoing,
        Completed
    }
}
