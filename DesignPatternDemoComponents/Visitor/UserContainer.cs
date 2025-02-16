namespace DesignPatternDemoComponents.Visitor;

//Step 5: Implement Object Structure
public class UserContainer
{
  private readonly List<IElement> _users = new();

  public void AddUser(IElement user) => _users.Add(user);

  public List<string> ApplyVisitor(IVisitor visitor)
  {
    var result = new List<string>();

    foreach (var user in _users)
    {
      result.Add(user.Accept(visitor));
    }

    return result;
  }
}
