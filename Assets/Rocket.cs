using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Rocket : MonoBehaviour {
    [SerializeField]float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive){
        //print("update");
        Rotate();
        Thrust();
        }

	}

    void OnCollisionEnter(Collision collision) 
    {
        if (state != State.Alive) { return; } //ignore collisions

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("safe");
                break;
            case "Finish":
                //print("finish");
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                print("DEAD");
                state = State.Dying;
                //SceneManager.LoadScene(0);
                Invoke("LoadFirstLevel", 1f);
                break;
        }
        

    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            //if (!audioSource.isPlaying)
            //{
            //    audioSource.Play();
            //}
            //else
            //{
            //    audioSource.Stop();
            //}
        }
    }

    void Rotate()
    {
        rigidBody.freezeRotation = true; // manual control of rotation.

        float rotationSpeed = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }


}
