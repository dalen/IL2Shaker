using System.Numerics;

namespace IL2TelemetryRelay.State;

internal static class StateDecoder
{
    internal static Event Decode(byte[] packet, out int offset)
    {
        // uint   packetID   = BitConverter.ToUInt32(packet, 0);
        ushort packetSize = BitConverter.ToUInt16(packet, 4);
        uint tick = BitConverter.ToUInt32(packet, 6);

        offset = 11;
        var state = new StateData(tick, packet, offset);
        state.Paused = packetSize == 12;
        byte length = packet[10];

        for (int i = 0; i < length; i++)
        {
            var stateType = (StateType)BitConverter.ToUInt16(packet, offset);
            byte stateLength = packet[offset + 2];
            offset += 3;

            switch (stateType)
            {
                case StateType.EngineRPM:
                    state.EngineCount = stateLength;
                    for (int j = 0; j < state.EngineCount; j++)
                    {
                        state.EngineRPM[j] = BitConverter.ToSingle(packet, offset + j * 4);
                    }

                    break;
                case StateType.IntakeManifoldPressurePa:
                    for (int j = 0; j < stateLength; j++)
                    {
                        state.IntakeManifoldPressurePa[j] = BitConverter.ToSingle(
                            packet,
                            offset + j * 4
                        );
                    }

                    break;
                case StateType.EngineShakeFrequency:
                    // Seems to range between 7-15 with the engine running, around 12-13 normally.
                    for (int j = 0; j < stateLength; j++)
                    {
                        state.EngineShakeFrequency[j] = BitConverter.ToSingle(
                            packet,
                            offset + j * 4
                        );
                    }

                    break;
                case StateType.EngineShakeAmplitude:
                    // Sits around 0.007 normally, doubles when pitching up, 0 with engine off.
                    for (int j = 0; j < stateLength; j++)
                    {
                        state.EngineShakeAmplitude[j] = BitConverter.ToSingle(
                            packet,
                            offset + j * 4
                        );
                    }
                    break;
                case StateType.LandingGearPosition:
                    // 0 is retracted, 1 is fully deployed
                    state.LandingGearCount = stateLength;
                    for (int j = 0; j < stateLength; j++)
                    {
                        state.LandingGearPosition[j] = BitConverter.ToSingle(
                            packet,
                            offset + j * 4
                        );
                    }

                    break;
                case StateType.LandingGearPressure:
                    // How much pressure is applied to wheels, seems to range from 0-1. Maybe breaks at 1?
                    state.LandingGearCount = stateLength;
                    for (int j = 0; j < stateLength; j++)
                    {
                        state.LandingGearPressure[j] = BitConverter.ToSingle(
                            packet,
                            offset + j * 4
                        );
                    }

                    break;
                case StateType.EquivalentAirSpeed:
                    state.EquivalentAirSpeed = BitConverter.ToSingle(packet, offset);
                    break;
                case StateType.AngleOfAttack:
                    // +- forces? ranges up to about 0.35. Seems to be somewhat related to G Forces
                    state.AngleOfAttack = BitConverter.ToSingle(packet, offset);
                    break;
                case StateType.Acceleration:
                    // Longitudinal, Vertical, Lateral
                    state.Acceleration = new(
                        BitConverter.ToSingle(packet, offset),
                        BitConverter.ToSingle(packet, offset + 4),
                        BitConverter.ToSingle(packet, offset + 8)
                    );
                    break;
                case StateType.StallBuffet:
                    state.StallBuffetFrequency = BitConverter.ToSingle(packet, offset);
                    state.StallBuffetAmplitude = BitConverter.ToSingle(packet, offset + 4);
                    break;
                case StateType.AboveGroundLevelMetres:
                    state.AboveGroundLevelMetres = BitConverter.ToSingle(packet, offset);
                    break;
                case StateType.FlapsPosition:
                    // 0 fully retracted, 1 fully extended
                    state.FlapsPosition = BitConverter.ToSingle(packet, offset);
                    break;
                case StateType.AirBrakePosition:
                    // 0 fully retracted, 1 fully extended
                    state.AirBrakePosition = BitConverter.ToSingle(packet, offset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(((uint)stateType).ToString());
            }

            // Here the state length refers to how many floats
            offset += 4 * stateLength;
        }

        return state;
    }
}
