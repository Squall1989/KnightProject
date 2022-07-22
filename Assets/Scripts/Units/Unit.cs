using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            SetInitiables(2);
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
            foreach (var behaviour in unitBehaviours)
            {
                // Get initiable interface of behaviour
                Type initiating = behaviour.GetType().GetInterface("Initiable`" + numOfArguments);

                if (initiating == null)
                    continue;

                List<object> argList = new List<object>(numOfArguments);

                // Get initiable interface argument type
                foreach(var argument in initiating.GetGenericArguments())
                {
                    var initiable = FindBehaviourOfType(argument);

                    if (initiable != null)
                        argList.Add(initiable);
                }
                if(argList.Count == numOfArguments)
                    initiating.GetMethod("Init").Invoke(behaviour, argList.ToArray()); ;

            }
        }


        protected UnityEngine.Object FindBehaviourOfType<T>(T type) where T: Type
        {

            foreach(var behaviour in unitBehaviours)
            {
                Type behType = behaviour.GetType();
                if(behType == type || behType.IsSubclassOf(type))
                {
                    return behaviour;
                }    
            }

            return null;
        }
    }
}