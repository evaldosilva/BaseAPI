namespace BaseAPI.Models.Domain;

public record HardwareStatus(TimeSpan AppRunningTime, TimeSpan TotalProcessorTime, float TotalMemory);