using UnityEngine;

namespace SpaceCharacterStateMachine
{
    public abstract class CharacterState
    {
        protected Character character;
        protected Rigidbody2D rigidbody;

        public CharacterState(Character character)
        {
            this.character = character;
            rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }
    }
}