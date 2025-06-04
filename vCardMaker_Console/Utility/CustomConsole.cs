using System;

namespace vCardMaker_Console.Utility{
  public static class CustomConsole{
    public static void ConsoleWindow(){
      Console.WindowWidth = 60;
      Console.Title = "vCardMaker";
    }
  }
}