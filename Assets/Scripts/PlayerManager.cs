using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool MoveByTouch, gameState, attackToTheBoss;
    private Vector3 Direction;
    public List<Rigidbody> RbList = new List<Rigidbody>();
    private List<Animator> AnimatorList = new List<Animator>(); 
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    public Transform road;

    public static PlayerManager Instance;

    void Start()
    {
        Instance = this;
        RbList.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        AnimatorList.Add(transform.GetChild(0).GetComponent<Animator>());
        gameState = true;
    }

    bool isBossDead= false;
    // Update is called once per frame
    void Update()
    {
        if (gameState)
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
                //  Debug.Log(Input.GetAxis("Mouse X"));
                Direction.x = Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);

                Direction = Vector3.ClampMagnitude(Direction, 1f);



                road.position = new Vector3(0f, 0f, Mathf.SmoothStep(road.position.z, -100f, Time.deltaTime * roadSpeed));

                //PlrAnimator.SetFloat("speed", 1);

                for (int i = 0; i < AnimatorList.Count; i++)
                {
                    AnimatorList[i].SetFloat("speed", 1);
                }

                /* foreach (var stickman_Anim in Rblst)
                     stickman_Anim.GetComponent<Animator>().SetFloat("run", 1f);*/
            }
            else
            {
                //PlrAnimator.SetFloat("speed", 0);
                for (int i = 0; i < AnimatorList.Count; i++)
                {
                    AnimatorList[i].SetFloat("speed", 0);
                }
            }

            for (int i = 0; i < RbList.Count; i++)
            {

                if (RbList[i].velocity.magnitude > 0.5f)
                {
                    RbList[i].rotation = Quaternion.Slerp(RbList[i].rotation, Quaternion.LookRotation(RbList[i].velocity), Time.deltaTime * velocity);
                }
                else
                {
                    RbList[i].rotation = Quaternion.Slerp(RbList[i].rotation, Quaternion.identity, Time.deltaTime * velocity);
                }
            }
        }
        else
        {
            if (!BossManager.Instance.BossIsAlive && !isBossDead)
            {
                isBossDead = true;
                for (int i = 0; i < RbList.Count; i++)
                {
                    RbList[i].GetComponent<Animator>().SetFloat("attackMode", 4);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameState)
        {
            if (MoveByTouch)
            {
                // PlrRb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f);

                for (int i = 0; i < RbList.Count; i++)
                {
                    RbList[i].velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f);
                }

            }
            else
            {
                //PlrRb.velocity = Vector3.zero;

                for (int i = 0; i < RbList.Count; i++)
                {
                    RbList[i].velocity = Vector3.zero;
                }
            }
        }
    }

    public void addPlayer(GameObject objPlayer)
    {        
        objPlayer.transform.SetParent(transform);
        RbList.Add(objPlayer.GetComponent<Rigidbody>());
        AnimatorList.Add(objPlayer.GetComponent<Animator>());


    }
}
