﻿namespace OnlineShopWeb.Dtos;

public class ErrorDto
{
    public ErrorStatusCodeDto StatusCode { get; set; }
    public string? Message { get; set; }
}
