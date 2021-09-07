namespace LoggingLibriary
{
    public interface ILogger
    {
        void Info(string message);
        
        void Debug(string message);
        
        void Warning(string message);
        
        void Error(string message);
        
        void Fatal(string message);
    }
}
