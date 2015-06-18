using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qujck.Core
{
    public interface IStrategyHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface IStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
