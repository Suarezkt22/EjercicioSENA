namespace PruebaTecnicaAmaris.Common.Abstract;

public abstract class ConfigClient(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    protected TValue GetValue<TValue>(string key) {
        return _configuration.GetValue<TValue>(key) ?? throw new KeyNotFoundException($"El valor para la key ${key} no se ha configurado.");
    }
}
