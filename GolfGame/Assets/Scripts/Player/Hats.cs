using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hats : MonoBehaviour
{
    Transform ball;

    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        ball = transform.parent.GetChild(0).transform;
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.position;
    }
}
