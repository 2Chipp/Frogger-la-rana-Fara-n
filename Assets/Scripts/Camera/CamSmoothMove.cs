using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CamSmoothMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 distance;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(player.position.x,0,player.position.z) + distance;
        transform.DOMove(target, 0.3f);
    }
}
