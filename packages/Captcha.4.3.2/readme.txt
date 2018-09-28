The BotDetect ASP.NET CAPTCHA NuGet package has been added to your project.

    1. Captcha integration instructions
    2. ASP.NET Captcha code examples download
    3. BotDetect Captcha documentation


-------------------------------------------------------------------------------
 1. CAPTCHA INTEGRATION INSTRUCTIONS
-------------------------------------------------------------------------------

To show a Captcha challenge on the form and check user input during form submission:

ASP.NET MVC 6
-------------------------------------------------------------------------------
In View code: import the BotDetect namespace, include BotDetect styles in page <head>, create a MvcCaptcha object and pass it to the Captcha HtmlHelper:

        @using BotDetect.Web.Mvc;

          [...]

          <link href="@BotDetect.Web.CaptchaUrls.Absolute.LayoutStyleSheetUrl" rel="stylesheet" type="text/css" />
        </head>

          [...]
				@{ var exampleCaptcha = new MvcCaptcha("ExampleCaptcha", ViewContext.Current()) { UserInputID = "CaptchaCode" }; }
				<captcha mvc-captcha="exampleCaptcha" />
				<input asp-for="CaptchaCode" />
				<input type="submit" value="Validate" />
				<span asp-validation-for="CaptchaCode"></span>
When the form is submitted, the Captcha validation result must be checked:
				@if (ViewContext.IsPost() && ViewData.ModelState.IsValid)
				{
					<span class="correct">Correct!</span>
				}

Open Startup.cs and update it in the way demonstrated in the following:
 - The ConfigureServices method:
				public void ConfigureServices(IServiceCollection services)
				{
					services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
					services.AddMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
					services.AddMvc(); // Add framework services.
					services.AddSession(options =>
					{
						options.IdleTimeout = TimeSpan.FromMinutes(20);
					});
				}
 - The Configure method:				
				public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
				{
					[...]
					app.UseSession();
					
					//configure BotDetectCaptcha
					app.UseCaptcha(Configuration);
					[...]
				}
				
To expose BotDetect Captcha Tag Helper in your project, you would use the following in Views/_ViewImports.cshtml file:
				[...]
				@addTagHelper "BotDetect.Web.Mvc.CaptchaTagHelper, BotDetect.Web.Mvc"
				
Mark the protected Controller action with the CaptchaValidation attribute to include Captcha validation in ModelState.IsValid checks:

    using BotDetect.Web.Mvc;

    [HttpPost]
    [AllowAnonymous]
    [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
    public ActionResult ExampleAction(ExampleModel model)
    {
        if (!ModelState.IsValid)
        {
            // TODO: Captcha validation failed, show error message
        }
        else
        {
            // TODO: Captcha validation passed, proceed with protected action
        }

Detailed ASP.NET MVC integration instructions can be found at http://captcha.com/doc/aspnet/howto/asp.net-mvc-captcha.html.

ASP.NET MVC 1-5
-------------------------------------------------------------------------------
In View code: import the BotDetect namespace, include BotDetect styles in page <head>, create a MvcCaptcha object and pass it to the Captcha HtmlHelper:

        @using BotDetect.Web.Mvc;

          [...]

          <link href="@BotDetect.Web.CaptchaUrls.Absolute.LayoutStyleSheetUrl" rel="stylesheet" type="text/css" />
        </head>

          [...]

        @{ MvcCaptcha exampleCaptcha = new MvcCaptcha("ExampleCaptcha"); }
        @Html.Captcha(exampleCaptcha)
        @Html.TextBox("CaptchaCode")

Exclude BotDetect paths from ASP.NET MVC Routing:

    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        // BotDetect requests must not be routed
        routes.IgnoreRoute("{*botdetect}",  new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

Mark the protected Controller action with the CaptchaValidation attribute to include Captcha validation in ModelState.IsValid checks:

    using BotDetect.Web.Mvc;

    [HttpPost]
    [AllowAnonymous]
    [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
    public ActionResult ExampleAction(ExampleModel model)
    {
        if (!ModelState.IsValid)
        {
            // TODO: Captcha validation failed, show error message
        }
        else
        {
            // TODO: Captcha validation passed, proceed with protected action
        }

Detailed ASP.NET MVC integration instructions can be found at http://captcha.com/doc/aspnet/howto/asp.net-mvc-5.0-captcha.html.

ASP.NET Web Pages
-------------------------------------------------------------------------------
On the ASP.NET form you want to protect against bots, add:

@{
    Captcha exampleCaptcha = new Captcha("ExampleCaptcha");
    exampleCaptcha.UserInputID = "CaptchaCode";
}
When the form is submitted, the Captcha validation result must be checked:

@if ((Request.HttpMethod == "POST"))
{
    bool isHuman = exampleCaptcha.Validate();
    if (isHuman)
    {
    <span class="correct">Correct!</span>
    }
    else
    {
    <span class="incorrect">Incorrect!</span>
    }
}
Detailed ASP.NET Web Pages integration instructions can be found at http://captcha.com/doc/aspnet/howto/asp.net-webpages-captcha.html.

ASP.NET WebForms
-------------------------------------------------------------------------------
On the ASP.NET form you want to protect against bots, add:

    <BotDetect:WebFormsCaptcha ID="ExampleCaptcha" runat="server" />
    <asp:TextBox ID="CaptchaCodeTextBox" runat="server" />

When the form is submitted, the Captcha validation result must be checked:

    if (IsPostBack)
    {
        // validate the Captcha to check we're not dealing with a bot
        bool isHuman = ExampleCaptcha.Validate(CaptchaCodeTextBox.Text);

        CaptchaCodeTextBox.Text = null; // clear previous user input

        if (!isHuman)
        {
            // TODO: Captcha validation failed, show error message
        }
        else
        {
            // TODO: Captcha validation passed, proceed with protected action
        }
    }

Detailed ASP.NET WebForms integration instructions can be found at http://captcha.com/doc/aspnet/howto/asp.net-webforms-captcha.html.


-------------------------------------------------------------------------------
 2. ASP.NET CAPTCHA CODE EXAMPLES DOWNLOAD
-------------------------------------------------------------------------------

CAPTCHA code examples (ASP.NET WebForms integration examples, ASP.NET MVC integration examples, BotDetect configuration examples - with both C# and VB.NET and ASPX/Razor variants), as well as equivalents for older .NET framework versions and additional resources are included in the BotDetect setup package which can be downloaded from:

http://captcha.com/captcha-download.html?version=aspnet.


-------------------------------------------------------------------------------
 3. BOTDETECT CAPTCHA DOCUMENTATION
-------------------------------------------------------------------------------

The full index of available BotDetect ASP.NET CAPTCHA documentation can be found at:

http://captcha.com/documentation.html#aspnet_doc


