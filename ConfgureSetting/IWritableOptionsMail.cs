
using Microsoft.Extensions.Options;

namespace SimpleEmailApp.ConfgureSetting
{
    public interface IWritableOptionsMail<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
