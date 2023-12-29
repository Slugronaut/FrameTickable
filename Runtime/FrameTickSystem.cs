using Peg.AutoCreate;
using Peg.UpdateSystem;
using System.Collections.Generic;

namespace Peg.Systems
{
    /// <summary>
    /// 
    /// </summary>
    [AutoCreate(resolvableTypes: typeof(IFrameTickSystem))]
    public class FrameTickSystem : Updatable, IFrameTickSystem
    {
        public static FrameTickSystem Instance;
        List<IFrameTickable> Tickables;

        void AutoAwake()
        {
            Instance = this;
            Tickables = new List<IFrameTickable>();
        }

        public override void Update()
        {
            int len = Tickables.Count;
            for (int i = 0; i < len; i++)
            {
                var tickable = Tickables[i];
                if (tickable.TickEnabled)
                    tickable.OnTick();
            }
        }

        public void Register(IFrameTickable tickable)
        {
            Tickables.Add(tickable);
        }

        public void Unregister(IFrameTickable tickable)
        {
            Tickables.Remove(tickable);
        }
    }


    /// <summary>
    /// Implemented on any class that wishes to register itself with the FrameTickSystem
    /// and receive callbacks every frame.
    /// </summary>
    public interface IFrameTickable
    {
        void OnTick();
        bool TickEnabled { get; }
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IFrameTickSystem
    {
        void Register(IFrameTickable tickable);
        void Unregister(IFrameTickable tickable);
    }
}
