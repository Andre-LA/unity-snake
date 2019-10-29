using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Paddle : MonoBehaviour
{
    public float vel;

    Transform tr;
    Rigidbody rb;
    PhotonView photonView;

    void Awake ()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    void FixedUpdate ()
    {
        if (!photonView.IsMine)
            return;

        float pos_x = tr.position.x;
        float pos_y = tr.position.y;


        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) && pos_x > -4)
            dir.x = -1;
        else if (Input.GetKey(KeyCode.RightArrow) && pos_x < 4)
            dir.x = 1;

        if (Input.GetKey(KeyCode.UpArrow) && pos_y < 4)
            dir.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow) && pos_y > -4)
            dir.y = -1;

        rb.velocity = dir * vel;
    }
}
