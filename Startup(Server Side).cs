
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer()      //BU BÖLÜM SİZİN STARTUP CLASS INIZIN BANA GÖRE YAPILANDIRILMASI OLACAK;
                .AddInMemoryIdentityResources(Config.Ids)  // CONFİG CLASSI REFERANS OLARAK EKLENİYOR
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users);  // TÜM BİLGİLER EŞLEŞİYOR İSE, LOGİN SAYFAMA YÖNLENDİRİYOR  
															//BENİ VE BURDA BULUNANAN TEST USERLER ile GİRİŞ SAĞLIYORUM.
                                                       //     SIZIN EKLEYECEĞİNİZ DATABASE İLE ALAKALI MUHTEMEL ŞEYLER OLABİLİR BURADA
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "ID";
                    options.ClientSecret = "SECRET";
                })
                .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>

                {
                    // BURADAKI VERİLER SİZİNLE AZURE ARASINDAKI BAGLANTI İÇİN GEREKEN BİLGİLERİNİZ
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";     
                    options.ClientId = "native.code";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                  
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}