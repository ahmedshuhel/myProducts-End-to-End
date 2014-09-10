﻿using myProducts.Xamarin.Contracts.Locale;

namespace myProducts.Xamarin.Common.Locale.Languages
{
	public class EnglishTranslation : ITranslation
	{
		public string IsoCode
		{
			get { return "en"; }
		}

		public bool IsDefault
		{
			get { return true; }
		}

		public string UserLogin
		{
			get { return "User Login"; }
		}

		public string UserName
		{
			get { return "User name"; }
		}

		public string Password
		{
			get { return "Password"; }
		}

		public string LogIn
		{
			get { return "Log in"; }
		}

		public string LogInNotPossible
		{
			get { return "Login failed."; }
		}

		public string Articles
		{
			get { return "Articles"; }
		}

		public string Gallery
		{
			get { return "Gallery"; }
		}

		public string Logs
		{
			get { return "Logs"; }
		}

		public string Statistics
		{
			get { return "Statistics"; }
		}

		public string Info
		{
			get { return "Info"; }
		}

		public string Overview
		{
			get { return "Overview"; }
		}

		public string Search
		{
			get { return "Search"; }
		}
	}
}