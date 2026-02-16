namespace IL2TelemetryRelay.Events;

internal enum EventType : ushort
{
    VehicleName = 0,
    EngineData = 1,
    GunData = 2,
    WheelData = 3,
    BombRelease = 4,
    RocketLaunch = 5,
    Hit = 6,
    Damage = 7,
    Explosion = 8,
    GunFired = 9,
    ServerAddress = 10,
    ServerTitle = 11,
    SimpleRadioStandaloneServerAddress = 12,
    ClientData = 13,
    ControlledObjectData = 14,
}
