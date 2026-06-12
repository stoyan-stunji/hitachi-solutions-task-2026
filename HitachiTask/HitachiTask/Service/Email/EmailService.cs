using HitachiTask.Dto.Email;

namespace HitachiTask.Service.Email;

public interface EmailService {
    void SendReport(EmailSettingsDto settingsDto, string report, string subject);
}