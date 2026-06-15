using System;
using System.Collections.Generic;
using System.Linq;
using XivAuth.Models;

namespace XivAuth.Testing
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
            return string.Join('\n', lines);
        }

        public static string GetDetailedString(UserModel user)
        {
            var socials = user.SocialIdentities;
            var lines = new List<string>(15);
            var header = $"{user.Id}";
            lines.Add(new string('=', header.Length));
            lines.Add(header);
            lines.Add(new string('=', header.Length));
            lines.Add($"Email: {user.Email} (Verified: {ValueOrNullString(user.EmailVerified)})");
            lines.Add($"MFA: {user.MfaEnabled}");
            lines.Add($"Has Verified Characters: {user.VerifiedCharacters}");
            lines.Add($"Socials Count: {ValueOrNullString(socials?.Count())}");
            lines.Add($"Created At: {user.CreatedAt}");
            lines.Add($"Updated At: {user.UpdatedAt}");
            lines.Add(new string('-', header.Length));
            return string.Join('\n', lines);
        }


        public const string NullString = "null";
        public static string ValueOrNullString<T>(T? value)
        {
            if (value is null) return NullString;
            return value.ToString()!;
        }
    }
}
