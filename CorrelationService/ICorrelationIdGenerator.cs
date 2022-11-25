namespace SimpleEmailApp.CorrelationService
{
    public interface ICorrelationIdGenerator
    {
        string Get();
        void Set(string correlationId);
    }
}
