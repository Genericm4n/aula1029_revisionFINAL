using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplitScrenController : MonoBehaviour
{
    #region VARIABLES

    public enum Players
    {
        PLAYER_1,
        PLAYER_2
    }

    [Header("PLAYER SETTINGS")]
    public Players players;

    [Header("MOVE SETTINGS")]
    public float vel = 5f;
    public float velRot = 150f;

    #endregion VARIABLES

    private void Update()
    {
        #region MOVING THE PLAYERS

        if (players == Players.PLAYER_1)
        {
            // get axis values and multiply by public speed values
            var h = Input.GetAxis("Horizontal1") * velRot;
            var v = Input.GetAxis("Vertical1") * vel;

            // move with translate
            transform.Translate(Vector3.forward * (v * Time.deltaTime));
            // spin with rotate
            transform.Rotate(Vector3.up * (h * Time.deltaTime));
        }
        else if (players == Players.PLAYER_2)
        {
            // get axis values and multiply by public speed values
            var h = Input.GetAxis("Horizontal2") * velRot;
            var v = Input.GetAxis("Vertical2") * vel;

            // move with translate
            transform.Translate(Vector3.forward * (v * Time.deltaTime));
            // spin with rotate
            transform.Rotate(Vector3.up * (h * Time.deltaTime));
        }

        #endregion MOVING THE PLAYERS
    }
}