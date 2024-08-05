using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject groundParticle;
    public bool live = true;
    [SerializeField] private float speed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float jumpForce;
    public int currentLine;
    [SerializeField] private float[] lines = new float[3];
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip loseClip;
    private bool isJumped = false;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject losePanel;
    private GameController gameController;
    private AudioSource audioSource;
    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        if (live)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, speed);

            Vector3 targetXPosition = new Vector3(lines[currentLine], transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetXPosition, sideSpeed);
        }
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }



        currentLine = Mathf.Clamp(currentLine, 0, 2);
    }


    private void LateUpdate()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }

    public void Left()
    {
        currentLine--;


    }

    public void Right()
    {
        currentLine++;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isJumped = true;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumped == true)
        {
            audioSource.Play();
            GameObject newParticle = Instantiate(groundParticle, transform.position, Quaternion.identity, null);
            Destroy(newParticle, 3);

            isJumped = false;
        }


        if (collision.gameObject.CompareTag("Obstacle"))
        {
            float yDistance = transform.position.y - collision.transform.position.y;

            if (yDistance < 0.8f && live)
            {
                live = false;
                animator.gameObject.SetActive(false);
                Instantiate(ragdoll, transform.position, Quaternion.identity, null);

                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                audioSource.PlayOneShot(loseClip);

                gameController.Save();

                StartCoroutine("ActivateLosePanel");
            }

        }
    }

    IEnumerator ActivateLosePanel()
    {
        yield return new WaitForSeconds(0.5f);
        losePanel.SetActive(true);
    }


}

