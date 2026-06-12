using HitachiTask.DataStructures;
using HitachiTask.Dto.Email;
using HitachiTask.Dto.Mission;

namespace HitachiTask.Service.Email;

public sealed class EmailMissionResultsSender(EmailMissionReportBuilder reportBuilder, EmailService emailService) {
    public void SendMissionResults(Grid grid, List<MissionResultDto> results, EmailDto emailDto) {
        string report = reportBuilder.Build(grid, results);
        EmailSettingsDto emailSettingsDto = emailDto.EmailSettingsDto;
        string subject = emailDto.subject;
        emailService.SendReport(emailSettingsDto, report, subject);
    }
}