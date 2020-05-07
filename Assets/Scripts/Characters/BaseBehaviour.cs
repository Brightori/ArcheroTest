using System;

namespace Characters
{
    public abstract class BaseBehaviour : IDisposable
    {
        public abstract void Update();
        public abstract void Pause();
        public abstract void UnPause();

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
