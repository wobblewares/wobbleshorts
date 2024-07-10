#nullable enable

using System.Collections.Generic;

namespace WobbleShorts
{
    public struct Short
    {
        public string Title;
        public string Description;
        public IReadOnlyList<string> Tags;
        public ShortConfig.Status Status;
        public Account Author;
        public IReadOnlyList<Requirement> Requirements;
        public IReadOnlyList<Effect> Effects;
        public ShortGraph Graph;
    }
}