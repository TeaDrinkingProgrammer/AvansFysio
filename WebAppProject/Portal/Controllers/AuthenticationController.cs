using DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Domain;
using System;
using System.Security.Claims;

namespace Portal.Controllers {

    namespace SportsStore.Controllers {
        public class AuthenticationController : Controller {
            private UserManager<IdentityUser> _userManager;
            private SignInManager<IdentityUser> _signInManager;
            private IPatientRepo _patientRepository;
            private IEmployeeRepo _employeeRepository;
            public AuthenticationController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr, IPatientRepo patientRepository, IEmployeeRepo employeeRepository) {
                _userManager = userMgr; _signInManager = signInMgr; _patientRepository = patientRepository; _employeeRepository = employeeRepository;
            }
            [HttpGet]
            public ViewResult Login() {
                return View(new LoginModel());
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginModel loginModel) {
                if (ModelState.IsValid) {
                    IdentityUser user = await _userManager.FindByEmailAsync(loginModel.Email);
                    if (user != null) {
                        //Claims worden eerder opgevraagd om inlog te versnellen
                        var claims = await _userManager.GetClaimsAsync(user);
                        await _signInManager.SignOutAsync();
                        var signInState = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                        if (signInState.Succeeded) {
                            if (claims.Any(claim => claim.Value == "Patient")) {
                                return RedirectToAction("Index", "Patient");
                            } else {
                                return RedirectToAction("Index", "Employee");
                            }
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid email or password");
                return View(loginModel);
            }
            [HttpGet]
            public ViewResult Register() {
                return View(new RegisterModel());
            }
            [HttpPost]
            public async Task<IActionResult> Register(RegisterModel registerModel) {
                if (ModelState.IsValid) {
                    //Checks wether a user is an employee or patient by looking up the emailadress
                    List<Patient> patientResults = _patientRepository.Get().Where(x => x.EmailAdress == registerModel.Email).ToList();
                    List<Employee> employeeResults = _employeeRepository.Get().Where(x => x.EmailAdress == registerModel.Email).ToList();
                    if (await _userManager.FindByEmailAsync(registerModel.Email) != null) {
                        ModelState.AddModelError("Email", "Dit emailadres is heeft al een account. Ga naar de loginpagina om in te loggen");
                    } else if (registerModel.Password != registerModel.PasswordCheck) {
                        ModelState.AddModelError("Password", "Uw wachtwoorden komen niet overeen");
                    } else if (patientResults.Count == 1) {
                        //UserName has been changed to accomodate spaces
                        //TODO Maybe add checks for special characters?
                        string userName = String.Concat(patientResults.First().Name);
                        IdentityUser user = new() { UserName = userName, Email = registerModel.Email };
                        IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);
                        if (result.Succeeded) {
                            //Patient gets patientclaim which is coupled to policy in startup
                            Claim claim = new(ClaimTypes.Authentication, "Patient");
                            await _userManager.AddClaimAsync(user, claim);
                            return RedirectToAction("RegisterSuccess");
                        } else {
                            ModelState.AddModelError("", "Er is iets foutgegaan tijdens het registreren van uw account. Neem aub contact op met de administrator");
                        }
                    } else if (employeeResults.Count == 1) {
                        //UserName has been changed to accomodate spaces in startup
                        //TODO Maybe add checks for special characters?
                        string userName = String.Concat(patientResults.First().Name);
                        IdentityUser user = new() { UserName = userName, Email = registerModel.Email };
                        IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);
                        if (result.Succeeded) {
                            await _userManager.AddClaimAsync(user, new(ClaimTypes.Authentication, "Employee"));
                            //Every employee has employee claim and either the therapistclaim (= bignumber) or studentemployee
                            if (employeeResults.First().IsTherapist) {
                                await _userManager.AddClaimAsync(user, new(ClaimTypes.Authentication, "TherapistEmployee"));
                            } else {
                                await _userManager.AddClaimAsync(user, new(ClaimTypes.Authentication, "StudentEmployee"));
                            }
                            return RedirectToAction("RegisterSuccess");
                        } else {
                            ModelState.AddModelError("", "Er is iets foutgegaan tijdens het registreren van uw account. Neem aub contact op met de administrator");
                        }
                    } else {
                        ModelState.AddModelError("Email", "Dit Emailadres bestaat niet. Om een account te maken moet uw emailadres aangemeld zijn door uw therapeut.");
                    }
                }
                return View(registerModel);
            }
            public ViewResult RegisterSuccess() {
                return View();
            }

            [HttpGet]
            public async Task<RedirectToActionResult> Logout() {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            [HttpGet]
            public ViewResult AccessDenied() {
                return View();
            }

        }
    }
}
