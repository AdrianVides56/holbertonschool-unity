public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) {}

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);

        Ctx.Footsteps.pitch = 0.8f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * (Ctx.runMultiplier * 0.75f);
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * (Ctx.runMultiplier * 0.75f);

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
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
            SwitchState(Factory.Walk());
    }
}
