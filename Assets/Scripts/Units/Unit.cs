using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base for moveable and attackable units
/// </summary>
namespace KnightProject
{
    [DisallowMultipleComponent]
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected BaseBehaviour[] unitBehaviours;

        private List<IKillable> killables = new List<IKillable>();

        protected virtual void Start()
        {
            SetKillables();
            // Auto init 
            SetInitiables(1);
            // Manual init
            SetInitiables();
        }

        protected virtual void SetKillables()
        {
            foreach(BaseBehaviour baseBehaviour in unitBehaviours)
            {
                if (baseBehaviour is IKillable)
                    killables.Add(baseBehaviour);
            }
        }

        protected virtual void SetInitiables()
        {
            
        }

        protected virtual void SetInitiables(int numOfArguments)
        {
            foreach (BaseBehaviour behaviour in unitBehaviours)
            {
                // Get initiable interface of behaviour
                Type initInterface = behaviour.GetType().GetInterface("Initiable`" + numOfArguments);

                if (initInterface == null)
                    continue;

                // Get initiable interface argument type
                foreach(var argument in initInterface.GetGenericArguments())
                {
                    initBehaviour(argument);
                }
                // Init behaviour with this type
                void initBehaviour(Type argument)
                {
                    BaseBehaviour initiable = FindBehaviourOfType(argument);

                    if (initiable != null)
                    {
                        (initInterface as Initiable<BaseBehaviour>).Init(initiable);
                    }
                }
            }
        }


        protected BaseBehaviour FindBehaviourOfType(Type type)
        {
            foreach(BaseBehaviour behaviour in unitBehaviours)
            {
                if(behaviour.GetType() == type)
                {
                    return behaviour;
                }    
            }

            return null;
        }
    }
}