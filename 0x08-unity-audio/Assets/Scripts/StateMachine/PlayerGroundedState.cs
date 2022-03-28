using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
        {
            IsRootState = true;
            InitializeSubState();
        }

    public override void EnterState()
    {
            Ctx.CurrentMovementY = Ctx.GroundedGravity;
            Ctx.AppliedMovementY = Ctx.GroundedGravity;
    }

    public override void UpdateState()
    {
        if (Ctx.VelocityY < 0)
            Ctx.VelocityY = Ctx.GroundedGravity;
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.InitialJumpPosition = Ctx.CharacterController.transform.position.y;
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            SetSubState(Factory.Idle());
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            SetSubState(Factory.Walk());
        else
            SetSubState(Factory.Run());
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsJumpPressed && Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Jump());
        }
    }
    
}
