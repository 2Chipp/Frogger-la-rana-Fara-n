using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CamSmoothMove : MonoBehaviour
{
    Transform player;
    Vector3 distance;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        distance = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(player.position.x,0,player.position.z) + distance;
        transform.DOMove(target, 0.3f);
    }
}
