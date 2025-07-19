# TimeTravel
Time Travel Key-Value Store - Developer Challenge
=================================================

Description:
------------
This is a typed key-value store that supports time travel features.
It stores all mutations, allows querying values at any point in time,
shows differences between timestamps, and allows rollback without losing history.


Why It’s Structured This Way:
-----------------------------
- Core logic is encapsulated in a single class: TimeTravel
- Uses a Dictionary<string, SortedList<int, string>> to store full mutation history per key
- Each key holds a time-ordered list of values tagged with a unique timestamp.
- Rollback is implemented by tracking a timestamp pointer (__currentTime)
- History is preserved regardless of rollback operations


Tradeoffs and Known Limitations:
--------------------------------
- No optimization like binary search for historical lookups (uses linear search)
- Not thread-safe (no locks or concurrency control)
- No data persistence (data is lost on app restart)
- No delete functionality yet (cannot remove a key or value)
- Not suitable for very large datasets without performance tuning


How to Run the Tests:
---------------------
1. Install .NET SDK (https://dotnet.microsoft.com/download)
2. Open a terminal or command prompt in the solution folder
3. Run the following command:

   dotnet test

Tests are written using xUnit and validate the core behaviors such as:

- Set() and Get() behavior over time
- Rollback to earlier timestamps
- Detect changes with ChangedBetween(t1, t2)
- History integrity after rollback
