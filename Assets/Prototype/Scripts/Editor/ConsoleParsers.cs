#nullable enable

using QFSW.QC;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace WobbleShorts
{
    public class AccountConfigParser : AssetParser<AccountConfig> {}
    public class ClipConfigParser : AssetParser<ShortConfig> {}

    public class AccountParser : BasicQcParser<Account?> {
        public override Account? Parse(string value) {
            return Algorithm.AppState.Accounts.First(x => x.AccountName == value);
        }
    }
    
    public class ClipParser : BasicQcParser<Short?> {
        public override Short? Parse(string value) {
            return Algorithm.AppState.Feed.First(x => x.Title == value);
        }
    }
    
    public class StateParser : BasicQcParser<State?> {
        public override State? Parse(string value)
        {
            List<State> combinedState = new();
            foreach (var account in Algorithm.AppState.Accounts)
                combinedState.AddRange(account.State);
            return combinedState.First(x => x.Identifier == value);
        }
    }
    
    public abstract class AssetParser<T> : BasicQcParser<T> where T : UnityEngine.Object {
        public override T? Parse(string value)
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}")
                .Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x)))
                .FirstOrDefault(x => x.name == value);
        }
    }
}