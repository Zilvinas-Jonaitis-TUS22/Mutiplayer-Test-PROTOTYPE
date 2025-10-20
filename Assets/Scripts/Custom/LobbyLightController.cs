using UnityEngine;
using Unity.Entities;

public class LobbyLightMono : MonoBehaviour
{
    [SerializeField] private Light lobbyLight; // Drag your Light in the Inspector

    private EntityManager _entityManager;

    void Start()
    {
        if (lobbyLight == null)
        {
            Debug.LogError("LobbyLight not assigned!");
            enabled = false;
            return;
        }

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        lobbyLight.enabled = false; // start off
    }

    void Update()
    {
        if (_entityManager == null) return;

        // Query entities with GameplayMaps buffer
        EntityQuery query = _entityManager.CreateEntityQuery(
            ComponentType.ReadOnly<Unity.Template.CompetitiveActionMultiplayer.GameplayMaps>()
        );

        if (query.IsEmptyIgnoreFilter) return;

        var entities = query.ToEntityArray(Unity.Collections.Allocator.Temp);
        var gameplayMapBuffer = _entityManager.GetBuffer<Unity.Template.CompetitiveActionMultiplayer.GameplayMaps>(entities[0]);

        int connectedPlayers = 0;
        foreach (var map in gameplayMapBuffer)
        {
            if (map.ConnectionEntity != Entity.Null)
                connectedPlayers++;
        }

        // ✅ Turn light ON only if there are exactly 2 players
        if (connectedPlayers == 2)
        {
            if (!lobbyLight.enabled)
            {
                lobbyLight.enabled = true;
                Debug.Log("💡 Lobby light ON - 2 players connected!");
            }
        }
        else
        {
            if (lobbyLight.enabled)
            {
                lobbyLight.enabled = false;
                Debug.Log("💡 Lobby light OFF - not exactly 2 players!");
            }
        }

        entities.Dispose();
    }
}
