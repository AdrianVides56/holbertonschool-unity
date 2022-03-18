public abstract class PlayerBaseState
{
    private bool isRootState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    protected PlayerBaseState currentSubState;
    protected PlayerBaseState currentSuperState;

    protected bool IsRootState { set { isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();


    public void UpdateStates()
    {
        UpdateState();
        if (currentSubState != null)
            currentSubState.UpdateStates();
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        // Current state exits state
        ExitState();

        // New state enters state
        newState.EnterState();

        if (isRootState)
        {
            // Switch current state of context
            _ctx.CurrentState = newState;
        }
        else if (currentSuperState != null)
        {
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
