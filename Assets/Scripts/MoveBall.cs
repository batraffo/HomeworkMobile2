using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;

    [SerializeField]
    private float speed = 10;

    private Rigidbody rb;

    private Vector3 movement = new Vector3();


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement = Vector3.zero;
        if(virtualJoystick.Movement != default(Vector3))
        {
            movement = virtualJoystick.Movement;
            //print(movement);
        }
        else if (Input.touches.Length != 0)
        {
            Vector3 touchPositionInWord = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            movement = touchPositionInWord - transform.position;
            movement.y = 0f;
            movement = movement.normalized;
        }
        else
        {
            movement.x = Input.acceleration.x;
            movement.z = Input.acceleration.y;
            if(movement.sqrMagnitude > 1)
                movement = movement.normalized;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * speed);
    }
}
