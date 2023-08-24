using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{
    private Vector3 _faceDirection;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(_faceDirection), 360 * Time.deltaTime);
    }

    public void SetRotationTarget(Vector3 target)
    {
        _faceDirection = target;
    }
}
