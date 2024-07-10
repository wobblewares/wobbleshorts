#nullable enable

using QFSW.QC;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace WobbleShorts
{
#region Accounts
    public struct AccountConfigTag : IQcSuggestorTag {}
    public sealed class AccountConfigAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new AccountConfigTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }
    public class AccountConfigSuggestor : ConfigSuggestor<AccountConfig, AccountConfigTag> {}
    
    public struct AccountTag : IQcSuggestorTag {}
    public sealed class AccountAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new AccountTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }

    public class AccountSuggestor : BasicCachedQcSuggestor<Account> 
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options) => context.HasTag<AccountTag>();
        protected override IQcSuggestion ItemToSuggestion(Account account) => new RawSuggestion(account.AccountName, true);
        protected override IEnumerable<Account> GetItems(SuggestionContext context, SuggestorOptions options) => Algorithm.AppState.Accounts.ToArray();
    }
#endregion
    
#region Shorts
    public struct ShortConfigTag : IQcSuggestorTag {}
    public sealed class ShortConfigAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new ShortConfigTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }
    public class ShortConfigSuggestor : ConfigSuggestor<ShortConfig, ShortConfigTag> {}
    
    public struct ShortTag : IQcSuggestorTag {}
    public sealed class ShortAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new ShortTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }
    public class ShortSuggestor : BasicCachedQcSuggestor<Short> 
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options) => context.HasTag<ShortTag>();
        protected override IQcSuggestion ItemToSuggestion(Short @short) => new RawSuggestion(@short.Title, true);
        protected override IEnumerable<Short> GetItems(SuggestionContext context, SuggestorOptions options) => Algorithm.AppState.Feed.ToArray();
    }
    
    public struct FeedShortTag : IQcSuggestorTag {}
    public sealed class FeedShortAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new FeedShortTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }
    public class FeedShortSuggestor : BasicCachedQcSuggestor<Short> 
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options) => context.HasTag<FeedShortTag>();
        protected override IQcSuggestion ItemToSuggestion(Short @short) => new RawSuggestion(@short.Title, true);
        protected override IEnumerable<Short> GetItems(SuggestionContext context, SuggestorOptions options) => Algorithm.AppState.Feed.ToArray();
    }
    
    public struct StateTag : IQcSuggestorTag {}
    public sealed class StateAttribute : SuggestorTagAttribute
    {
        private readonly IQcSuggestorTag[] _tags = { new StateTag() };
        public override IQcSuggestorTag[] GetSuggestorTags() => _tags;
    }
    public class StateSuggestor : BasicCachedQcSuggestor<State> 
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options) => context.HasTag<StateTag>();
        protected override IQcSuggestion ItemToSuggestion(State state) => new RawSuggestion(state.Identifier, true);
        protected override IEnumerable<State> GetItems(SuggestionContext context, SuggestorOptions options)
        {
            List<State> combinedState = new();
            foreach (var account in Algorithm.AppState.Accounts)
                combinedState.AddRange(account.State);
            return combinedState;
        } 
    }
#endregion    
    
    public abstract class ConfigSuggestor<TAsset, TTag> : BasicCachedQcSuggestor<TAsset>
        where TAsset : UnityEngine.Object
        where TTag : IQcSuggestorTag
    {
        protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options) =>
            context.HasTag<TTag>();

        protected override IQcSuggestion ItemToSuggestion(TAsset asset) =>
            new RawSuggestion(asset.name, true);

        // TODO - would be great to be able to supply suggestions here based on the current command.
        protected override IEnumerable<TAsset> GetItems(SuggestionContext context, SuggestorOptions options) {
            return AssetDatabase.FindAssets($"t:{typeof(TAsset).Name}").Select(x => AssetDatabase.LoadAssetAtPath<TAsset>(AssetDatabase.GUIDToAssetPath(x)));
        }
    }
}