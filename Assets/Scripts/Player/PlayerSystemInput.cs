using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSystemInput : MonoBehaviour
{
    PlayerController playerController;
    protected Vector2 movement;
    protected bool attackOne;
    private bool jump;

    public Vector2 Movement { get => movement; }
    public bool AttackOne { get => attackOne; }
    public bool JumpInput { get => jump; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    void OnAttackOne(InputValue value)
    {
        if (value != null) playerController.AttackOne(true);
        else playerController.AttackOne(false);
    }
    void OnAttackTwo(InputValue value)
    {
        if (value != null) playerController.AttackTwo(true);
        else playerController.AttackTwo(false);
    }
    void OnJump(InputValue value)
    {
        if (value != null) playerController.Jump();
    }
    void OnClimb(InputValue value)
    {
        if (value != null) playerController.Climb();
        Debug.Log("<color=yellow><b>" + "pr" + "</b></color>");
    }
}
