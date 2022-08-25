using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool MoveByTouch, gameState, attackToTheBoss;
    private Vector3 Direction;
    private Rigidbody PlrRb;
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    public Transform road;

    void Start()
    {
        PlrRb = transform.GetChild(0).GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveByTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }

        if (MoveByTouch)
        {

            Direction.x = Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);

            Direction = Vector3.ClampMagnitude(Direction, 1f);

            road.position = new Vector3(0f, 0f, Mathf.SmoothStep(road.position.z, -100f, Time.deltaTime * roadSpeed));

           /* foreach (var stickman_Anim in Rblst)
                stickman_Anim.GetComponent<Animator>().SetFloat("run", 1f);*/
        }
    }
}
