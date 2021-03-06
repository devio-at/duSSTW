﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using ScrewTurn.Wiki.PluginFramework;
#if AzureStorage
using ScrewTurn.Wiki.Plugins.AzureStorage;
#endif
using ScrewTurn.Wiki.Configuration;

namespace ScrewTurn.Wiki {

	/// <summary>
    /// Allows access to all the ScrewTurn Wiki global settings and configuration options.
	/// </summary>
	public static class GlobalSettings {

		private static List<string> _allKeys;

		private static string version = null;

        private static readonly CacheBase Cache = new CacheBase(new TimeSpan(24, 0, 0));

		/// <summary>
		/// A value indicating whether the public directory can still be overridden.
		/// </summary>
		internal static bool CanOverridePublicDirectory = true;
		private static string _overriddenPublicDirectory = null;

	    private static string _pageExtension = ".ashx";// ""; //

		/// <summary>
		/// Initializes the <see cref="GlobalSettings"/> class.
		/// </summary>
		static GlobalSettings() {
			var properties = typeof(GlobalSettings).GetProperties();
			_allKeys = new List<string>((from p in properties where p.CanRead && p.CanWrite select p.Name).ToList());

			try {
				// try/catch for execution outside web runtime
				if(HttpRuntime.UsingIntegratedPipeline) _pageExtension = "";
			}
			catch { }
		}

		/// <summary>
		/// Gets all global settings names.
		/// </summary>
		public static List<string> AllSettingsNames {
			get { return _allKeys; }
		}

		/// <summary>
		/// Gets the global settings storage provider.
		/// </summary>
		public static IGlobalSettingsStorageProviderV60 Provider {
			get { return Collectors.CollectorsBox.GlobalSettingsProvider; }
		}

		/// <summary>
		/// Gets the Master Password of the given wiki, used to encrypt the Users data.
		/// </summary>
		public static string GetMasterPassword() {
            return (string)Cache.GetCachedItem("MasterPassword", () => SettingsTools.GetString(Provider.GetSetting("MasterPassword"), ""));
		}

        /// <summary>
        /// Gets the Master Password of the given wiki, used to encrypt the Users data.
        /// </summary>
        public static string GetMasterPassword(IGlobalSettingsStorageProviderV60 provider)
        {
            return (string)Cache.GetCachedItem("MasterPassword", () => SettingsTools.GetString(provider.GetSetting("MasterPassword"), ""));
        }

        /// <summary>
        /// Sets the master password for the given wiki, used to encrypt the Users data.
        /// </summary>
        /// <param name="newMasterPassword">The new master password.</param>
        public static void SetMasterPassword(string newMasterPassword) {
			Provider.SetSetting("MasterPassword", newMasterPassword);
            Cache.AddToCache("MasterPassword", newMasterPassword);
		}

		/// <summary>
		/// Gets the bytes of the MasterPassword.
		/// </summary>
		public static byte[] GetMasterPasswordBytes() {
			MD5 md5 = MD5CryptoServiceProvider.Create();
			return md5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(GetMasterPassword()));
		}

		/// <summary>
		/// Gets the name of the Login Cookie.
		/// </summary>
		public static string LoginCookieName {
			get { return "ScrewTurnWikiLogin3"; }
		}

		/// <summary>
		/// Gets the name of the Culture Cookie.
		/// </summary>
		public static string CultureCookieName {
			get { return "ScrewTurnWikiCulture3"; }
		}

		/// <summary>
		/// Gets the version of the Wiki.
		/// </summary>
		public static string WikiVersion {
			get {
				if(version == null) {
					version = typeof(Settings).Assembly.GetName().Version.ToString();
				}

				return version;
			}
		}

		/// <summary>
		/// Overrides the public directory, unless it's too late to do that.
		/// </summary>
		/// <param name="fullPath">The full path.</param>
		internal static void OverridePublicDirectory(string fullPath) {
			if(!CanOverridePublicDirectory) throw new InvalidOperationException("Cannot override public directory - that can only be done during Settings Storage Provider initialization");

			_overriddenPublicDirectory = fullPath;
		}

