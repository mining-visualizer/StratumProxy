
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class Utils {

   #region pinvoke

   [DllImport("kernel32.dll", SetLastError = true)]
   public static extern bool AttachConsole(uint dwProcessId);

   [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
   public static extern bool FreeConsole();

   [DllImport("kernel32.dll")]
   public static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
   // Delegate type to be used as the Handler Routine for SCCH
   public delegate Boolean ConsoleCtrlDelegate(CtrlTypes CtrlType);

   // Enumerated type for the control messages sent to the handler routine
   public enum CtrlTypes : uint {
      CTRL_C_EVENT = 0,
      CTRL_BREAK_EVENT,
      CTRL_CLOSE_EVENT,
      CTRL_LOGOFF_EVENT = 5,
      CTRL_SHUTDOWN_EVENT
   }

   [DllImport("kernel32.dll")]
   [return: MarshalAs(UnmanagedType.Bool)]
   public static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

   #endregion pinvoke

   public static string removeHexPrefix(this string value) {
      return value.Substring(value.StartsWith("0x") ? 2 : 0);
   }

   public static bool hasHexPrefix(this string value) {
      return value.StartsWith("0x");
   }

   public static bool isHex(this string value) {
      bool isHex;
      foreach (var c in value.removeHexPrefix()) {
         isHex = ((c >= '0' && c <= '9') ||
                  (c >= 'a' && c <= 'f') ||
                  (c >= 'A' && c <= 'F'));

         if (!isHex)
            return false;
      }
      return true;
   }

   public static bool isEthAddress(this string value) {
      string addr = value.removeHexPrefix();
      return value.hasHexPrefix() && addr.isHex() && addr.Length == 40;
   }

   public static bool equivalentAddress(this string addr1, string addr2) {
      return addr1.isEthAddress() && addr1.ToLower() == addr2.ToLower();
   }

   public static void shutdownNodeProxy(Process proc) {
      if (AttachConsole((uint)proc.Id)) {
         //Disable Ctrl-C handling for our program
         SetConsoleCtrlHandler(null, true);
         GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);

         //Must wait here. If we don't and re-enable Ctrl-C handling below too fast, we might terminate ourselves.
         if (!proc.WaitForExit(2000)) {
            // the Ctrl-C should end the program, but in case it doesn't ...
            proc.Kill();
         }

         FreeConsole();

         //Re-enable Ctrl-C handling or any subsequently started programs will inherit the disabled state.
         SetConsoleCtrlHandler(null, false);
      }

   }
}