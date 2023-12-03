using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _maxLength;

    // [SerializeField] private ParticleSystem _muzzleParticles;
    // [SerializeField] private ParticleSystem _hitParticles;

    [SerializeField] private float _damage;

    // [SerializeField] private AudioSource laserSound; // 레이저 사운드를 재생하기 위한 Audio Source

    private bool isShooting = false; // 공격 중인지 여부를 나타내는 변수

    private void Awake()
    {
        _beam.enabled = false;
    }

    private void Activate()
    {
        _beam.enabled = true;

        // _muzzleParticles.Play();
        // _hitParticles.Play();

        // laserSound.Play();
    }

    private void Deactivate()
    {
        _beam.enabled = false;

        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);

        // _muzzleParticles.Stop();
        // _hitParticles.Stop();

        // laserSound.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!isShooting)
            {
                Activate();
                isShooting = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            if (isShooting)
            {
                Deactivate();
                isShooting = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            Ray ray = new Ray(_muzzlePoint.position, _muzzlePoint.forward);
            bool cast = Physics.Raycast(ray, out RaycastHit hit, _maxLength);
            Vector3 hitPosition = cast ? hit.point : _muzzlePoint.position + _muzzlePoint.forward * _maxLength;

            _beam.SetPosition(0, _muzzlePoint.position);
            _beam.SetPosition(1, hitPosition);

            // _hitParticles.transform.position = hitPosition;
        }
    }
}