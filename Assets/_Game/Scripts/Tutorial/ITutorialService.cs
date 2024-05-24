namespace Scripts.Tutorial
{
    public interface ITutorialService
    {
        void RegisterPopUp( TutorialPopUp   popup );
        void RegisterWindow( TutorialWindow window );
        void EndTutorial();
    }
}