		/// <summary>
		/// Gets direction of the application
		/// </summary>
		public static string Direction {
			get {
				if(Tools.IsRightToLeftCulture()) return "rtl";
				else return "ltr";
			}
		}

		/// <summary>
		/// Gets the extension used for Pages, including the dot.
		/// </summary>
		//[Obsolete]
		public static string PageExtension {
			get { return _pageExtension; }
		}

		/// <summary>
		/// Gets the display name validation regex.
		/// </summary>
		public static string DisplayNameRegex {
			get { return "^[^\\|\\r\\n]*$"; }
		}

		/// <summary>
		/// Gets the Email validation Regex.
		/// </summary>
		public static string EmailRegex {
			get { return @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})$"; }
		}

		/// <summary>
		/// Gets the WikiTitle validation Regex.
		/// </summary>
		public static string WikiTitleRegex {
			get { return ".+"; }
		}

		/// <summary>
		/// Gets the MainUrl validation Regex.
		/// </summary>
		public static string MainUrlRegex {
			get { return @"^https?\://{1}\S+/$"; }
		}

		/// <summary>
		/// Gets the SMTP Server validation Regex.
		/// </summary>
		public static string SmtpServerRegex {
			get { return @"^[A-Za-z0-9\.\-_]+$"; }
		}

#region Directories and Files

		/// <summary>
		/// Gets the Root Directory of the Wiki.
		/// </summary>
		public static string RootDirectory {
			get { return System.Web.HttpRuntime.AppDomainAppPath; }
		}

		/// <summary>
		/// Gets the Public Directory of the Wiki.
		/// </summary>
		public static string PublicDirectory {
			get {
				if(!string.IsNullOrEmpty(_overriddenPublicDirectory)) return _overriddenPublicDirectory;

				string pubDirName = PublicDirectoryName;
				if(Path.IsPathRooted(pubDirName)) return pubDirName;
				else {
					string path = Path.Combine(RootDirectory, pubDirName);
					if(!path.EndsWith(Path.DirectorySeparatorChar.ToString())) path += Path.DirectorySeparatorChar;
					return path;
				}
			}
		}

		/// <summary>
		/// Gets the Public Directory Name (without the full Path) of the Wiki.
		/// </summary>
		private static string PublicDirectoryName {
			get {
				string dir = ApplicationSettings.Instance.PublicDirectory;
				if(string.IsNullOrEmpty(dir)) throw new InvalidConfigurationException("PublicDirectory cannot be empty or null");
				dir = dir.Trim('\\', '/'); // Remove '/' and '\' from head and tail
				if(string.IsNullOrEmpty(dir)) throw new InvalidConfigurationException("PublicDirectory cannot be empty or null");
				else return dir;
			}
		}

		/// <summary>
		/// Gets the Name of the Themes directory.
		/// </summary>
		public static string ThemesDirectoryName {
			get { return "Content\\Themes"; }
		}

		/// <summary>
		/// Gets the Themes directory.
		/// </summary>
		public static string ThemesDirectory {
			get { return RootDirectory + ThemesDirectoryName + Path.DirectorySeparatorChar; }
		}

		/// <summary>
		/// Gets the Name of the JavaScript Directory.
		/// </summary>
		public static string JsDirectoryName {
			get { return "Scripts"; }
		}



		/// <summary>
		/// Gets the JavaScript Directory.
		/// </summary>
		public static string JsDirectory {
			get { return RootDirectory + JsDirectoryName + Path.DirectorySeparatorChar; }
		}

#endregion

