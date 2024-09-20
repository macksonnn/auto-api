namespace AutoMais.Integrations
{
    /// <summary>
    /// Used to define an automatic way to register startup classes
    /// </summary>
    public interface IStartupRegister
    {
        ServiceCollection Register(IServiceCollection services);
    }

}
