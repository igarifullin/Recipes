using MediatR;

namespace Recipe.Services.Queries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}