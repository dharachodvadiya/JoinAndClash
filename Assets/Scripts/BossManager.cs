using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public Animator BossAnimator;
    public static BossManager Instance;
    private float attackMode;
    public bool LockOnTarget, BossIsAlive;
    private Transform target;
    public Slider HealthBar;
    public TextMeshProUGUI Health_bar_amount;
    public int Health;
    public GameObject Particle_Death;
    public float maxDistance, minDistance;

    void Start()
    {
        Instance = this;

        var enemy = GameObject.FindGameObjectsWithTag("Add");

        foreach (var stickMan in enemy)
            Enemies.Add(stickMan);

        BossAnimator = GetComponent<Animator>();

        BossIsAlive = true;

        HealthBar.value = HealthBar.maxValue = Health = 200;

        Health_bar_amount.text = Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && BossIsAlive)
        {
            gameObject.SetActive(false);
            BossIsAlive = false;
           // Instantiate(Particle_Death, transform.position, Quaternion.identity);
        }
    }

    public void ChangeTheBossAttackMode()
    {
        BossAnimator.SetFloat("attackmode", Random.Range(2, 4));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("hit"))
        {
            Health--;
            Health_bar_amount.text = Health.ToString();
            HealthBar.value = Health;
        }

    }
}
