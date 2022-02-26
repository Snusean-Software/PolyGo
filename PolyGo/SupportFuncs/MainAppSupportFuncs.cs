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
			//All = Language | Name | IsStudent | GroupNum
		}
		public static User ParseAccFile()
		{
			User user = new User();
			string accInfo = File.ReadAllText(Constants.AccountPath).Trim();

			string[] words = accInfo.Split(new string[] { " ", "\n" }, StringSplitOptions.RemoveEmptyEntries);

			for(int i = 0; i < words.Length; ++i)
			{
				switch (words[i])
				{
					case "Language:":
						++i;
						user.Language = words[i];
						break;
					case "Name:":
						++i;
						user.Name = words[i];
						break;
					case "IsStudent:":
						++i;
						user.IsStudent = words[i].Contains("True");
						break;
					case "GroupNum:":
						++i;
						user.GroupNum = words[i];
						break;
				}
			}
			return user;
		}

		public static void ChangeAccParam(string value, AccParams ap)
		{
			string accInfo = File.ReadAllText(Constants.AccountPath).Trim();

			string pattern = "";
			switch (ap)
			{
				case AccParams.Language:
					pattern = @"Language: .*\n?";
					value = "Language: " + value + "\n";
					break;
				case AccParams.Name:
					pattern = @"Name: .*\n?";
					value = "Name: " + value + "\n";
					break;
				case AccParams.IsStudent:
					pattern = @"IsStudent: .*\n?";
					value = "IsStudent: " + value + "\n";
					break;
				case AccParams.GroupNum:
					pattern = @"GroupNum: .*\n?";
					value = "GroupNum: " + value + "\n";
					break;
			}

			accInfo = Regex.Replace(accInfo, pattern, value);
			FileStream fs = File.Create(Constants.AccountPath);
			StreamWriter sw = new StreamWriter(fs);

			sw.WriteLine(accInfo);

			sw.Close();	
			fs.Close();
		}
	}
}
