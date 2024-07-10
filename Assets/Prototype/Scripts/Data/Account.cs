#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace WobbleShorts
{
    public struct Account
    {
        public AccountConfig Config;
        public string AccountName;
        public string Description;
        public IReadOnlyList<State> State;

        public State? GetState(StateConfig config)
        {
            foreach (var state in State)
            {
                if (state.Identifier == config.Identifier)
                    return state;
            }
            return null;
        }
    }
}