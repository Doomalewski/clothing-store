namespace clothing_store.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(string email);
    }
}
