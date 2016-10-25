using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xamarin
{
	public class AndroidSdkTool
	{
		// /Users/redth/Library/Developer/Xamarin/android-sdk-mac_x86/tools/android
		// extra-android-m2repository
		public AndroidSdkTool(string androidSdkHome)
		{
			AndroidSdkHome = androidSdkHome;
		}

		public string AndroidSdkHome { get; private set; }

		public void Update (string id)
		{
			var process = new Process();
			process.StartInfo.FileName = AndroidPath();
			process.StartInfo.Arguments = "--silent update sdk --force --no-ui --all --filter " + id;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;

			// the events are only raised if this property is set to true
			process.EnableRaisingEvents = true;

			process.OutputDataReceived += (_, e) => {

				Console.WriteLine(e.Data);

				//// eg:  Do you accept the license 'android-sdk-license-c81a61d9'[y / n]:
				//if (Regex.IsMatch(e.Data, "\\[y\\s+?/\\s+n\\]:"))
				//{
				//	process.StandardInput.WriteLine("y");
				//}
			};
			process.ErrorDataReceived += (_, e) => {
				Console.WriteLine(e.Data);
			};

			process.Start();
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();

			// We can just do this right away and it seems to work
			// Accept the license agreement
			process.StandardInput.WriteLine("y");

			process.WaitForExit();
		}

		string AndroidPath()
		{
			var exeName = IsWindows() ? "android.bat" : "android";

			return Path.Combine(AndroidSdkHome, "tools", exeName);
		}

		bool IsWindows()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return true;
				default:
					return false;
			}
		}
	}
}
