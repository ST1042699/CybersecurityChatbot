public class TaskManager
{
    //this is where i added my taks 
    private readonly TaskStorageHelper _storage = new TaskStorageHelper();
    private readonly ActivityLogger _logger;

    public TaskManager(ActivityLogger logger)
    {
        _logger = logger;
    }

    public string AddTask(string title, string description, string reminder = "")
    {
        _storage.AddTask(title, description, reminder);
        string remText = string.IsNullOrEmpty(reminder) ? "no reminder" : reminder;
        _logger.Log($"Task added: '{title}' (Reminder: {remText})");
        return $"Task added with the description '{description}'. Would you like a reminder?";
    }

    public List<CyberTask> GetAllTasks() => _storage.LoadTasks();

    public void MarkAsComplete(int id)
    {
        _storage.MarkAsComplete(id);
        _logger.Log($"Task marked complete: ID {id}");
    }

    public void DeleteTask(int id)
    {
        _storage.DeleteTask(id);
        _logger.Log($"Task deleted: ID {id}");
    }
}