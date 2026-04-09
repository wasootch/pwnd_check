# pwnd_check

Checks whether a password has appeared in a known data breach using the [Have I Been Pwned](https://haveibeenpwned.com/Passwords) Pwned Passwords API.

Passwords are never sent to the API. Only the first 5 characters of the SHA1 hash are transmitted ([k-anonymity model](https://haveibeenpwned.com/API/v3#SearchingPwnedPasswordsByRange)), and the match is resolved locally.

## Projects

| Project | Type | Description |
|---|---|---|
| `pwnd_check.common` | Class library | Shared `HibpService` — SHA1 hashing and HIBP API calls |
| `pwnd_check` (`pwnd_check.client`) | WinForms app | Desktop UI |
| `pwnd_check.web` | ASP.NET Core web app | Browser UI with async form submission |

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Running

**Desktop (WinForms):**
```
dotnet run --project pwnd_check
```

**Web:**
```
dotnet run --project pwnd_check.web
```
Then open `http://localhost:5000`.

## How it works

1. The password is hashed with SHA1.
2. The first 5 hex characters of the hash are sent to `api.pwnedpasswords.com/range/{prefix}`.
3. The API returns all hash suffixes that match that prefix.
4. The remaining 35 characters are matched locally — the full hash is never transmitted.
5. If a match is found, the number of times it has appeared in known breaches is displayed.

## License

[MIT](LICENSE)
