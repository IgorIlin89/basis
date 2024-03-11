using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTry;

public abstract class Person
{

  private string _privateString; // convention: Public with capital case, private with underscore ( _p)

  protected bool _trueOrFalse;

  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public int Age { get; set; }
  public int ReadOnly { get; }
  public int InitOnly { get; init; }
  public double MyProperty { get; set; }
  public decimal MyProperty1 { get; set; }
  public byte[] FileArray { get; set; }
  public DateTime? Date { get; set; } //? means that it is nullable, it can be either an object or null. It means "31.12.2025 11:59:59" //class with ? is not initiallized it will be null
  public DateTimeOffset Date2 { get; set; } //It means "31.12.2025 11:59:59 +01:00"

  public Person()
  {

  }

  public void EmptyFunction()
  {

  }

  public int EmptyFunction(int x = 0)
  {
    return x;
  }

  public string EmptyFunctionToString(int x)
  {
    return x.ToString();
  }

  public int EmptyFunctionToString(string x) //"9" "9.0" "9,1"
  {
    return Int32.Parse(x);
  }

  public int FunctionReturnsInt()
  {
    return 0;
  }

  public virtual Gender GetGender()
  {
    return Gender.Female;
  }

  public abstract int TestAbstract(int x);

}
