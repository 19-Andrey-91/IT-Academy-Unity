using UnityEngine;

namespace SpaceCharacterStateMachine
{

    public class CharacterStateMove : CharacterState
    {
        private float moveSpeed;
        float directionMove;
        Vector2 vectorDirectionMove = Vector2.zero;
        public CharacterStateMove(Character character) : base(character)
        {
            rigidbody = character.GetComponent<Rigidbody2D>();
            moveSpeed = character.MoveSpeed;
            character.OnMoving += Moving;
        }

        public override void FixedUpdate()
        {
            vectorDirectionMove.x = directionMove * moveSpeed;
            rigidbody.AddForce(vectorDirectionMove);
        }

        private void Moving(float direction)
        {
            directionMove = direction;
        }
    }
}