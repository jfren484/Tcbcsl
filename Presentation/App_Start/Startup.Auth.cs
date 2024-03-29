﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Tcbcsl.Data;
using Tcbcsl.Data.Identity;
using Microsoft.Owin.Security.Google;
using System.Configuration;
using Microsoft.Owin.Security.Facebook;

namespace Tcbcsl.Presentation
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(TcbcslDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
                                        {
                                            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                                            LoginPath = new PathString("/Account/Login"),
                                            Provider = new CookieAuthenticationProvider
                                                       {
                                                           // Enables the application to validate the security stamp when the user logs in.
                                                           // This is a security feature which is used when you change a password or add an external login to your account.  
                                                           OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, TcbcslUser>(
                                                               validateInterval: TimeSpan.FromMinutes(30),
                                                               regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                                                       }
                                        });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var options = new FacebookAuthenticationOptions
                          {
                              AppId = ConfigurationManager.AppSettings["Facebook.AppId"],
                              AppSecret = ConfigurationManager.AppSettings["Facebook.AppSecret"]
                          };
            options.Scope.Add("email");
            app.UseFacebookAuthentication(options);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                                        {
                                            ClientId = ConfigurationManager.AppSettings["Google.ClientId"],
                                            ClientSecret = ConfigurationManager.AppSettings["Google.ClientSecret"]
                                        });

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
        }
    }
}