using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentForum.BusinessLogic.Extensions;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess;
using StudentForum.WebUI.MapperConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SFDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SFDatabaseContext>();

builder.Services.AddAutoMapper(typeof(AccountMapperProfile).Assembly,
    typeof(StudentForum.BusinessLogic.MapperConfig.MapperProfile).Assembly);

builder.Services.AddServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
