# Corgi To NGO

This is a collection of code changes and files added to allow the conversion of the corgi engine to allow multiplayer using Unity Netcode Game Objects.  I'll progressively update the pieces and parts that I'm making changes in here both to get feedback and to show progress.  Hopefully this will be fruitful!

## Items Added To Network Prefabs

- Player character prefab
- NetworkPlayerSpawner prefab

## NetworkPlayerSpawner (Prefab)

- NetworkObject
- NetworkPlayerSpawner

## Character Object (Prefab)

- Standard Corgi engine player character
- Added NetworkObject
- Added ClientNetworkTransform (for Client Auth Movement, could be done as server)
- Added NetworkRigidbody2D

## Scene Items

- Change LevelManager out for NetworkLevelManager
- Add NetworkPlayerSpawner prefab
- 