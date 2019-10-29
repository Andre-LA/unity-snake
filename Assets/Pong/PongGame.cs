using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PongGame : MonoBehaviour
{
    void Start()
    {
        bool isMasterClient = PhotonNetwork.IsMasterClient;

        Vector3 pos = new Vector3(
            0f,
            isMasterClient ? -4f : 4f,
            0f
        );

        PhotonNetwork.Instantiate("Paddle", pos, Quaternion.identity);

        if (isMasterClient)
        {
            PhotonNetwork.InstantiateSceneObject(
                "Ball", Vector3.up*2, Quaternion.identity
            );
        }
    }
}
