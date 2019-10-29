using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviour
{
    public float vel;
    public bool shouldControl;

    Transform tr;
    Rigidbody rb;
    PhotonView photonView;

    void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        bool isMasterClient = PhotonNetwork.IsMasterClient;

        if (isMasterClient)
            rb.velocity = new Vector3(1, -1f, 0) * vel;
    }

    void FixedUpdate()
    {
        bool isMasterClient = PhotonNetwork.IsMasterClient;
        float pos_x = tr.position.x;
        float pos_y = tr.position.y;

        bool _shouldControl = shouldControl;

        shouldControl =
            (pos_y > 0.5 && !isMasterClient) || (pos_y < -0.5 && isMasterClient);

        //if (_shouldControl != shouldControl)
            //photonView.TransferOwnership(photonView.Owner);

        if (shouldControl)
        {
            var vel = rb.velocity;

            if (Mathf.Abs(pos_x) > 4)
            {
                if (pos_x > 0.1f && vel.x > 0.1f || pos_x < -0.1f && vel.x < -0.1f)
                    vel.x *= -1;
            }
            if (Mathf.Abs(pos_y) > 4)
            {
                if (pos_y > 0.1f && vel.y > 0.1f || pos_y < -0.1f && vel.y < -0.1f)
                    vel.y *= -1;
            }

            rb.velocity = vel;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        bool isMasterClient = PhotonNetwork.IsMasterClient;
        float pos_y = tr.position.y;

        if (isMasterClient && col.tag == "Player")
        {
            var vel = rb.velocity;
            if (pos_y > 0.1f && vel.y > 0.1f || pos_y < -0.1f && vel.y < -0.1f)
            {
                vel.y *= -1;
                rb.velocity = vel;
            }
        }
    }
}
