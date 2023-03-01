using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceCharacterStateMachine
{
    [RequireComponent(typeof(Character))]
    public class CharacterStateMachine : MonoBehaviour
    {
        private Dictionary<Type, CharacterState> stateMap;
        private CharacterState stateCurrent;
        private Character character;

        private void Start()
        {
            character = GetComponent<Character>();
            InitState(character);
            SetStateByDefault();
        }

        private void InitState(Character character)
        {
            stateMap = new Dictionary<Type, CharacterState>
            {
                [typeof(CharacterStateMove)] = new CharacterStateMove(character),
                [typeof(CharacterStateJump)] = new CharacterStateJump(character),
                [typeof(CharacterStateDead)] = new CharacterStateDead(character),
                [typeof(CharacterStateIdle)] = new CharacterStateIdle(character),
                [typeof(CharacterStateRespawn)] = new CharacterStateRespawn(character),
            };
        }

        private void SetState(CharacterState newState)
        {
            stateCurrent?.Exit();
            stateCurrent = newState;
            stateCurrent.Enter();
        }

        private void SetStateByDefault()
        {
            SetStateIdle();
        }

        private CharacterState GetState<T>() where T : CharacterState
        {
            var type = typeof(T);
            return stateMap[type];
        }

        private void Update()
        {
            stateCurrent?.Update();
        }

        private void FixedUpdate()
        {
            stateCurrent?.FixedUpdate();
        }

        public void SetStateDead()
        {
            var state = GetState<CharacterStateDead>();
            SetState(state);
        }

        public void SetStateMove()
        {
            var state = GetState<CharacterStateMove>();
            SetState(state);
        }

        public void SetStateJump()
        {
            var state = GetState<CharacterStateJump>();
            SetState(state);
        }

        public void SetStateIdle()
        {
            var state = GetState<CharacterStateIdle>();
            SetState(state);
        }

        public void SetStateRespawn()
        {
            var state = GetState<CharacterStateRespawn>();
            SetState(state);
        }
    }
}