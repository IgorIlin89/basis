﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using OnlineShopWeb.Controllers;

public class AuthorizeControllerModelConvention : IControllerModelConvention
{
    public static string PolicyName = "AuthorizePolicy";


    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerName.Contains("Login")) //(typeof(LoginController).GetControllerName()))
        {
            controller.Filters.Add(new AuthorizeFilter(PolicyName));
        }
    }
}