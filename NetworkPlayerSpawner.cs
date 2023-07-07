using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private List<ulong> spawnedPlayerIds = new List<ulong>();

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnServerStopped += OnServerStopped;
    }

    private void OnServerStarted()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnServerStopped(bool serverIsHost)
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Updating player list for newly connected player");

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send =
            {
                TargetClientIds = new[] { clientId }
            }
        };

        foreach (ulong networkObjectId in spawnedPlayerIds)
        {
            SendPlayerToClientClientRpc(networkObjectId, clientRpcParams);
        }
    }

    public void SpawnPlayer(Vector3 spawnPosition)
    {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, spawnPosition);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong clientId, Vector3 spawnPosition)
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        newPlayer.name = playerPrefab.name;

        var networkPlayer = newPlayer.GetComponent<NetworkObject>();
        if (networkPlayer != null)
        {
            Debug.Log("ClientId: " + clientId + "; NetworkObjectId: " + networkPlayer.NetworkObjectId);
            networkPlayer.SpawnAsPlayerObject(clientId);

            spawnedPlayerIds.Add(networkPlayer.NetworkObjectId);
        }
        else
        {
            Debug.LogWarning("Level Manager:  The Character you've spawned is not a network object and thus will not work in the network.");
        }

        SendPlayerToAllClientsClientRpc(networkPlayer.NetworkObjectId);
    }

    [ClientRpc]
    private void SendPlayerToAllClientsClientRpc(ulong networkObjectId)
    {
        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];

        GameObject player = networkObject.gameObject;

        FindObjectOfType<NetworkLevelManager>().AddPlayerAfterSpawn(player);
    }

    [ClientRpc]
    private void SendPlayerToClientClientRpc(ulong networkObjectId, ClientRpcParams clientRpcParams = default)
    {
        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];

        GameObject player = networkObject.gameObject;

        FindObjectOfType<NetworkLevelManager>().AddPlayerAfterSpawn(player);
    }
}
