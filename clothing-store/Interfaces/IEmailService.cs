namespace clothing_store.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(string email);
        Task SendForgotPasswordEmail(string name, string url);
    }
}