#region Basic Settings and Associated Data

		/// <summary>
		/// Gets or sets the SMTP Server.
		/// </summary>
		public static string SmtpServer {
			get {
                return (string)Cache.GetCachedItem("SmtpServer", () => SettingsTools.GetString(Provider.GetSetting("SmtpServer"), "smtp.server.com"));
			}
			set {
				Provider.SetSetting("SmtpServer", value);
                Cache.AddToCache("SmtpServer", value);
			}
		}

		/// <summary>
		/// Gets or sets the SMTP Server Username.
		/// </summary>
		public static string SmtpUsername {
			get {
                return (string)Cache.GetCachedItem("SmtpUsername", () => SettingsTools.GetString(Provider.GetSetting("SmtpUsername"), ""));
			}
			set {
				Provider.SetSetting("SmtpUsername", value);
                Cache.AddToCache("SmtpUsername", value);
			}
		}

		/// <summary>
		/// Gets or sets the SMTP Server Password.
		/// </summary>
		public static string SmtpPassword {
			get {
                return (string)Cache.GetCachedItem("SmtpPassword", () => SettingsTools.GetString(Provider.GetSetting("SmtpPassword"), ""));
			}
			set {
				Provider.SetSetting("SmtpPassword", value);
                Cache.AddToCache("SmtpPassword", value);
			}
		}

		/// <summary>
		/// Gets or sets the SMTP Server Port.
		/// </summary>
		public static int SmtpPort {
			get {
                return (int)Cache.GetCachedItem("SmtpPort", () => SettingsTools.GetInt(Provider.GetSetting("SmtpPort"), -1));
			}
			set {
				Provider.SetSetting("SmtpPort", value.ToString());
                Cache.AddToCache("SmtpPort", value);
			}
		}

		/// <summary>
		/// Gets or sets a value specifying whether to enable SSL in SMTP.
		/// </summary>
		public static bool SmtpSsl {
			get {
                return (bool)Cache.GetCachedItem("SmtpSsl", () => SettingsTools.GetBool(Provider.GetSetting("SmtpSsl"), false));
			}
			set {
				Provider.SetSetting("SmtpSsl", SettingsTools.PrintBool(value));
                Cache.AddToCache("SmtpSsl", value);
			}
		}

		/// <summary>
		/// Gets or sets the Contact Email.
		/// </summary>
		public static string ContactEmail {
			get {
                return (string)Cache.GetCachedItem("ContactEmail", () => SettingsTools.GetString(Provider.GetSetting("ContactEmail"), "info@server.com"));
			}
			set {
				Provider.SetSetting("ContactEmail", value);
                Cache.AddToCache("ContactEmail", value);
			}
		}

		/// <summary>
		/// Gets or sets the Sender Email.
		/// </summary>
		public static string SenderEmail {
			get {
                return (string)Cache.GetCachedItem("SenderEmail", () => SettingsTools.GetString(Provider.GetSetting("SenderEmail"), "no-reply@server.com"));
			}
			set {
				Provider.SetSetting("SenderEmail", value);
                Cache.AddToCache("SenderEmail", value);
			}
		}

		/// <summary>
		/// Gets or sets the email addresses to send a message to when an error occurs.
		/// </summary>
		public static string[] ErrorsEmails {
			get {
                return ((string)Cache.GetCachedItem("ErrorsEmails", () => SettingsTools.GetString(Provider.GetSetting("ErrorsEmails"), ""))).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			}
			set
			{
			    var val = string.Join("|", value);
                Provider.SetSetting("ErrorsEmails", val);
                Cache.AddToCache("ErrorsEmails", val);
			}
		}

		/// <summary>
		/// Gets the correct path to use with Cookies.
		/// </summary>
		public static string CookiePath {
			get {
				string requestUrl = System.Web.HttpContext.Current.Request.RawUrl;
				string virtualDirectory = System.Web.HttpContext.Current.Request.ApplicationPath;
				// We need to convert the case of the virtual directory to that used in the url
				// Return the virtual directory as is if we can't find it in the URL
				if(requestUrl.ToLowerInvariant().Contains(virtualDirectory.ToLowerInvariant())) {
					return requestUrl.Substring(requestUrl.ToLowerInvariant().IndexOf(virtualDirectory.ToLowerInvariant()), virtualDirectory.Length);
				}
				return virtualDirectory;
			}
		}

		/// <summary>
		/// Gets or sets the Type name of the Default Users Provider.
		/// </summary>
		public static string DefaultUsersProvider {
			get {
                return (string)Cache.GetCachedItem("DefaultUsersProvider", () => SettingsTools.GetString(Provider.GetSetting("DefaultUsersProvider"), null));
			}
			set {
				Provider.SetSetting("DefaultUsersProvider", value);
                Cache.AddToCache("DefaultUsersProvider", value);
			}
		}

		/// <summary>
		/// Gets or sets the Type name of the Default Pages Provider.
		/// </summary>
		public static string DefaultPagesProvider {
			get {
                return (string)Cache.GetCachedItem("DefaultPagesProvider", () => SettingsTools.GetString(Provider.GetSetting("DefaultPagesProvider"), null));
			}
			set {
				Provider.SetSetting("DefaultPagesProvider", value);
                Cache.AddToCache("DefaultPagesProvider", value);
			}
		}

		/// <summary>
		/// Gets or sets the Type name of the Default Files Provider.
		/// </summary>
		public static string DefaultFilesProvider {
			get {
                return (string)Cache.GetCachedItem("DefaultFilesProvider", () => SettingsTools.GetString(Provider.GetSetting("DefaultFilesProvider"), null));
			}
			set {
				Provider.SetSetting("DefaultFilesProvider", value);
                Cache.AddToCache("DefaultFilesProvider", value);
			}
		}

		/// <summary>
		/// Gets or sets the Type name of the Default Themes Provider.
		/// </summary>
		public static string DefaultThemesProvider {
			get {
                return (string)Cache.GetCachedItem("DefaultThemesProvider", () => SettingsTools.GetString(Provider.GetSetting("DefaultThemesProvider"), null));
			}
			set {
				Provider.SetSetting("DefaultThemesProvider", value);
                Cache.AddToCache("DefaultThemesProvider", value);
			}
		}

