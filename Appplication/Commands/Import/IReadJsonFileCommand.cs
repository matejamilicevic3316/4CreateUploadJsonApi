using Microsoft.AspNetCore.Http;

namespace Appplication.Commands.Import
{
    public interface IReadJsonFileCommand<TRes> : IBaseCommand<IFormFile, TRes>
    {
    }
}
