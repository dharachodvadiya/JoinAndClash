using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitMennt : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Add"))
        {
            PlayerManager.Instance.addPlayer(collision.gameObject);

            if(!collision.gameObject.GetComponent<RecruitMennt>())
            {
                collision.gameObject.AddComponent<RecruitMennt>();
            }

            collision.transform.GetChild(0).GetComponent<Renderer>().material = transform.GetChild(0).GetComponent<Renderer>().material;
        }
    }
}
