using Newtonsoft.Json;
using System.IO;
using System.Windows;

public class TaskStorageHelper
{
    // this classes is used to help me to store the tasks 
    private const string FilePath = "tasks.json";

    public List<CyberTask> LoadTasks()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new List<CyberTask>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<CyberTask>>(json) ?? new List<CyberTask>();
        }
        catch
        {
            return new List<CyberTask>();
        }
    }

    public void SaveTasks(List<CyberTask> tasks)
    {
        try
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving tasks: {ex.Message}");
        }
    }

    public void AddTask(string title, string description, string reminder)
    {
        var tasks = LoadTasks();
        int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

        tasks.Add(new CyberTask
        {
            Id = newId,
            Title = title,
            Description = description,
            Reminder = reminder,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        });

        SaveTasks(tasks);
    }

    public void MarkAsComplete(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsComplete = true;
            SaveTasks(tasks);
        }
    }

    public void DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            SaveTasks(tasks);
        }
    }
}