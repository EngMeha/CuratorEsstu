using BusinessLayer;
using BusinessLayer.Implementation;
using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quartz.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DiplomContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("DataLayer")));
builder.Services.AddTransient<IBasisOfLearning, EFBasisOfLearning>();
builder.Services.AddTransient<ICraduationDepartament, EFCraduationDepartament>();
builder.Services.AddTransient<IEvent, EFEvent>();
builder.Services.AddTransient<IEventOfStudent, EFEventOfStudy>();
builder.Services.AddTransient<IFormOfStudy, EFFormOfStudy>();
builder.Services.AddTransient<IGroup, EFGroup>();
builder.Services.AddTransient<IHistoryChangeStudent, EFHistoryChangeStudent>();
builder.Services.AddTransient<IStudent, EFStudent>();
builder.Services.AddTransient<IUser, EFUser>();
builder.Services.AddScoped<DataManager>();


//подключаем identity
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DiplomContext>();

//Инициализация БД
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var service = scope.ServiceProvider;
    DiplomContext context = service.GetRequiredService<DiplomContext>();
    await InitializeDB.InitDB(context);

    UserManager<User> userManager = service.GetRequiredService<UserManager<User>>();
    RoleManager<IdentityRole> roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
    await InitializeDB.InitRole(userManager, roleManager);
}


builder.Services.AddControllersWithViews();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Login}/{id?}");

app.Run();
