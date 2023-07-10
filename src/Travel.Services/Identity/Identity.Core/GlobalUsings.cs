﻿global using System;
global using System.Collections.Immutable;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json;
global using Ardalis.Specification;
global using Identity.Core.Data;
global using Identity.Core.Dtos;
global using Identity.Core.Entities;
global using Identity.Core.Helpers;
global using Identity.Core.Interfaces;
global using Identity.Core.Respositories;
global using Identity.Core.Specifications;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using OpenIddict.Abstractions;
global using OpenIddict.Server.AspNetCore;
global using Travel.BaseHttps.BaseEndpoints;
global using Travel.Repository.Data;
global using Travel.Repository.Interfaces;
global using static OpenIddict.Abstractions.OpenIddictConstants;
