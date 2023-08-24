using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlot : MonoBehaviour
{
    [SerializeField] private float MinSpringForce;
    [SerializeField] private float MaxSpringForce;

    private SpringJoint _joint;
    // Start is called before the first frame update
    void Awake()
    {
        _joint = GetComponent<SpringJoint>();
    }

    public void SetupSlot(Vector3 offset, Rigidbody secondBody)
    {
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedBody = secondBody;
        _joint.connectedAnchor = Vector3.zero;
        _joint.spring = Random.Range(MinSpringForce, MaxSpringForce);

        this.transform.localPosition = offset;
    }
}
