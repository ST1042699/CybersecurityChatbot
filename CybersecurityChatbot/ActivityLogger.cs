public class ActivityLogger
{
    // this is where all the activity logs will be stored 
    private List<string> _log = new List<string>();

    public void Log(string action)
    {
        string entry = $"[{DateTime.Now:HH:mm}] {action}";
        _log.Add(entry);
    }

    public string GetRecentLog(int count = 10)
    {
        var recent = _log.TakeLast(count).ToList();
        if (recent.Count == 0) return "No activities yet.";
        return "Here's a summary of recent actions:\n" +
               string.Join("\n", recent.Select((e, i) => $"{i + 1}. {e}"));
    }

    public string GetFullLog()
    {
        return "Full Activity History:\n" +
               string.Join("\n", _log.Select((e, i) => $"{i + 1}. {e}"));
    }

    public int GetCount() => _log.Count;
}