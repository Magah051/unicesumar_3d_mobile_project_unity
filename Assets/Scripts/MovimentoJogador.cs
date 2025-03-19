using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);

        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;

        controller.Move(movimento * Time.deltaTime * 5f);
        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);

        if(movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(movimento),
                Time.deltaTime * 10);
        }

        animator.SetBool("Mover", movimento != Vector3.zero);
    }
}
