public class TimeTravel
{ 

    int? _currentTime = null;
    int timestamp = 0;
    Dictionary<string, SortedDictionary<int, string>> keyValuePairs = new();

    public void Set(string key, string value)
    {      

        if (!keyValuePairs.ContainsKey(key))
        {
            keyValuePairs[key] = new SortedDictionary<int, string>();
        }
        timestamp++;
        keyValuePairs[key][timestamp] = value;

    }

    public string Get(string key, int timestamp)
    {
        int effectiveTime = _currentTime ?? timestamp;
        _currentTime = null;

        if (!keyValuePairs.ContainsKey(key))
            return null;

        var timeSeries = keyValuePairs[key];
        int closestTime = -1;

        foreach (var time in timeSeries.Keys.Reverse())
        {
            if (time <= effectiveTime)
            {
                closestTime = time;
                break;
            }
        }

        if (closestTime == -1)
            return null;

        return timeSeries[closestTime];
    }

    public List<string> ChangedBetween(int t1, int t2)
    {
        List<string> changedKeys = new List<string>();

        foreach (var kvp in keyValuePairs)
        {
            var timeSeries = kvp.Value;
            string previousValue = null;
            bool isFirstInRange = true;
            bool changed = false;

            foreach (var entry in timeSeries)
            {
                int time = entry.Key;
                string value = entry.Value;

                if (time < t1)
                    continue;

                if (time > t2)
                    break;

                if (isFirstInRange)
                {
                    previousValue = value;
                    isFirstInRange = false;
                    continue;
                }

                if (value != previousValue)
                {
                    changed = true;
                    break;
                }

                previousValue = value;
            }

            if (changed)
                changedKeys.Add(kvp.Key);
        }

        return changedKeys;
    }

    public void Rollback(int timestamp)
    {
        _currentTime = timestamp;
    }
}