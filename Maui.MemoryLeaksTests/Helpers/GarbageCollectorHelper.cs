namespace Maui.MemoryLeaksTests.Helpers;

public static class GarbageCollectorHelper
{
    public static async Task Collect()
    {
        await Task.Yield();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
