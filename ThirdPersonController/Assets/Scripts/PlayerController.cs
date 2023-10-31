using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;

    private float vInput;
    private float hInput;

    private float jumpSpeed = 5f;

    public float distanceToGround = 0.1f;
    private Rigidbody myBody;

    public LayerMask groundLayer;

    private CapsuleCollider col;

    public GameObject bullet;
    public float bulletSpeed = 100f;


    public GameBehaviour gameBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gameBehaviour = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            myBody.AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
        }
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        myBody.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        myBody.MoveRotation(myBody.rotation * angleRot);

        // this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        // this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * bulletSpeed;
        }
    }

    private bool isGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer,QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            gameBehaviour.HP -= 1;
        }
    }
}
