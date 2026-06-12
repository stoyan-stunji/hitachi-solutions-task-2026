using HitachiTask.PathFinding;
using HitachiTask.Service.Email;

namespace HitachiTask;

internal class Program {
    static void Main() {
        SSSPStrategy strategy = new DijkstraStrategy();

        EmailMissionReportBuilder reportBuilder = new EmailMissionReportBuilder();
        SmtpEmailService smtpEmailService = new SmtpEmailService();
        EmailMissionResultsSender sender = new EmailMissionResultsSender(reportBuilder, smtpEmailService);
        EmailMissionWorkflowService workflow = new EmailMissionWorkflowService(sender);
        
        SpaceNavigationApp app = new SpaceNavigationApp(strategy, workflow);
        app.Run();
    }
}