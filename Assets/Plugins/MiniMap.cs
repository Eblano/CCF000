using UnityEngine;
using System.Collections.Generic;

public class MiniMap : MonoBehaviour
{
    public Transform PlayerTr;
    public float Distanse = 100;
    public bool RotateCam = false;

    void FixedUpdate()
    {
        transform.position = new Vector3(PlayerTr.position.x, PlayerTr.position.y + Distanse, PlayerTr.position.z);
        if(RotateCam)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, PlayerTr.eulerAngles.y, transform.eulerAngles.z);
    }
}