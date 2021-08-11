using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.05f;

    //
    private bool isRunning = false;

    //animation
    private Animator anim;

    //movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private int desiredLane = 1; // 0 left, 1 middle, 2 right

    //speed modifier
    private float originalSpeed = 7.0f;
    private float speed;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f; // every 2.5 seconds, speed will grow by 0.1
    private float speedIncreaseAmount = 0.1f;

    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            //change the modifier text
            GameManager.Instance.UpdateModifier(speed - originalSpeed);

        }
        //gather input on which lane we should be
        if (MobileInput.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight)
            MoveLane(true);

        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        //calculate move delta (vector)
        Vector3 moveVector = Vector3.zero;
        //where we're going - where we are
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        //calculate Y
        if (isGrounded) //if grounded
        {
            verticalVelocity = -0.1f;

            if (MobileInput.Instance.SwipeUp)
            {
                //jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }

            else if (MobileInput.Instance.SwipeDown)
            {
                //slide
                StartSliding();
                Invoke("StopSliding", 1.0f); // Stop sliding after 1 second
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

            //fast falling mechanic
            if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //move the penguin
        controller.Move(moveVector * Time.deltaTime);

        //rotate penguin in direction he's heading
        Vector3 dir = controller.velocity; //direction
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

        ////longer way to code above lines
        ////left
        //if (!goingRight)
        //{
        //    desiredLane--;
        //    if (desiredLane == 1)
        //        desiredLane = 0;
        //}
        ////right
        //else
        //{
        //    desiredLane++;
        //    if (desiredLane == 3)
        //        desiredLane = 2;
        //}
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }
}