#endregion

#region Advanced Settings and Associated Data

		/// <summary>
		/// Gets or sets a value indicating whether to disable the Automatic Version Check.
		/// </summary>
		public static bool DisableAutomaticVersionCheck {
			get {
                return (bool)Cache.GetCachedItem("DisableAutomaticVersionCheck", () => SettingsTools.GetBool(Provider.GetSetting("DisableAutomaticVersionCheck"), false));
			}
			set {
				Provider.SetSetting("DisableAutomaticVersionCheck", SettingsTools.PrintBool(value));
                Cache.AddToCache("DisableAutomaticVersionCheck", value);
			}
		}

		/// <summary>
		/// Gets or sets the Max file size for upload (in KB).
		/// </summary>
		public static int MaxFileSize {
			get {
                return (int)Cache.GetCachedItem("MaxFileSize", () => SettingsTools.GetInt(Provider.GetSetting("MaxFileSize"), 10240));
			}
			set {
				Provider.SetSetting("MaxFileSize", value.ToString());
                Cache.AddToCache("MaxFileSize", value);
			}
		}

		/// <summary>
		/// Gets or sets a value specifying whether ViewState compression is enabled or not.
		/// </summary>
		//[Obsolete]
		public static bool EnableViewStateCompression {
			get {
                return (bool)Cache.GetCachedItem("EnableViewStateCompression", () => SettingsTools.GetBool(Provider.GetSetting("EnableViewStateCompression"), false));
			}
			set {
				Provider.SetSetting("EnableViewStateCompression", SettingsTools.PrintBool(value));
                Cache.AddToCache("EnableViewStateCompression", value);
			}
		}

		/// <summary>
		/// Gets or sets a value specifying whether HTTP compression is enabled or not.
		/// </summary>
		public static bool EnableHttpCompression {
			get {
                return (bool)Cache.GetCachedItem("EnableHttpCompression", () => SettingsTools.GetBool(Provider.GetSetting("EnableHttpCompression"), false));
			}
			set {
				Provider.SetSetting("EnableHttpCompression", SettingsTools.PrintBool(value));
                Cache.AddToCache("EnableHttpCompression", value);
			}
		}

		/// <summary>
		/// Gets or sets the Username validation Regex.
		/// </summary>
		public static string UsernameRegex {
			get {
                return (string)Cache.GetCachedItem("UsernameRegex", () => SettingsTools.GetString(Provider.GetSetting("UsernameRegex"), @"^\w[\w\ !$@%^\.\(\)\-_]{3,25}$"));
			}
			set {
				Provider.SetSetting("UsernameRegex", value);
                Cache.AddToCache("UsernameRegex", value);
			}
		}

		/// <summary>
		/// Gets or sets the Password validation Regex.
		/// </summary>
		public static string PasswordRegex {
			get {
                return (string)Cache.GetCachedItem("PasswordRegex", () => SettingsTools.GetString(Provider.GetSetting("PasswordRegex"), @"^\w[\w~!@#$%^\(\)\[\]\{\}\.,=\-_\ ]{5,25}$"));
			}
			set {
				Provider.SetSetting("PasswordRegex", value);
                Cache.AddToCache("PasswordRegex", value);
			}
		}

		/// <summary>
		/// Gets or sets the Logging Level.
		/// </summary>
		public static LoggingLevel LoggingLevel {
			get {
                int value = (int)Cache.GetCachedItem("LoggingLevel", () => SettingsTools.GetInt(Provider.GetSetting("LoggingLevel"), 3));
				return (LoggingLevel)value;
			}
			set {
				Provider.SetSetting("LoggingLevel", ((int)value).ToString());
                Cache.AddToCache("LoggingLevel", value);
			}
		}

		/// <summary>
		/// Gets or sets the Max size of the Log file (KB).
		/// </summary>
		public static int MaxLogSize {
			get {
                return (int)Cache.GetCachedItem("MaxLogSize", () => SettingsTools.GetInt(Provider.GetSetting("MaxLogSize"), 256));
			}
			set {
				Provider.SetSetting("MaxLogSize", value.ToString());
                Cache.AddToCache("MaxLogSize", value);
			}
		}

