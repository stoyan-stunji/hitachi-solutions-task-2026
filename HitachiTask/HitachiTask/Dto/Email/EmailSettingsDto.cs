namespace HitachiTask.Dto.Email;

public sealed class EmailSettingsDto {
    public string SenderEmail {
        get; 
        init;
    } = string.Empty;

    public string Password {
        get; 
        init;
    } = string.Empty;

    public string ReceiverEmail {
        get;
        init;
    } = string.Empty;
}