using System;
using Unity.Netcode;
using UnityEngine;

public class LobbyLightManager : NetworkBehaviour
{
    [SerializeField] private Light lobbyLight;

    private NetworkVariable<int> playerCount = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Awake()
    {
        if (lobbyLight != null)
            lobbyLight.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Update when players join or leave
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
        }

        /
