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