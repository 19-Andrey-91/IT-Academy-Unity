using SpaceCharacterStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateRespawn : CharacterState
{
    public CharacterStateRespawn(Character character) : base(character)
    {
    }

    public override void Enter()
    {
        character.gameObject.SetActive(true);
        character.transform.position = character.SpawnPosition;
    }
}
