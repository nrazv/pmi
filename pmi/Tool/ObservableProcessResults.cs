


public class ObservableProcessResults
{
    private readonly Dictionary<string, string> _results = new();

    public event EventHandler<ProcessResultChangedEventArgs>? ResultChanged;

    public string? Get(string key)
    {
        return _results.TryGetValue(key, out var value) ? value : null;
    }

    public void Append(string key, string value)
    {
        if (!_results.ContainsKey(key))
        {
            _results[key] = "";
        }
        _results[key] += $"\n{value}";

        // Raise the event
        ResultChanged?.Invoke(this, new ProcessResultChangedEventArgs(key, _results[key]));
    }

    public void Remove(string key)
    {
        _results.Remove(key);
    }
}


public class ProcessResultChangedEventArgs : EventArgs
{
    public string Key { get; }
    public string NewValue { get; }

    public ProcessResultChangedEventArgs(string key, string newValue)
    {
        Key = key;
        NewValue = newValue;
    }
}
