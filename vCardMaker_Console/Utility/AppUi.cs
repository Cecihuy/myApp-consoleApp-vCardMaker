using System;

namespace vCardMaker_Console.Utility{
  public static class AppUi {
    public static void MainUi() {
      Console.Clear();
      Console.WriteLine("Welcome to vCardMaker Application");
      Console.WriteLine("input \"<-\" key for step back or delete last contact");
      Console.WriteLine("input \"<>\" key for preview all contacts");
      Console.WriteLine("input \"->\" key for generating vCard");
      Console.WriteLine("-----------------------------------------------------");
    }
    public static void NoDataUi() {
      Console.WriteLine("No data at the moment");
    }
    public static void InputNameUi() {
      Console.Write("Input Name : ");
    }
    public static void InputNumberUi() {
      Console.Write("Input Number : ");
    }
    public static void DeletedContactUi(string name) {
      Console.WriteLine($"Contact with name \"{name}\" deleted");
    }
    public static void SavedContactUi(string name) {
      Console.WriteLine($"Last saved contact is \"{name}\"");
    }
    public static void MaxInputUi(){
      Console.WriteLine("Maximum input length is 15 digit");
    }
  }
}