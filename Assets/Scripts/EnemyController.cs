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

    private EnemyStates _state;
    private enum EnemyStates
    {
        Idle,
        Attack
    }

    void Awake()
    {
        _state = EnemyStates.Idle;
        _player = FindObjectOfType<PlayerController>().gameObject;
        _rotationHandler = GetComponent<LookRotation>();
        _aimDirection = Vector3.back;
        StartCoroutine(EnemySequence());
    }

    void Update()
    {
        _rotationHandler.SetRotationTarget(_aimDirection);

        if (_state == EnemyStates.Idle)
        {
            _aimDirection = Vector3.back;
        }
        else if (_state == EnemyStates.Attack && _player)
        {
            _aimDirection = (_player.transform.position - this.transform.position).normalized;
        }

    }

    private IEnumerator EnemySequence()
    {
        while (true)
        {
            yield return Idle();
            yield return Attack();
        }
    }

    private IEnumerator Idle()
    {
        _state = EnemyStates.Idle;
        yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
    }

    private IEnumerator Attack()
    {
        if (!_player)
            yield break;

        _state = EnemyStates.Attack;
        yield return new WaitForSeconds(0.5f);
        Fire();
        yield return new WaitForSeconds(0.5f);

    }
    private void Fire()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = this.transform.position;
        projectile.GetComponent<ProjectileController>().SetDirection(_aimDirection);
    }
}
