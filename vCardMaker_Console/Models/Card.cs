using System;

namespace vCardMaker_Console.Models{
  public class Card
  {
    public string Header { get; set; } = "BEGIN:VCARD\nVERSION:3.0";
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Footer { get; set; } = "END:VCARD";
  }
}
