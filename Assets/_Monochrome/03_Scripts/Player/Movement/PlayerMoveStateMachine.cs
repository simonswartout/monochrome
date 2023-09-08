using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveStateMachine : MonoBehaviour
{
    //create a plater movement state machine
    public enum PlayerMovementStates
    {
        Idle, 
        Walking, 
        AirWalk,
        Running
    }
    [SerializeField] private PlayerMovementStates _playerMovementState;
    public PlayerMovementStates playerMovementState
    { 
        get { return _playerMovementState; }
        set 
        { 
            if(_playerMovementState != value)
            {
                _playerMovementState = value;
                Debug.Log("Player movement state changed to: " + _playerMovementState);
            }
        }
    }


    public enum PlayerJumpStates
    {
        Grounded, 
        Jumping, 
        Falling
    }
    [SerializeField] private PlayerJumpStates _playerJumpState;
    public PlayerJumpStates playerJumpState
    { 
        get { return _playerJumpState; }
        set 
        { 
            if(_playerJumpState != value)
            {
                _playerJumpState = value;
                Debug.Log("Player jump state changed to: " + _playerJumpState);
            }
        }
    }


    public enum PlayerDirectionStates
    {
        Left, 
        Right
    }
    [SerializeField] private PlayerDirectionStates _playerDirectionState;
    public PlayerDirectionStates playerDirectionState
    { 
        get { return _playerDirectionState; }
        set 
        { 
            if(_playerDirectionState != value)
            {
                _playerDirectionState = value;
                Debug.Log("Player direction state changed to: " + _playerDirectionState);
            }
        }
    }
}
