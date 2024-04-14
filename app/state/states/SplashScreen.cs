namespace Crackdownlike;
public partial class AppLogic
{
  public partial record State
  {
    public record SplashScreen : State, IGet<Input.FadeOutFinished>
    {
      public SplashScreen()
      {
        OnEnter<SplashScreen>(
          (previous) => Context.Output(new Output.ShowSplashScreen())
        );

        OnAttach(
          () => Get<IAppRepo>().SplashScreenSkipped += OnSplashScreenSkipped
        );

        OnDetach(
          () => Get<IAppRepo>().SplashScreenSkipped -= OnSplashScreenSkipped
        );
      }

      public IState On(Input.FadeOutFinished input) => new MainMenu();

      public void OnSplashScreenSkipped() =>
        Context.Output(new Output.HideSplashScreen());
    }
  }
}
