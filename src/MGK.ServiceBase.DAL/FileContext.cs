namespace MGK.ServiceBase.DAL;

public abstract class FileContext : IFileContext
{
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract void Dispose(bool disposing);

    protected abstract void OnModelCreating();
}
