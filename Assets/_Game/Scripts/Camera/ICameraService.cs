namespace Scripts.Camera
{
    public interface ICameraService
    {
        CameraView        CameraView   { get; }
        CameraStateConfig CurrentState { get; }

        void ChangeCameraState( string stateId );
    }
}
