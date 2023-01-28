using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    private Rigidbody rb;
    private AudioSource thrustSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrusting();
        Rotation();
    }

    void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!thrustSound.isPlaying)
        {
            thrustSound.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
        {
            thrustSound.Stop();
            mainEngineParticles.Stop();
        }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
    
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
}
