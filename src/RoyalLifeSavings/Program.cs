using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Integrations;
using RoyalLifeSavings.Integrations.SendGrid;
using RoyalLifeSavings.Integrations.VitalSource;
using SendGrid;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RoyalLifeSavingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<RoyalLifeSavingDbContext>()
    .AddDefaultTokenProviders();





builder.Services.AddHttpClient<VitalSourceService>(client => client.DefaultRequestHeaders.Add("X-VitalSource-API-Key", builder.Configuration.GetValue<string>("VitalSource:ApiKey")));
builder.Services.AddScoped<IStripeClient>(x =>
               new StripeClient(
                   apiKey: builder.Configuration.GetValue<string>($"Stripe:SecretApiKey"),
                   httpClient: new SystemNetHttpClient(x.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(StripeClient)))
                   ));

builder.Services.AddScoped<VitalSourceWorkflow>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
builder.Services.AddTransient<ISendGridClient, SendGridClient>(x => new SendGridClient(builder.Configuration.GetValue<string>("Sendgrid:ApiKey")));
// Add services to the container.

builder.Services.AddRazorPages(options => options.Conventions.AuthorizePage("/PurchaseLibrary"));
builder.Services.AddControllers();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

//reduce the token lifespan for all workflows (good solution if only one is used) 
builder.Services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromMinutes(15));
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // TODO: Review what values we should have here
    options.KnownProxies.Clear();
    options.KnownNetworks.Clear();
});

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    //use dataprotectionkeys stored properly


}

using var scope = app.Services.CreateScope();
await using var db = scope.ServiceProvider.GetRequiredService<RoyalLifeSavingDbContext>();
if ((await db.Database.GetPendingMigrationsAsync()).Any())
{
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    if (!await db.EBooks.AnyAsync(x => x.Id == "VCS0429073907956"))
    {
        db.EBooks.Add(new EBook
        {
            Name = "Lifeguarding 6th Edition",
            Id = "VCS0429073907956",
            StripePriceId = "price_1MEhi9CzH3nRAVCIpx6NVSSf",
            Edition = "6th Edition - 2022",
            Author = "Author",
            TaxApplicable = true
        });
        await db.SaveChangesAsync();
    }
}
else
{
    if (!await db.EBooks.AnyAsync(x => x.Id == "VCS0429073907956"))
    {
        db.EBooks.Add(new EBook
        {
            Name = "Lifeguarding 6th Edition",
            Id = "VCS0429073907956",
            StripePriceId = "price_1MEiP1CzH3nRAVCITy08BR2q",
            Edition = "6th Edition - 2022",
            Author = "Author",
            TaxApplicable = true
        });
        await db.SaveChangesAsync();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/{0}");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
