// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LookRotation))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float minIdleTime;

    [SerializeField] private float maxIdleTime;

    [SerializeField] private GameObject projectilePrefab;
    private GameObject _player;
    private Vector3 _aimDirection;
    private LookRotation _rotationHandler;


    void Awake()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        _rotationHandler = GetComponent<LookRotation>();
        _aimDirection = Vector3.back;
        StartCoroutine(EnemySequence());
    }

    void Update()
    {
        _rotationHandler.SetRotationTarget(_aimDirection);

        if (_player)
            _aimDirection = (_player.transform.position - this.transform.position).normalized;

    }

    private IEnumerator EnemySequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
            Fire();
        }
    }

    private void Fire()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = this.transform.position;
        projectile.GetComponent<ProjectileController>().SetDirection(_aimDirection);
    }
}
