using System;

namespace Behaviours
{
    public abstract class BaseBehaviour : IBehaviour
    {
        public abstract void Update();
        public abstract void Pause();
        public abstract void UnPause();

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public interface IBehaviour : IDisposable
    {
        void Update();
        void Pause();
        void UnPause();
    }
}
