using System;
using System.IO;

using PolyGo.Models;

namespace PolyGo.SupportFuncs
{
	internal class MainAppSupportFuncs
	{
		public static void ParseAccFile(string accPath, ref User user)
		{
			string accInfo = File.ReadAllText(accPath);
			string[] separator = { " ", "\n" };
			string[] words = accInfo.Split(separator, StringSplitOptions.RemoveEmptyEntries);
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
		}
	}
}
