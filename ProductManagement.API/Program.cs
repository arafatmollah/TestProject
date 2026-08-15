using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.API.Middleware;
using ProductManagement.Application.Auth.Login;
using ProductManagement.Application.Auth.Register;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Products.Create;
using ProductManagement.Application.Products.Delete;
using ProductManagement.Application.Products.Get;
using ProductManagement.Application.Products.GetById;
using ProductManagement.Application.Products.Update;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Services;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Repositories;
using ProductManagement.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<RegistrationService>();

builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILogInService, LoginService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
builder.Services.AddAuthorization();
builder.Services.AddScoped<ProductDomainService>();

builder.Services.AddScoped<
    IPasswordHasher<User>,
    PasswordHasher<User>>();


builder.Services.AddMemoryCache();
builder.Services.AddScoped<
    ICreateProductService,
    CreateProductService>();
builder.Services.AddScoped<
    IGetProductsService,
    GetProductsService>();
builder.Services.AddScoped<
    IGetProductByIdService,
    GetProductByIdService>();
builder.Services.AddScoped<
    IUpdateProductService,
    UpdateProductService>();
builder.Services.AddScoped<
    IDeleteProductService,
    DeleteProductService>();
builder.Services.AddScoped<
    IValidator<CreateProductDto>,
    CreateProductValidator>();
builder.Services.AddScoped<
    IValidator<UpdateProductDto>,
    UpdateProductValidator>();
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();