using GeekShooping.Web.Services;
using GeekShooping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//adicionado o http cliente
builder.Services.AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]));
builder.Services.AddHttpClient<ICartService, CartService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartApi"]));
builder.Services.AddHttpClient<ICouponService, CouponService>(c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
      .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
      .AddOpenIdConnect("oidc", options =>
      {
          options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
          options.GetClaimsFromUserInfoEndpoint = true;
          options.ClientId = "geek_shopping";
          options.ClientSecret = "my_super_secret";
          options.ResponseType = "code";
          options.ClaimActions.MapJsonKey("role", "role", "role");
          options.ClaimActions.MapJsonKey("sub", "sub", "sub");
          options.TokenValidationParameters.NameClaimType = "name";
          options.TokenValidationParameters.RoleClaimType = "role";
          options.Scope.Add("geek_shopping");
          options.SaveTokens = true;
          options.RequireHttpsMetadata = false;

      }
  );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// utiliza 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
