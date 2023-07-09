using BaseAPI.Models.Domain;

namespace BaseAPI.Models.Responses;

public record AliveResponse(string Application, string Version, HardwareStatus HardwareStatus);