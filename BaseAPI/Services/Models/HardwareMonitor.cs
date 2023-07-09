using BaseAPI.Models.Domain;
using System.Diagnostics;

namespace BaseAPI.Services.Models;

internal static class HardwareMonitor
{
    private static TimeOnly initialTime;
    private static TimeSpan totalProcessorTime;
    private static long appMemory;

    public static HardwareStatus GetStatus()
    {
        GetSnapshot();
        return new(TimeOnly.FromDateTime(DateTime.Now) - initialTime, totalProcessorTime, appMemory);
    }

    internal static void Start()
    {
        initialTime = TimeOnly.FromDateTime(DateTime.Now);
    }

    private static void GetSnapshot()
    {
        var currProc = Process.GetCurrentProcess();
        appMemory = currProc.PrivateMemorySize64 / 1024 / 1024;
        totalProcessorTime = currProc.TotalProcessorTime;
    }
}