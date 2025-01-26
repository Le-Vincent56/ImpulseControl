using System;
using System.Collections.Generic;

namespace ImpulseControl.AI
{
    public class StateMachine 
    {
        StateNode current;
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransitions = new();

        class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }
            current.State?.Update();
        }

        public void FixedUpdate()
        {
            current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }

        void ChangeState(IState state)
        {
            // Prevent changing into itself
            if (state == current.State) return; 

            var previousState = current.State;
            var nextState = nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            
            current = nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            // Check if a condition meets a transition from the Any State
            foreach(var transition in anyTransitions)
            {
                if(transition.Condition.Evaluate()) return transition;
            }
            // If none, check if a condition is met within the current state's transitions
            foreach(var transition in current.Transitions)
            {
                if (transition.Condition.Evaluate()) return transition;
            }
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }
        StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());

            // If node not found, the default value (null) is returned. 
            // Create and add the node to our dictionary if this is the case. 
            if(node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }

            return node;
        }

    }
}
