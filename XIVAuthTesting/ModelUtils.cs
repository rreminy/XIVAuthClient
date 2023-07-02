using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XIVAuth.Models;

namespace XIVAuth
{
    public static class ModelUtils
    {
        public static string GetDetailedString(CharacterModel character)
        {
            var lines = new List<string>(15);
            var header = $"{character.Name} <{character.HomeWorld}>";
            lines.Add(new string('=', header.Length));
            lines.Add(header);
            lines.Add(new string('=', header.Length));
            lines.Add($"Data Center: {character.DataCenter}");
            lines.Add($"Lodestone ID: {character.LodestoneId}");
            lines.Add($"Content ID: {character.ContentId}");
            lines.Add($"Avatar URL: {character.AvatarUrl}");
            lines.Add($"Portrait URL: {character.PortraitUrl}");
            lines.Add($"Verified: {(character.Verified ? character.VerifiedAt : "Not verified")}");
            lines.Add($"Verification Key: {character.VerificationKey}");
            lines.Add($"Created At: {character.CreatedAt}");
            lines.Add($"Updated At: {character.UpdatedAt}");
            lines.Add($"Persistent Key: {character.PersistentKey}");
            lines.Add(new string('-', header.Length));
            lines.Add(string.Empty);
            return string.Join('\n', lines);
        }
    }
}
