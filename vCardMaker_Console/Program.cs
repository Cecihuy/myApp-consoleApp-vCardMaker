using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using vCardMaker_Console.Models;

namespace vCardMaker_Console {
  public class Program {
    public static void Main() {
      string name = "";
      string number = "";
      Card card = new Card();
      Dictionary<string, string> cards = new Dictionary<string, string>();
      do {
        Console.WriteLine("input name :");
        name = Console.ReadLine()!;
        if (name.Equals("->")){
          continue;
        }
        Console.WriteLine("input number :");
        number = Console.ReadLine()!;
        if(!number.Equals("->")){
          cards.Add(name, number);
        }
      } while (!name.Equals("->") && !number.Equals("->"));

      StringBuilder stringBuilder = new StringBuilder();
      foreach(KeyValuePair<string,string> cardLoop in cards){
        stringBuilder.AppendLine(card.Header);
        stringBuilder.AppendLine($"N:{cardLoop.Key};");
        stringBuilder.AppendLine($"TEL;CELL:{cardLoop.Value}");
        stringBuilder.AppendLine(card.Footer);
      }
      using (FileStream fileStream = new FileStream("test.vcf", FileMode.Create)){
        byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        fileStream.Write(bytes);
      }
    }
  }
}
