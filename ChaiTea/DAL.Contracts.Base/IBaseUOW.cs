namespace DAL.Contracts.Base;

public interface IBaseUow
{
    Task<int> SaveChangesAsync();
}