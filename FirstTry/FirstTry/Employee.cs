namespace FirstTry;

internal class Employee : Person, IEmployee
{


  //public double AreaOfCircle { get; set; }
  public override int TestAbstract(int x)
  {
    throw new NotImplementedException();
  }

  public override Gender GetGender()
  {
    var gender = base.GetGender();

    if (gender == Gender.Male)
    {
      gender = Gender.Female;
    }
    else
    {
      gender = Gender.Male;
    }

    gender = gender == Gender.Male ? Gender.Female : Gender.Male;

    return gender;
  }

  public double AreaOfCircle(double r)
  {
    //this.AreaOfCircle = (r * r) * Math.PI;
    return (r * r) * Math.PI;
  }
}
