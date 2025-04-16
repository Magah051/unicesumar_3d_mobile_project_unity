using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoMobile : MonoBehaviour
{
    [SerializeField] private float velocidade = 4;
    private Vector2 myInput;
    private CharacterController characterController;
    private Transform myCamera;
    private Animator animator;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        myCamera = Camera.main.transform;
    }

    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    void Update()
    {
        RotacionarPersonagem();
        characterController.Move(transform.forward *
            myInput.magnitude * velocidade * Time.deltaTime);
        characterController.Move(Vector3.down * 9.81f * Time.deltaTime);

        animator.SetBool("Mover", myInput != Vector2.zero);
    }

    private void RotacionarPersonagem()
    {
        Vector3 forward = myCamera.TransformDirection
            (Vector3.forward);
        Vector3 right = myCamera.TransformDirection
            (Vector3.right);

        Vector3 targetDirection = myInput.x *
            right + myInput.y * forward;

        if (myInput != Vector2.zero && 
            targetDirection.magnitude > 0.1f)
        {
            Quaternion freeRotation = Quaternion.LookRotation(
                targetDirection.normalized);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(new Vector3
                (transform.eulerAngles.x, freeRotation.eulerAngles.y,
                transform.eulerAngles.z)), 10 * Time.deltaTime);
        }

    }
}
