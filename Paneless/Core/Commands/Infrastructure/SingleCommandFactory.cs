namespace Paneless.Core.Commands
{
    public class SingleCommandFactory<T> : ICommandFactory where T: ICommand, new()
    {
        public ICommand CreateCommand()
        {
            return new T();
        }
    }
}
