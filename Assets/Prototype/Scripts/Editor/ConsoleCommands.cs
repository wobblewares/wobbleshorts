#nullable enable

using QFSW.QC;
using UnityEngine;

namespace WobbleShorts
{
    public static class ConsoleCommands
    {
        private static AppState App => Algorithm.AppState;
        
        [Command("accounts")]
        public static void ShowAccounts()
        {
            for (int i = 0; i < App.Accounts.Count; i++)
                Debug.Log($"[{i}] {App.Accounts[i].AccountName}");
        }
        
        [Command("feed")]
        public static void ShowFeed()
        {
            for (int i = 0; i < App.Feed.Count; i++)
                Debug.Log($"[{i}] {App.Feed[i].Title} by {App.Feed[i].Author.AccountName}");
        }

        [Command("show")]
        public static void ShowShort([FeedShort] Short? @short)
        {
            if (@short == null)
                return;
            
            App.Show(@short.Value);
        }

        [Command("watch")]
        public static void Watch([FeedShort] Short? @short)
        {
            if (@short == null)
                return;
            
            App.Watch(@short.Value);
        }
        
        [Command("skip")]
        public static void Skip([FeedShort] Short? @short)
        {
            if (@short == null)
                return;
            
            App.Skip(@short.Value);
        }

        [Command("account")]
        public static void ShowAccountDetails([Account] Account? account)
        {
            if (account != null)
                Debug.Log(account.Value.Description);
        }
        
        [Command("short")]
        public static void ShowShortDetails([Short] Short? @short)
        {
            if (@short is {} c)
            {
                var title = $"<b>{c.Title} ({c.Status})</b>";
                var description = $"{c.Description}";
                var tags = string.Join(" #", c.Tags);
                Debug.Log($"{title}\n{description}\n#{tags}");
            }
        }

        [Command("state")]
        public static void ShowStateDetails([State] State? state)
        {
            if (state is {} s)
                Debug.Log($"State {s.Identifier} = {s.Value}");
        }

        [Command("postShort")]
        public static void PostShort([Account] Account? account, [ShortConfig] ShortConfig? config)
        {
            if (account != null && config != null) {
                var @short = config.Create(account.Value);
                App.Post(@short);
            }
        }

        [Command("post")]
        public static void Post([Account] Account? account)
        {
            if (account == null)
            {
                Debug.LogError("Account is null.");
                return;
            }
            
            // Sample a random short
            var config = account.Value.Config;
            int index = config.ShortConfigs.Count > 0 ? Random.Range(0, config.ShortConfigs.Count) : -1;

            if (index == -1)
            { 
                Debug.Log($"No available shorts for {account.Value.AccountName}");
                return;
            }
            
            var clip = config.ShortConfigs[index].Create(account.Value);
            App.Post(clip);
        }
    }
}