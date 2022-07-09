using UnityEngine;

namespace KnightProject
{
    /// <summary>
    /// Basis for all units behaviours
    /// </summary>
    public abstract class BaseBehaviour : MonoBehaviour, IKillable
    {
        protected bool isDead;

        public virtual void Kill()
        {
            isDead = true;
        }

        public virtual void Resurrect()
        {
            isDead = false;
        }
    }
}