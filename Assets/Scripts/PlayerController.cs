using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    #region VARIABLES

    [Header("ID SETTINGS")]
    [SyncVar] private int playerId = 0;
    private static int playerIdAvaliable = 0;

    [Header("MOVING SETTINGS")]
    public float vel = 5.0f;
    public float velRot = 150f;

    [Header("INSTATIATING SETTINGS")]
    public GameObject cubePrefab;
    public GameObject cubeRedPrefab;
    public GameObject cubeBluePrefab;

    #endregion VARIABLES

    private void Start()
    {
        LoadPlayerId();
        LoadMaterial();
    }

    #region LOADING PLAYER ID

    private void LoadPlayerId()
    {
        // only load playerId if is server
        if (isServer)
        {
            if (hasAuthority)
            {
                // reset playerIdAvaliableif current player has authority
                // that means we're on host player
                playerIdAvaliable = 0;
            }
            // change playerIdAvaliable for all players
            playerIdAvaliable++;
            // change current player's Id variable
            playerId = playerIdAvaliable;
        }
    }

    #endregion LOADING PLAYER ID

    #region LOADING PLAYER'S MATERIAL BASED IN THE ID

    private void LoadMaterial()
    {
        // get Mesh Renderer Component
        var meshrenderer = GetComponent<MeshRenderer>();
        // apply on mainMaterial a color based on playerId
        // if playerId is equal to, we're on host, and set color red
        // otherwhise, we set color blue, no matter playerId
        meshrenderer.material.color = playerId == 1 ? Color.red : Color.blue;
    }

    #endregion LOADING PLAYER'S MATERIAL BASED IN THE ID

    private void Update()
    {
        MovePlayer();
        if (hasAuthority && Input.GetKeyDown(KeyCode.Z))
        {
            CmdSpawnCube();
        }
    }

    #region MOVING THE PLAYER

    private void MovePlayer()
    {
        // only move player if we are on player that we have authority
        if (hasAuthority)
        {
            // get axis values and multiply by public speed values
            var h = Input.GetAxis("Horizontal") * velRot;
            var v = Input.GetAxis("Vertical") * vel;

            // move with translate
            transform.Translate(Vector3.forward * (v * Time.deltaTime));
            // spin with rotate
            transform.Rotate(Vector3.up * (h * Time.deltaTime));
        }
    }

    #endregion MOVING THE PLAYER

    #region MAKING THE PLAYER INSTANTIATE A CUBE

    [Command]
    private void CmdSpawnCube()
    {
        // instatiate 'cubePrefab' and hold it into 'cube' variable
        var cube = Instantiate(cubePrefab);
        // code Depreciated: var cube = Instantiate(playerId == 1 ? cubeRedPrefab : cubeBluePrefab);

        #region LOADING CUBE'S MATERIAL BASED IN THE ID

        // set cube's owner id
        cube.GetComponent<CubeMaterialController>().ownerId = playerId;

        #endregion LOADING CUBE'S MATERIAL BASED IN THE ID

        // change the position of created cube to be in front of player onwer
        // transform.position: represent player's position in the scene
        // transform.forward: represent player's "front"
        // transform.forward*2: change magnitude of player's forward by 2
        //      that means cube will be positioned 2 units away from the player
        // all this math will be applied to cube's position
        cube.transform.position = transform.position + transform.forward * 2;

        // change rotation of created cube, to match player's rotation
        cube.transform.rotation = transform.rotation;

        // since cube is only instantiated on server because of [Command] annotation,
        // we habe to spawn it on entire server, so all players can see this new cube.
        // this will only work ig cube is registered as a 'SpawnablePrefab' on NetworkManager
        NetworkServer.Spawn(cube);
    }

    #endregion MAKING THE PLAYER INSTANTIATE A CUBE
}