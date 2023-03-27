using BusinessLayer;
using BusinessLayer.Implementation;
using BusinessLayer.Interface;
using DataLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<DataManager>();

//Инициализация БД
var service = builder.Services.BuildServiceProvider().CreateScope().ServiceProvider;
DiplomContext context = service.GetRequiredService<DiplomContext>();
InitializeDB.InitDB(context);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=Index}/{id?}");

app.Run();
