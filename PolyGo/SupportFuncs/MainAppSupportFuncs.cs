using System;
using System.IO;
using System.Text.RegularExpressions;

using PolyGo.Models;

namespace PolyGo.SupportFuncs
{
	internal static class MainAppSupportFuncs
	{
		public enum AccParams : ushort
		{
			Language,
			Name,
			IsStudent,
			GroupNum,
		}
		public static User ParseAccFile()
		{
			User user = new User();
			string accInfo = File.ReadAllText(Constants.AccountPath).Trim();

			string[] words = accInfo.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

			for(int i = 0; i < words.Length; ++i)
			{
				if (words[i].StartsWith("Language:"))
				{
					user.Language = words[i].Substring(10); // Skip definition (Language:)
				}
				else if (words[i].StartsWith("Name:"))
				{
					user.Name = words[i].Substring(6);
				}
				else if (words[i].StartsWith("IsStudent:"))
				{
					user.IsStudent = words[i].Substring(11).Contains("True");
				}
				else if (words[i].StartsWith("GroupNum:"))
				{
					user.GroupNum = words[i].Substring(10);
				}
			}
			return user;
		}

		public static void ChangeAccParam(string value, AccParams ap, bool isParamInFile)
		{
			if (isParamInFile)
			{
				string accInfo = File.ReadAllText(Constants.AccountPath).Trim();

				string pattern = "";
				switch (ap)
				{
					case AccParams.Language:
						pattern = @"Language:.*.\n?";
						value = "Language: " + value + '\n';
						break;
					case AccParams.Name:
						pattern = @"Name:.*\n?";
						value = "Name: " + value + '\n';
						break;
					case AccParams.IsStudent:
						pattern = @"IsStudent:.*\n?";
						value = "IsStudent: " + value + '\n';
						break;
					case AccParams.GroupNum:
						pattern = @"GroupNum:.*\n?";
						value = "GroupNum: " + value + '\n';
						break;
				}

				accInfo = Regex.Replace(accInfo, pattern, value);

				FileStream fs = File.Create(Constants.AccountPath);
				StreamWriter sw = new StreamWriter(fs);

				sw.WriteLine(accInfo);

				sw.Close();
				fs.Close();
			}
			else
			{
				string accInfo = File.ReadAllText(Constants.AccountPath).Trim();
				switch (ap)
				{
					case AccParams.Language:
						accInfo += "\nLanguage: " + value;
						break;
					case AccParams.Name:
						accInfo += "\nName: " + value;
						break;
					case AccParams.IsStudent:
						accInfo += "\nIsStudent: " + value;
						break;
					case AccParams.GroupNum:
						accInfo += "\nGroupNum: " + value;
						break;
				}

				FileStream fs = File.Create(Constants.AccountPath);
				StreamWriter sw = new StreamWriter(fs);

				sw.WriteLine(accInfo);

				sw.Close();
				fs.Close();
			}
		}
	}
}