#endregion

#region CAPTCHA
        
        /// <summary>
        /// Show captcha on LoginPage
        /// </summary>
        public static bool IsRecaptchaEnabled
        {
            get
            {
                return (bool)Cache.GetCachedItem("IsRecaptchaEnabled", () => SettingsTools.GetBool(Provider.GetSetting("IsRecaptchaEnabled"), false));
            }
            set
            {
                Provider.SetSetting("IsRecaptchaEnabled", SettingsTools.PrintBool(value));
                Cache.AddToCache("IsRecaptchaEnabled", value);
            }
        }


        /// <summary>
        /// Show captcha on LoginPage
        /// </summary>
        public static bool ShowCaptchaOnLoginPage
        {
            get
            {
                return (bool)Cache.GetCachedItem("ShowCaptchaOnLoginPage", () => SettingsTools.GetBool(Provider.GetSetting("ShowCaptchaOnLoginPage"), false));
            }
            set
            {
                Provider.SetSetting("ShowCaptchaOnLoginPage", SettingsTools.PrintBool(value));
                Cache.AddToCache("ShowCaptchaOnLoginPage", value);
            }
        }

        /// <summary>
        /// Show captcha on PasswordReset page
        /// </summary>
        public static bool ShowCaptchaOnPasswordResetPage
        {
            get
            {
                return (bool)Cache.GetCachedItem("ShowCaptchaOnPasswordResetPage", () => SettingsTools.GetBool(Provider.GetSetting("ShowCaptchaOnPasswordResetPage"), false));
            }
            set
            {
                Provider.SetSetting("ShowCaptchaOnPasswordResetPage", SettingsTools.PrintBool(value));
                Cache.AddToCache("ShowCaptchaOnPasswordResetPage", value);
            }
        }

        /// <summary>
        /// The private key for the recaptcha service, if enabled. This is optained when you sign up for the free service at https://www.google.com/recaptcha/.
        /// </summary>
        public static string RecaptchaPrivateKey
        {
            get
            {
                return (string)Cache.GetCachedItem("RecaptchaPrivateKey", () => SettingsTools.GetString(Provider.GetSetting("RecaptchaPrivateKey"), null));
            }
            set
            {
                Provider.SetSetting("RecaptchaPrivateKey", value);
                Cache.AddToCache("RecaptchaPrivateKey", value);
            }
        }

        /// <summary>
        /// The public key for the recaptcha service, if enabled. This is optained when you sign up for the free service at https://www.google.com/recaptcha/.
        /// </summary>
        public static string RecaptchaPublicKey
        {
            get
            {
                return (string)Cache.GetCachedItem("RecaptchaPublicKey", () => SettingsTools.GetString(Provider.GetSetting("RecaptchaPublicKey"), null));
            }
            set
            {
                Provider.SetSetting("RecaptchaPublicKey", value);
                Cache.AddToCache("RecaptchaPublicKey", value);
            }
        }

#endregion

    }
}
