namespace Api_Catalogo_Livros.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning; //define o nível dos logs
        public int EventId { get; set; } = 0;//define o id dos logs
    }
}
