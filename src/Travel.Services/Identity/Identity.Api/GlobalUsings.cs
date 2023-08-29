global using System;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography.X509Certificates;
global using Identity.Api.Certificates;
global using Identity.Api.DI;
global using Identity.Api.Endpoints;
global using Identity.Core.Data;
global using Identity.Core.Dtos;
global using Identity.Core.Entities;
global using Identity.Core.Helpers;
global using Identity.Core.Interfaces;
global using Identity.Core.Respositories;
global using Identity.Core.Services;
global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using OpenIddict.Abstractions;
global using OpenIddict.Server.AspNetCore;
global using OpenIddict.Validation.AspNetCore;
global using Quartz;
global using Serilog;
global using Travel.Repository.Data;
global using static OpenIddict.Abstractions.OpenIddictConstants;



