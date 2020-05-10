using Characters;
using Commands;
using System;

namespace Behaviours
{
    public abstract class BaseBehaviour : IBehaviour
    {
        public IActor Actor { get; set; }

        public abstract void Update();
        public abstract void Pause();
        public abstract void UnPause();

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual void CommandBehavaiour(ICommand command) { }
    }

    public interface IBehaviour : IDisposable
    {
        IActor Actor { get; set; }

        void CommandBehavaiour(ICommand command);
        void Update();
        void Pause();
        void UnPause();
    }
}
