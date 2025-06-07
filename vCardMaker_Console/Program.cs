using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using vCardMaker_Console.Models;
using vCardMaker_Console.Utility;

namespace vCardMaker_Console {
  public class Program{
    public static void Main(){
      string endLoop = "no";
      do{
        CustomConsole.ConsoleWindow();
        AppUi.MainUi();
        Card card = new Card();
        Dictionary<string, string> cards = new Dictionary<string, string>();
        string name = "";
        string number = "";
        string lastSavedContact = "";
        if (!cards.Any()){
          AppUi.NoDataUi();
        }
        do{
          do{
            if (name.Length > 15){
              AppUi.MainUi();
              AppUi.MaxInputUi();
            }
            AppUi.InputNameUi();
            name = Console.ReadLine()!;
          } while (name.Length > 15);
          if (name.Equals("->")){
            break;
          } else if (name.Equals("<-")){
            if (!cards.Any()){
              AppUi.MainUi();
              AppUi.NoDataUi();
              continue;
            }
            if (cards.Last().Key != null){
              if (cards.Count <= 1){
                AppUi.MainUi();
                lastSavedContact = cards.Last().Key;
                cards.Remove(lastSavedContact);
                AppUi.NoDataUi();
                AppUi.DeletedContactUi(lastSavedContact);
                continue;
              }
              string prevContact = cards.ElementAt(cards.Count - 2).Key;
              AppUi.MainUi();
              AppUi.SavedContactUi(prevContact);
              lastSavedContact = cards.Last().Key;
              cards.Remove(lastSavedContact);
              AppUi.DeletedContactUi(lastSavedContact);
              continue;
            }
            AppUi.MainUi();
            AppUi.NoDataUi();
            continue;
          } else if (name.Equals("<>")){
            AppUi.MainUi();
            Console.WriteLine($"{"Nama",-20}NomorHape");
            foreach (KeyValuePair<string, string> viewCard in cards){
              Console.WriteLine($"{viewCard.Key,-20}{viewCard.Value}");
            }
            Console.ReadLine();
            if (cards.Count() != 0){
              AppUi.MainUi();
              lastSavedContact = cards.Last().Key;
              AppUi.SavedContactUi(lastSavedContact);
              continue;
            } else{
              AppUi.MainUi();
              AppUi.NoDataUi();
              continue;
            }
          }
          do{
            do{
              if (number.Length > 15){
                if (cards.Count != 0){
                  lastSavedContact = cards.Last().Key;
                  AppUi.MainUi();
                  AppUi.SavedContactUi(lastSavedContact);
                  AppUi.MaxInputUi();
                  AppUi.InputNameUi();
                  Console.WriteLine(name);
                } else{
                  AppUi.MainUi();
                  AppUi.NoDataUi();
                  AppUi.MaxInputUi();
                  AppUi.InputNameUi();
                  Console.WriteLine(name);
                }
              }
              AppUi.InputNumberUi();
              number = Console.ReadLine()!;
            } while (number.Length > 15);
            bool match = Regex.IsMatch(number, "^[0-9]");
            if (match){
              cards.Add(name, number);
              lastSavedContact = cards.Last().Key;
              AppUi.MainUi();
              AppUi.SavedContactUi(lastSavedContact);
              break;
            } else if (number.Equals("->")){
              endLoop = "yes";
              break;
            } else if (number.Equals("<-")){
              if (lastSavedContact.Any()){
                lastSavedContact = cards.Last().Key;
                AppUi.MainUi();
                AppUi.SavedContactUi(lastSavedContact);
                break;
              } else{
                AppUi.MainUi();
                AppUi.NoDataUi();
                break;
              }
            } else if (number.Equals("<>")){
              AppUi.MainUi();
              Console.WriteLine($"{"Nama",-20}NomorHape");
              foreach (KeyValuePair<string, string> viewCard in cards){
                Console.WriteLine($"{viewCard.Key,-20}{viewCard.Value}");
              }
              Console.ReadLine();
              if (lastSavedContact.Any()){
                AppUi.MainUi();
                lastSavedContact = cards.Last().Key;
                AppUi.SavedContactUi(lastSavedContact);
                AppUi.InputNameUi();
                Console.WriteLine(name);
                continue;
              } else{
                AppUi.MainUi();
                AppUi.NoDataUi();
                AppUi.InputNameUi();
                Console.WriteLine(name);
              }
            } else{
              AppUi.MainUi();
              if (lastSavedContact.Any()){
                AppUi.SavedContactUi(lastSavedContact);
              } else{
                AppUi.NoDataUi();
              }
              Console.WriteLine("Only number and action input key allowed");
              AppUi.InputNameUi();
              Console.WriteLine(name);
              endLoop = "no";
            }
          } while (endLoop == "no");
        } while (endLoop == "no");
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<string, string> cardLoop in cards){
          stringBuilder.AppendLine(card.Header);
          stringBuilder.AppendLine($"N:{cardLoop.Key};");
          stringBuilder.AppendLine($"TEL;CELL:{cardLoop.Value}");
          stringBuilder.AppendLine(card.Footer);
        }
        using (FileStream fileStream = new FileStream("vCard.vcf", FileMode.Create)){
          byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
          fileStream.Write(bytes);
        }
        Console.WriteLine("vCard successfully generated!");
        Console.Write("Try again? yes[y] or no[n] : ");
        string? tryAgain = Console.ReadLine();
        if (tryAgain!.ToLower() == "y"){
          endLoop = "no";
          continue;
        } else {
          endLoop = "yes"; 
        }
      } while (endLoop == "no");
    }
  }
}
