using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MemberManager : MonoBehaviour
{
    public Animator character_animator;
    public GameObject Particle_Death;
    private Transform Boss;
    public int Health;
    public float MinDistanceOfEnemy, MaxDistanceOfEnemy, moveSpeed;
    public bool fight, member;
    private Rigidbody rb;
    private CapsuleCollider _capsuleCollider;

    void Start()
    {
        character_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        Boss = GameObject.FindWithTag("boss").transform;

        Health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        var bossDistance = Boss.position - transform.position;
        if (!fight)
        {
            if (bossDistance.sqrMagnitude <= MaxDistanceOfEnemy * MaxDistanceOfEnemy)
            {
                PlayerManager.Instance.attackToTheBoss = true;
                PlayerManager.Instance.gameState = false;
            }

            if (PlayerManager.Instance.attackToTheBoss && member)
            {
                transform.position = Vector3.MoveTowards(transform.position, Boss.position, moveSpeed * Time.deltaTime);

                var stickManRotation = new Vector3(Boss.position.x, transform.position.y, Boss.position.z) - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion.LookRotation(stickManRotation, Vector3.up), 10f * Time.deltaTime);

                character_animator.SetFloat("speed", 1f);

                rb.velocity = Vector3.zero;
            }
        }
        if (bossDistance.sqrMagnitude <= MinDistanceOfEnemy * MinDistanceOfEnemy)
        {
            fight = true;

            var stickManRotation = new Vector3(Boss.position.x, transform.position.y, Boss.position.z) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion.LookRotation(stickManRotation, Vector3.up), 10f * Time.deltaTime);

            character_animator.SetBool("fight", true);

            //MinDistanceOfEnemy = MaxDistanceOfEnemy;

            rb.velocity = Vector3.zero;
        }

        else
        {
            fight = false;
        }

    }

    public void ChangeTheAttackMode()
    {
        character_animator.SetFloat("attackMode", UnityEngine.Random.Range(0, 3));

    }
}
