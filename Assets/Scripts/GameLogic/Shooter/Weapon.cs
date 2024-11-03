using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    ParticleSystem _particleSystem;
    [SerializeField]
    public AudioSource _audioSource;
    [SerializeField]
    AudioClip ShootSound;
    //public GameObject Barrel;
    [SerializeField]
    public float rotationSpeed = 1f;
    [SerializeField]
    float Radius;
    [SerializeField]
    bool rotateToMouseCursor;
    [SerializeField]
    GameObject bulletpf;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    Transform BulletParent;
    [SerializeField]
    float BulletSpeed;
    [SerializeField]
    HealthComponent health;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        RotateToMouseCursor();
        PlayerRotate();
    }

    private void PlayerRotate()
    {
        Vector2 direction = (PointerInput.GetPointerInputVector2() - (Vector2)transform.position).normalized;
        Vector2 position = direction * Radius;
        transform.localPosition = position;
    }

    private void RotateToMouseCursor()
    {
        if (rotateToMouseCursor)
        {
            Vector2 direction = PointerInput.GetPointerInputVector2() - (Vector2)transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void Shoot(PlayerForm playerForm)
    {
        if(playerForm == PlayerForm.Fire)
        {
            Shoot();
        }
        else
        {
            Debug.Log("U cant shoot in this form");
        }
    }
    public void Shoot()
    {
        if(health.GetForm() == PlayerForm.Fire)
        {
            if (Time.timeScale != 0)
            {
                if(_particleSystem!=null)
                    _particleSystem?.Play();
                if (_audioSource != null)
                    _audioSource?.PlayOneShot(ShootSound);
                GameObject bulletGO = Instantiate(bulletpf, spawnPoint.position, transform.localRotation, BulletParent);
                Bullet bullet =  bulletGO.GetComponent<Bullet>();
                Vector3 direction = (Vector3.MoveTowards(spawnPoint.position, PointerInput.GetPointerInput(), 1f) - transform.position).normalized;
                bullet.Init(direction, BulletSpeed);
                bullet.SetForm(health.GetForm());
                Debug.Log("Spawn Direction: " + bullet.GetDirection().ToString());
            }
        }

    }
}
