﻿using LumiaMVC.Models;
using LumiaMVC.ViewModels.AccountVM;
using LumiaMVC.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LumiaMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    { 
        if(!ModelState.IsValid)
        {
            return View();
        }
        AppUser newUser = new AppUser()
        {
            Name = registerVM.Name,
            Surname = registerVM.Surname,
            Email = registerVM.EmailAdress,
            UserName = registerVM.UserName
        };
        IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        AppUser appUser = await _userManager.FindByEmailAsync(loginVM.EmailAdress);
        if (appUser is null)
        {
            ModelState.AddModelError("", "Invalid Password or Email adress");
            return View();
        }
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Email or Passwor Wrong");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
