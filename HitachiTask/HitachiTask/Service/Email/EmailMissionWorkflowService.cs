using System.Net.Mail;
using HitachiTask.DataStructures;
using HitachiTask.Dto.Email;
using HitachiTask.Dto.Mission;

namespace HitachiTask.Service.Email;

public sealed class EmailMissionWorkflowService(EmailMissionResultsSender sender) {
    public void Run(Grid grid, List<MissionResultDto> results) {
        Console.Write("Send report by email? (y/n): ");
        string answer = Console.ReadLine()!;

        if (answer.ToLower() != "y") {
            return;
        }

        EmailDto emailDto = ReadEmailInput();
        try {
            sender.SendMissionResults(grid, results, emailDto);
            Console.WriteLine("Report sent.");
        }
        catch (SmtpException ex) {
            Console.WriteLine($"SMTP error: {ex.Message}");
        }
        catch (Exception ex) {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static EmailDto ReadEmailInput() {
        Console.Write("Sender email: ");
        string sender = Console.ReadLine()!;

        Console.Write("Password: ");
        string password = Console.ReadLine()!;

        Console.Write("Receiver email: ");
        string receiver = Console.ReadLine()!;

        EmailSettingsDto settingsDto = new() { SenderEmail = sender, Password = password, ReceiverEmail = receiver };
        return new EmailDto(settingsDto, "Space Mission Results");
    }
}