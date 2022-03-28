public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);

        Ctx.Footsteps.pitch = 0.6f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;

        if (Ctx.Footsteps.isPlaying && Ctx.IsJumping)
            Ctx.Footsteps.Pause();
        else if (!Ctx.Footsteps.isPlaying && !Ctx.IsJumping)
            Ctx.Footsteps.Play();
    }

    public override void ExitState()
    {
        Ctx.Footsteps.Stop();
    }

    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
            SwitchState(Factory.Idle());
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
            SwitchState(Factory.Run());
    }
}
