// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using TMPro;
using UnityEngine;

[RequireComponent(typeof(LookRotation))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; // Default speed sensitivity
    [SerializeField] private GameObject projectilePrefab;

    private Vector3 _aimDirection;
    private Plane _gamePlane;
    private LookRotation _rotationHandler;

    private void Awake()
    {
        _gamePlane = new Plane(Vector3.up, Vector3.zero);
        _rotationHandler = GetComponent<LookRotation>();
        _aimDirection = Vector3.forward;
        _rotationHandler.SetRotationTarget(_aimDirection);
    }

    // Update is called once per frame
    private void Update()
    {
        Aim();

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * (this.speed * Time.deltaTime), Space.World);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * (this.speed * Time.deltaTime), Space.World);

        // Use the "down" variant to avoid spamming projectiles. Will only get
        // triggered on the frame where the key is initially pressed.
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Aim()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_gamePlane.Raycast(mouseRay, out float hitloc))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitloc);
            _aimDirection = (hitPoint - this.transform.position).normalized;
        }
        _rotationHandler.SetRotationTarget(_aimDirection);
    }

    private void Fire()
    {
        var projectile = Instantiate(this.projectilePrefab);
        projectile.GetComponent<ProjectileController>().SetDirection(_aimDirection);
        projectile.transform.position = gameObject.transform.position;
    }
}
