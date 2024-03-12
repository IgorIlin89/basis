namespace OnlineShopWeb.Domain;

public class StaticSingleton
{
  private StaticSingleton()
  {

  }

  public static StaticSingleton Create()
  {
    return new StaticSingleton();
  }
}
