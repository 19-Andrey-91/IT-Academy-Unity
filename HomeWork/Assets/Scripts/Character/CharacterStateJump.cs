using UnityEngine;

namespace SpaceCharacterStateMachine
{
    public class CharacterStateJump : CharacterState
    {
        private float jumpForce;

        public CharacterStateJump(Character character) : base(character)
        {
            jumpForce = character.ForceJump;
        }

        public override void Enter()
        {
            if (character.GroundCheck.IsGround)
            {
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}