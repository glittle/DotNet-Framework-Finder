using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace ConsoleApplication1
{
  /// <summary>
  /// Simple program to report on what is found in the registry.
  /// Built for Sunwapta Solutions Inc.
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine();

      GetFromRegistry();

      Console.WriteLine();
      Console.WriteLine("Press a key to continue");
      Console.ReadKey();
    }

    private static void GetFromRegistry()
    {
      using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
      {
        if (ndpKey?.GetValue("Release") != null)
        {
          Console.WriteLine("Version: " + CheckForDot45Version((int)ndpKey.GetValue("Release")));
        }
        else {
          Console.WriteLine("Version 4.5 or later is not detected.");
        }
      }
    }

    // Checking the version using >= will enable forward compatibility, 
    // however you should always compile your code on newer versions of
    // the framework to ensure your app works the same.
    private static string CheckForDot45Version(int releaseKey)
    {
      var knownVersions = VersionNames();

      if (knownVersions.ContainsKey(releaseKey))
      {
        return knownVersions[releaseKey];
      }

      if (releaseKey >= 394254)
      {
        return "4.6.1 or later";
      }
      if (releaseKey >= 393295)
      {
        return "4.6 or later";
      }
      if ((releaseKey >= 379893))
      {
        return "4.5.2 or later";
      }
      if ((releaseKey >= 378675))
      {
        return "4.5.1 or later";
      }
      if ((releaseKey >= 378389))
      {
        return "4.5 or later";
      }
      // This line should never execute. A non-null release key should mean
      // that 4.5 or later is installed.
      return "No 4.5 or later version detected";
    }

    /// <summary>
    /// See https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx#net_d
    /// </summary>
    /// <returns></returns>
    static Dictionary<int, string> VersionNames()
    {
      return new Dictionary<int, string>
      {
        {378389, ".NET Framework 4.5"},
        {378675, ".NET Framework 4.5.1 installed with Windows 8.1"},
        {378758, ".NET Framework 4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2"},
        {379893, ".NET Framework 4.5.2"},
        {393295, ".NET Framework 4.6 installed with Windows 10"},
        {393297, ".NET Framework 4.6 installed (not Windows 10)"},
        {394254, ".NET Framework 4.6.1 installed on Windows 10"},
        {394271, ".NET Framework 4.6.1 installed (not Windows 10)"}
      };
    }
  }
}