using Unity.Entities;
using Unity.NetCode;

// This struct represents a network message (RPC) sent from the server to clients
public struct LobbyLightRpc : IRpcCommand
{
    public bool TurnOn; // true = turn the light on, false = turn it off
}
