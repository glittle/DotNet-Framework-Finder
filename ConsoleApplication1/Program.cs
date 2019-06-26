using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace ConsoleApplication1
{
  /// <summary>
  ///   Simple program to report on what is found in the registry.
  ///   Built for Sunwapta Solutions Inc.
  /// </summary>
  internal class Program
  {
    private static void Main(string[] args)
    {
      Console.WriteLine("Version Finder " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
      Console.WriteLine();
      Console.WriteLine();

      GetFromRegistry();

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("Press a key to exit");
      Console.ReadKey();
    }

    private static void GetFromRegistry()
    {
      using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
        .OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
      {
        if (ndpKey?.GetValue("Release") != null)
        {
          Console.WriteLine("Installed version of .NET Framework: " + CheckForDot45Version((int)ndpKey.GetValue("Release")));
        }
        else
        {
          // should never be possible...
          Console.WriteLine("Version 4.5 or later is not detected.");
        }
      }
    }

    /// <summary>
    ///   See https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx#net_d
    /// </summary>
    /// <returns></returns>
    private static string CheckForDot45Version(int releaseKey)
    {
      if (releaseKey >= 528040)
      {
        return "4.8 or later";
      }

      if (releaseKey >= 461808)
      {
        return "4.7.2";
      }

      if (releaseKey >= 461308)
      {
        return "4.7.1";
      }

      if (releaseKey >= 460798)
      {
        return "4.7";
      }

      if (releaseKey >= 394802)
      {
        return "4.6.2";
      }

      if (releaseKey >= 394254)
      {
        return "4.6.1";
      }

      if (releaseKey >= 393295)
      {
        return "4.6";
      }

      if (releaseKey >= 379893)
      {
        return "4.5.2";
      }

      if (releaseKey >= 378675)
      {
        return "4.5.1";
      }

      if (releaseKey >= 378389)
      {
        return "4.5";
      }

      // This line should never execute. A non-null release key should mean
      // that 4.5 or later is installed.
      return "No 4.5 or later version detected";
    }
  }
}