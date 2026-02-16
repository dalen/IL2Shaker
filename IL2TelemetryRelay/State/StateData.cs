using System.Numerics;

namespace IL2TelemetryRelay.State;

public record StateData : Event
{
    public int EngineCount;
    public Vector4 EngineRPM;
    public Vector4 IntakeManifoldPressurePa;

    public int LandingGearCount;
    public Vector4 LandingGearPosition;
    public Vector4 LandingGearPressure;

    public float EquivalentAirSpeed;

    // Longitudinal, Vertical, Lateral
    public Vector3 Acceleration;

    public float StallBuffetFrequency;
    public float StallBuffetAmplitude;

    public float AboveGroundLevelMetres;

    public float FlapsPosition;
    public float AirBrakePosition;

    /// <summary>
    /// Frequency in Hz
    /// </summary>
    public Vector4 EngineShakeFrequency;
    public Vector4 EngineShakeAmplitude;

    /// <summary>
    /// In radians
    /// </summary>
    public float AngleOfAttack;

    public bool Paused;

    public StateData(uint tick, byte[] packet, int offset)
        : base(tick, packet, offset) { }
}
