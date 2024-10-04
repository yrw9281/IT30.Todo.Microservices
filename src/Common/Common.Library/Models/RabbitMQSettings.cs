namespace Common.Library.Models;

internal class RabbitMQSettings
{
    public static string SectionName { get; } = "RabbitMQSettings";
    public string HostName { get; init; } = null!;
    public int Port { get; init; }
}
