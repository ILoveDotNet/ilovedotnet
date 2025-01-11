namespace DesignPatternDemoComponents.Facade;

// Facade class
public class EntertainmentFacade
{
  private readonly DvdPlayer _dvdPlayer;
  private readonly SoundSystem _soundSystem;
  private readonly Projector _projector;

  public EntertainmentFacade()
  {
    _dvdPlayer = new DvdPlayer();
    _soundSystem = new SoundSystem();
    _projector = new Projector();
  }

  public void TurnOnEntertainmentSystem()
  {
    _dvdPlayer.TurnOn();
    _soundSystem.TurnOn();
    _projector.TurnOn();
  }

  public void TurnOffEntertainmentSystem()
  {
    _dvdPlayer.TurnOff();
    _soundSystem.TurnOff();
    _projector.TurnOff();
  }
}
