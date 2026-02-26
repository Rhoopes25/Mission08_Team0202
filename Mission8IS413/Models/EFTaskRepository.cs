namespace Mission8IS413.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskContext _context;
        public EFTaskRepository(TaskContext temp)
        {
            _context = temp;
        }

        public List<TaskModel> Tasks => _context.Tasks.ToList();

        public void AddTask(TaskModel task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void DeleteTask(TaskModel task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void EditTask(TaskModel task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}