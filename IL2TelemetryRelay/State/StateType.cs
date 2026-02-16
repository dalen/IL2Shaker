namespace IL2TelemetryRelay.State;

internal enum StateType : ushort
{
    EngineRPM = 0,
    IntakeManifoldPressurePa = 1,
    EngineShakeFrequency = 2,
    EngineShakeAmplitude = 3,
    LandingGearPosition = 4,
    LandingGearPressure = 5,
    EquivalentAirSpeed = 6,
    AngleOfAttack = 7,
    Acceleration = 8,
    StallBuffet = 9,
    AboveGroundLevelMetres = 10,
    FlapsPosition = 11,
    AirBrakePosition = 12,
}
