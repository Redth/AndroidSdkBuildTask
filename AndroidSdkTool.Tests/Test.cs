using NUnit.Framework;
using System;
namespace AndroidSdkTool.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void UpdateAndroidM2Repository()
		{
			var sdkHome = "/Users/redth/Library/Developer/Xamarin/android-sdk-mac_x86/";
			var sdkId = "extra-android-m2repository";

			var tool = new Xamarin.AndroidSdkTool(sdkHome);

			tool.Update(sdkId);
		}
	}
}
