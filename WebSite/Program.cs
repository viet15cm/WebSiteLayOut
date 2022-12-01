using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
using WebSite.DbContextLayer;
using WebSite.Models.Identity;
using WebSite.Services.IdentityErrors;
using WebSite.Services.IdentityStoreServices;
using WebSite.Services.MailServices;
using WebSite.Services.MappingFileServices;
using WebSite.Services.MappingImageServices;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddTransient<IdentityStoreServices>();

builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ISendMailServices, SendMailServices>();

builder.Services.AddTransient<IFileServices, FileServices>();

builder.Services.AddTransient<IdentityStoreServices>();
builder.Services.AddTransient<ISendMailServices, SendMailServices>();

builder.Services.AddTransient<IimageServices, ImageServices>();

builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
builder.Services.AddSingleton<AppIdentityErrorDescriber>();

builder.Services.AddHttpClient("hosting").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }

});
builder.Services.AddSession();
//services.AddHttpClient("hosting", 
//    c => c.BaseAddress = new System.Uri("https://localhost:5001"));
var lockoutOptions = new LockoutOptions()
{
    AllowedForNewUsers = true,
    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
    MaxFailedAccessAttempts = 5
};

builder.Services.AddIdentity<AppUser, IdentityRole>()
  .AddEntityFrameworkStores<IdentityStoreServices>()
  .AddSignInManager<UserNameEmailSignInManager>()
.AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options => {

    //Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 8; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt


    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;


    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    options.Lockout = lockoutOptions; // khóa lockout

    //Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = false;
});
// Thiết lập , nếu IsPersistent = true thì đăng nhập user sẽ duy trì trong 1 ngày ,  IsPersistent = fasle thì đăng nhập user sẽ duy trì mãi mãi , với cookie máy khách 
builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromDays(1);
    option.LoginPath = "/Identity/Account/Login";
    option.LogoutPath = "/Identity/Account/Logout";
    option.AccessDeniedPath = $"/Identity/Account/Manager/AccessDenied";
});

builder.Services.AddAuthorization(options => {

    options.AddPolicy("Admin", builder => {
        builder.RequireAuthenticatedUser();
        builder.RequireRole("Admin");

    });

    options.AddPolicy("ProEmployee", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole("Admin", "ProEmployee");

    });

    options.AddPolicy("Employee", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole("Admin", "Employee", "ProEmployee");

    });

    options.AddPolicy("EmployeeSharedpostCategory", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole("Admin", "Employee", "ProEmployee");

        builder.RequireClaim("sharedpost", "sharedpost");
    });

    options.AddPolicy("EmployeeSharedAdvancedAddChildPost", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole("Admin", "ProEmployee");

        builder.RequireClaim("advancedaddchildpost", "advancedaddchildpost");
    });


});

builder.Services.Configure<SecurityStampValidatorOptions>(options => {
    options.ValidationInterval = TimeSpan.FromSeconds(10);

});

builder.Services.AddMemoryCache();
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePages(appBuilder => {

    appBuilder.Run(async context => {

        var reponse = context.Response;
        var code = context.Response.StatusCode;

        await reponse.WriteAsync($"Trang {code} {(HttpStatusCode)code}");

    });
});


app.UseRouting();
app.UseRouting();
app.UseSession();
app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
app.UseAuthorization();   // Phục hồi thông tinn về quyền của User
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new
        {
            controller = "Home",
            action = "index"
        },
        constraints: new
        {
            id = new GuidRouteConstraint()
        }

    );

    endpoints.MapRazorPages();
});

app.Run();
