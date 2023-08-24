// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using UnityEditor.MPE;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float minIdleTime;

    [SerializeField] private float maxIdleTime;

    [SerializeField] private GameObject projectilePrefab;
    private GameObject _player;


    void Awake()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        StartCoroutine(EnemySequence());
    }

    private IEnumerator EnemySequence()
    {
        while (_player)
        {
            yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
            Fire();
        }
    }

    private void Fire()
    {
        if (!_player)
            return;

        Vector3 shootingDirection = (_player.transform.position - this.transform.position);
        shootingDirection.Normalize();

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = this.transform.position;
        projectile.GetComponent<ProjectileController>().SetDirection(shootingDirection);
    }
}
