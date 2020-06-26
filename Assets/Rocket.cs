using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 60f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    int currentHealth = 100;
    int damage = 10;

    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    State state = State.Alive;

    // Use this for initialization
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        float upThrust = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * upThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Do Nothing
                break;
            case "Finish":
                SceneManager.LoadScene(1);
                break;
            default:
                TakeDamage();
                print(currentHealth);
                Death();
                break;
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - damage;
    }

    private void GainHealth()
    {
        currentHealth = currentHealth + 5;
    }

    private void Death()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene(0);
            currentHealth = 100;
        }
        else
        {
            //Do nothing
        }
    }

}
