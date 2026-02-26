namespace Mission8IS413.Models
{
    public interface ITaskRepository
    {
        List<TaskModel> Tasks { get; }

        public void AddTask(TaskModel task);

        public void EditTask(TaskModel task);
        public void DeleteTask(TaskModel task);
    }
}
