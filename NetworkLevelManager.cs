using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using Unity.Netcode;
using UnityEngine;

public class NetworkLevelManager : LevelManager
{
    private NetworkPlayerSpawner _networkPlayerSpawner;

    protected override void Awake()
    {
        base.Awake();
        _networkPlayerSpawner = GetComponent<NetworkPlayerSpawner>();
    }

    protected override void InstantiatePlayableCharacters()
    {
        Players = new List<Character>();

        if (GameManager.Instance.PersistentCharacter != null)
        {
            Players.Add(GameManager.Instance.PersistentCharacter);
            return;
        }

        if (GameManager.Instance.StoredCharacter != null)
        {
            Character newPlayer = (Character)Instantiate(GameManager.Instance.StoredCharacter, new Vector3(0, 0, 0), Quaternion.identity);
            newPlayer.name = GameManager.Instance.StoredCharacter.name;
            Players.Add(newPlayer);
            return;
        }

        if ((SceneCharacters != null) && (SceneCharacters.Count > 0))
        {
            foreach (Character character in SceneCharacters)
            {
                Players.Add(character);
            }

            return;
        }

        if (PlayerPrefabs == null)
        {
            return;
        }

        // player instantiation
        if (PlayerPrefabs.Count() != 0)
        {
            foreach (Character playerPrefab in PlayerPrefabs)
            {
                FindObjectOfType<NetworkPlayerSpawner>().SpawnPlayer(Vector3.zero);
            }
        }
        else
        {
            //Debug.LogWarning ("LevelManager : The Level Manager doesn't have any Player prefab to spawn. You need to select a Player prefab from its inspector.");
            return;
        }
    }

    public void AddPlayerAfterSpawn(GameObject playerGO)
    {
        Character player = playerGO.GetComponent<Character>();
        Players.Add(player);
        Debug.Log("Player count: " + Players.Count);
    }

}
