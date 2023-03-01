
using UnityEngine;

namespace SpaceCharacterStateMachine
{
    public class CharacterStateDead : CharacterState
    {
        private Canvas deadUI;
        public CharacterStateDead(Character character) : base(character)
        {
            deadUI = character.DeadUI;
        }

        public override void Enter()
        {
            character.gameObject.SetActive(false);
            deadUI.enabled = true;
        }
        public override void Exit()
        {
            character.gameObject.SetActive(true);
            deadUI.enabled = false;
        }
    }
}