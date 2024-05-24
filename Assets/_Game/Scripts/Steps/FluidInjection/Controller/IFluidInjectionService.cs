using System;

namespace Scripts.FluidInjection
{
    public interface IFluidInjectionService
    {
        public event Action OnBegin;
        public event Action OnEnd;

        //public HairCuttingPathView PathView { get; }
        //public HairCuttingCameraOffset CameraOffset { get; }
        //public HairCuttingPath CurrentPath { get; }
    }
}
