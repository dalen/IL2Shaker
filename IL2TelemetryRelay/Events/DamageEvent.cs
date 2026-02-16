using System.Numerics;

namespace IL2TelemetryRelay.Events;

public record DamageEvent : Event
{
    public Vector3 Offset;

    /// <summary>
    /// Hit force in N in each direction
    /// </summary>
    public Vector3 Force;

    public DamageEvent(uint tick, byte[] packet, int offset)
        : base(tick, packet, offset)
    {
        Offset = new()
        {
            X = BitConverter.ToSingle(packet, offset),
            Y = BitConverter.ToSingle(packet, offset + 4),
            Z = BitConverter.ToSingle(packet, offset + 8),
        };
        Force = new()
        {
            X = BitConverter.ToSingle(packet, offset + 12),
            Y = BitConverter.ToSingle(packet, offset + 16),
            Z = BitConverter.ToSingle(packet, offset + 20),
        };
    }
}
