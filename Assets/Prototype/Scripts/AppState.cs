#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WobbleShorts
{
    public class AppState
    {
        private List<Account> _accounts = new();
        public IReadOnlyList<Account> Accounts => _accounts;
        
        private List<Short> _feed = new();
        public IReadOnlyList<Short> Feed => _feed;

        public Action<Short> OnClipShown;
        
        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public void Post(Short @short)
        {
            @short.Status = ShortConfig.Status.Published;
            _feed.Add(@short);
            Debug.Log($"Added {@short.Title} by {@short.Author.AccountName} to feed.");
        }

        public void Show(Short @short)
        {
            if (@short.Graph == null)
            {
                Debug.LogError($"Short {@short.Title} has no graph.");
                return;
            }
            
            // Run the graph
            GameObject.FindObjectOfType<GraphExecutor>().ExecuteGraph(@short.Graph);
            
            // Invoke event to show the clip.
            OnClipShown?.Invoke(@short);
        }
        
        public void Watch(Short @short)
        {
            @short.Status = ShortConfig.Status.Watched;
            _feed.Remove(@short);

            Debug.Log($"Watching clip {@short.Title}");
            @short.Status = ShortConfig.Status.Watched;
            
            var effects = @short.Effects
                .Where(x => x.Type == EffectConfig.EffectType.Watch || x.Type == EffectConfig.EffectType.Both).ToList();
            if (effects.Any())
                ApplyEffects(effects);
        }

        public void Skip(Short @short)
        {
            @short.Status = ShortConfig.Status.Skipped;
            _feed.Remove(@short);
            
            Debug.Log($"Skipping clip {@short.Title}");
            @short.Status = ShortConfig.Status.Skipped;
            
            var effects = @short.Effects
                .Where(x => x.Type == EffectConfig.EffectType.Skip || x.Type == EffectConfig.EffectType.Both).ToList();
            if (effects.Any())
                ApplyEffects(effects);
        }

        public Account? GetAccount(AccountConfig config)
        {
            foreach (var account in Accounts)
            {
                if (account.AccountName == config.AccountName)
                    return account;
            }
            return null;
        }

        private void ApplyEffects(IReadOnlyList<Effect> effects)
        {
            var effectStrings = effects.Select(x => $"{x.Account.AccountName}|{x.State.Identifier} = {x.Value}");
            Debug.Log($"Applying effects:\n{string.Join(",", effectStrings)}");

            foreach (var effect in effects)
            {
                var state = effect.Account.State.First(x => x.Identifier == effect.State.Identifier);
                if (state == null)
                    Debug.LogWarning($"Unable to apply effect {effect.State.Identifier}.");
                else
                    state.Value = effect.Value;
            }
        }
    }
}