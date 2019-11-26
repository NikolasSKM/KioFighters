using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Animator anim;

    public float attackRange;
    private float nextAttack;
    private float nextAttack2;
    private float nextAttack3;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextAttack)
        {
            Attacking();
        }

        //if (Input.GetButtonDown("Fire2") && Time.time > nextAttack)
        //{
        //    Attacking2();
        //}

        //if (Input.GetKey(KeyCode.Q) && Time.time > nextAttack)
        //{
        //    Attacking3();
        //}
    }

    void Attacking()
    {
        anim.SetTrigger("Attack");
        nextAttack = Time.time + attackRange;
    }

    //void Attacking2()
    //{
    //    anim.SetTrigger("Attack2");
    //    nextAttack2 = Time.time + attackRange;
    //}

    //void Attacking3()
    //{
    //    anim.SetTrigger("Attack3");
    //    nextAttack3 = Time.time + attackRange;
    //}
}
