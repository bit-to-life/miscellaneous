using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BitToLife.Miscellaneous.Database;

/// <summary>
/// EF Core 트랜잭션
/// </summary>
/// <param name="dbContext"></param>
public sealed class EFCoreTransaction(DbContext dbContext)
{
    private readonly DatabaseFacade _database = dbContext.Database;

    /// <summary>
    /// 트랜잭션을 실행합니다.
    /// </summary>
    /// <param name="operation">트랜잭션 안에서 수행할 작업</param>
    /// <param name="cancellationToken"></param>
    public async Task ExecuteAsync(Func<IDbContextTransaction, CancellationToken, Task<bool>> operation, CancellationToken cancellationToken = default)
    {
        await _database.CreateExecutionStrategy().ExecuteAsync(
            async (cancellationToken) =>
            {
                using (IDbContextTransaction transaction = await _database.BeginTransactionAsync(cancellationToken))
                {
                    await operation(transaction, cancellationToken);
                }
            },
            cancellationToken
        );
    }

    /// <summary>
    /// 트랜잭션을 실행합니다.
    /// </summary>
    /// <typeparam name="TResult">반환 타입</typeparam>
    /// <param name="operation">트랜잭션 안에서 수행할 작업</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="TResult"/></returns>
    public async Task<TResult> ExecuteAsync<TResult>(Func<IDbContextTransaction, CancellationToken, Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        return await _database.CreateExecutionStrategy().ExecuteAsync(
            async (cancellationToken) =>
            {
                using (IDbContextTransaction transaction = await _database.BeginTransactionAsync(cancellationToken))
                {
                    TResult result = await operation(transaction, cancellationToken);

                    return result;
                }
            },
            cancellationToken
        );
    }
}
