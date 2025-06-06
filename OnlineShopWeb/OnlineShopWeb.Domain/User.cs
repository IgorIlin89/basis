﻿using System.ComponentModel.DataAnnotations;
namespace OnlineShopWeb.Domain;

public class User
{
    public int Id { get; set; }
    public string EMail { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
    public int PostalCode { get; set; }
    public string? Password { get; set; }
    public ICollection<Transaction>? TransactionHistories { get; set; }

}
