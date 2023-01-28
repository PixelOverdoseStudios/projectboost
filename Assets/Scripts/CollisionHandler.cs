using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    private float delayTimer = 2f;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessSequence();
                break;

             default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTimer);
        successParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound, 0.25f);
    }

    void StartCrashSequence()
    {

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTimer);
        crashParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound, 0.25f);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //This will toggle collision
        }
    }
}
