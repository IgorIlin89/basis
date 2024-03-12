using FirstTry;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var person = new Employee();
Person person2 = new Employee();
Person person3 = null;

person.Name = "Igor";
Console.WriteLine(person.Name);
long longInteger = 25; // first type of var cannot be changed after
var smallInteger = (int)longInteger;

int a = 7;
int b = 4;
int c = 3;
int d = (a + b) / c;
int e = (a + b) % c;
Console.WriteLine($"quotient: {d}");
Console.WriteLine($"remainder: {e}");

int maxInteger = int.MaxValue;
int minInteger = int.MinValue;
Console.WriteLine($"The range of integers is {minInteger} to {maxInteger}");
int what = maxInteger + 3;
Console.WriteLine($"An example of overflow: {what}");

double maxDouble = double.MaxValue;
double minDouble = double.MinValue;
Console.WriteLine($"The range of double is {minDouble} to {maxDouble}");

double third = 1.0 / 3.0;
Console.WriteLine(third);

decimal minDecimal = decimal.MinValue;
decimal maxDecimal = decimal.MaxValue;
Console.WriteLine($"The range of the decimal type is {minDecimal} to {maxDecimal}");

double aDouble = 1.0;
double bDouble = 3.0;
Console.WriteLine(aDouble / bDouble);

decimal cDecimal = 1.0M;
decimal dDecimal = 3.0M;
Console.WriteLine(cDecimal / dDecimal);

if (person2 is Employee)
{
  Console.WriteLine(((Employee)person2).AreaOfCircle(2.5));
}

if (person2 is Employee employee)
{
  Console.WriteLine(employee.AreaOfCircle(2.5));
}