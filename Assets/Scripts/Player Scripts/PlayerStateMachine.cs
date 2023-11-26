using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //All the States Player Can be in
    public enum PlayerState
    {
        Idle,
        Dash,
        Move,
        Jump,
        WallSliding,
        Instrucion
    }

    public static PlayerStateMachine instance;
    // creat and Set Instacne so that it Can be availabe from any Script 
    private void Awake() {
        instance =this;
    } 

    [SerializeField]private PlayerState currentState = PlayerState.Move;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        //  Condions for Changing From One State to Another
        if(currentState != PlayerState.Idle && !playerController.CanMove &&!playerController.IsDashing&&currentState != PlayerState.Instrucion)
        {  
            ChangeState(PlayerState.Idle);
        }else
        if(playerController.CanMove && currentState != PlayerState.Move && playerController.IsGrounded&&!playerController.IsDashing&&currentState != PlayerState.Instrucion)
        {
            ChangeState(PlayerState.Move);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.Jump&&currentState != PlayerState.Instrucion)
        {
            ChangeState(PlayerState.Jump);
        }
        if(playerController.IsDashing && currentState != PlayerState.Dash&&currentState != PlayerState.Instrucion)
        {  
            ChangeState(PlayerState.Dash);
        }
        if(playerController.IsWallSliding && currentState != PlayerState.WallSliding &&!playerController.IsDashing && !playerController.CanMove)
        {  
            ChangeState(PlayerState.WallSliding);
        }
        // Update the current state
        UpdateState();
    }

    //Called every Frame once State is Set
    void UpdateState()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Dash:
                ActionManager.Dash.Invoke();
                break;

            case PlayerState.Move:
                ActionManager.Move.Invoke();
                break;

            case PlayerState.Jump:
                break;

            case PlayerState.WallSliding:
                ActionManager.WallSliding.Invoke();
                break;

        }
    }

    //Change the State and perfoms Exit action for previous State and Entery Action for current State 
    public void ChangeState(PlayerState newState)
    {
        // Exit logic for the current state
        switch (currentState)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Dash:
                break;

            case PlayerState.Move:
                break;

            case PlayerState.Jump:
                break;

            case PlayerState.WallSliding:
                break;

        }

        // Enter logic for the new state
        switch (newState)
        {
            case PlayerState.Idle:
                
                break;

            case PlayerState.Dash:
                ActionManager.DashStart.Invoke();
                break;

            case PlayerState.Move:
                break;

            case PlayerState.Jump:
                ActionManager.Jump.Invoke();
                break;

            case PlayerState.WallSliding:
                break;

        }
        //change States;
        currentState = newState;
    }
}
