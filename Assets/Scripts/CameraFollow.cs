using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private PlayerController m_player;
    [SerializeField]
    private Vector2 cameraBoundingBox, shiftBoundingBoxOrigion;
    private Vector2 playerMaxBounds, playerMinBounds, playerPosCur, playerPosPre;

    private void Start()
    {
        playerPosCur = m_player.transform.position;
        playerPosPre = m_player.transform.position;
        playerMaxBounds = new Vector2(playerPosCur.x + cameraBoundingBox.x / 2, playerPosCur.y + cameraBoundingBox.y / 2 + 1);
        playerMinBounds = new Vector2(playerPosCur.x - cameraBoundingBox.x / 2, playerPosCur.y - cameraBoundingBox.y / 2 + 1);
    }


    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        playerPosCur = m_player.transform.position;

        if(playerPosCur.x > playerMaxBounds.x || playerPosCur.x < playerMinBounds.x)
        {
            transform.position = new Vector3(transform.position.x + playerPosCur.x - playerPosPre.x, transform.position.y, transform.position.z);
            playerMaxBounds.x += playerPosCur.x - playerPosPre.x;
            playerMinBounds.x += playerPosCur.x - playerPosPre.x;
        }
        if(playerPosCur.y > playerMaxBounds.y || playerPosCur.y < playerMinBounds.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + playerPosCur.y - playerPosPre.y, transform.position.z);
            playerMaxBounds.y += playerPosCur.y - playerPosPre.y;
            playerMinBounds.y += playerPosCur.y - playerPosPre.y;
        }

        playerPosPre = m_player.transform.position;
    }

    //unity function to draw simple shapes in editor for design/visual refrence.
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(new Vector3(m_player.transform.position.x + shiftBoundingBoxOrigion.x ,
            m_player.transform.position.y + shiftBoundingBoxOrigion.y,
            m_player.transform.position.z),
            cameraBoundingBox);
    }
}
