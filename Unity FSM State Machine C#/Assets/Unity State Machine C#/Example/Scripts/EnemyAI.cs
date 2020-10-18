using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AxlPlay
{
    public class EnemyAI : MonoBehaviour
    {
        public enum States
        {
            Wander,
            Pursue,
        }

        public StateMachine<States> fsm;

        private Wander _wander;
        private Pursue _pursue;
        private FieldOfView _fieldOfView;

        void Awake()
        {
            _wander = GetComponent<Wander>();
            _pursue = GetComponent<Pursue>();
            _fieldOfView = GetComponent<FieldOfView>();

            //Initialize State Machine Engine		
            fsm = StateMachine<States>.Initialize(this);
        }
        void Start()
        {

            fsm.ChangeState(States.Wander);

        }
        private void Update()
        {

            _fieldOfView.FindVisibleTargets();

        }
        void Wander_Enter()
        {
            _wander.fsm.ChangeState(Wander.States.Wander);
        }
        void Wander_Update()
        {
            if (_fieldOfView.visibleTargets.Count > 0)
                fsm.ChangeState(States.Pursue);
        }
        void Wander_Exit()
        {
            _wander.fsm.ChangeState(Wander.States.Finish);

        }

        void Pursue_Enter()
        {
            
            _pursue.fsm.ChangeState(Pursue.States.Pursue);
        }
        void Pursue_Update()
        {
            if (_fieldOfView.visibleTargets.Count == 0)
                fsm.ChangeState(States.Wander);

        }
        void Pursue_Exit()
        {
            _pursue.fsm.ChangeState(Pursue.States.Idle);

        }
    }
}