using System.ComponentModel;

namespace myCRUD.Enums
{
    public enum TasksStatus
    {
        [Description("To do")]
        Todo = 1,
        [Description("Doing")]
        Doing = 2,
        [Description("Conclued")]
        Conclued = 3
    }
}