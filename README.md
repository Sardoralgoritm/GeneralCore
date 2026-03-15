# GeneralCore

Personal .NET core library used as a foundation across projects.

## Features

### Pagination
Plug-and-play pagination with automatic sorting via reflection.

```csharp
// Options
var options = new SortFilterPageOptions
{
    PageNumber = 1,
    PageSize = 20,
    SortBy = "title",
    OrderType = "DESC"
};

// Usage
var result = await query.ToPagedResultAsync(options);
// → PagedResult<T> { TotalRows, TotalPages, Rows }
```

### Translation
Typed translation table pattern — no EAV, full type safety.

```csharp
public class NewsTranslation : TranslationBase<int>
{
    public string Title { get; set; } = null!;
}

public class News : ITranslatable<NewsTranslation>
{
    public string Title { get; set; } = null!; // fallback
    public ICollection<NewsTranslation> Translations { get; set; } = [];
}

// AutoMapper
.ForMember(x => x.Title, opt => opt.MapFrom(src =>
    src.Translations.ForLanguage(langId)?.Title ?? src.Title))
```

### Role-based Authorization
Enum-driven permission system — add a field to the enum, it syncs to DB automatically on startup.

```csharp
// 1. Define sub-groups
public enum MySubGroup
{
    [PermissionSubGroup("Users")]
    [Translate(LanguageIdConst.RU, "Пользователи")]
    Users,
}

// 2. Define permissions
[PermissionEnum]
public enum MyPermissionCode
{
    [PermissionCode(MySubGroup.Users, "View")]
    [Translate(LanguageIdConst.RU, "Просмотр")]
    UserView,

    [PermissionCode(MySubGroup.Users, "Create")]
    UserCreate,
}

// 3. Sync on startup (Program.cs)
await context.SyncPermissionsAsync<
    MyPermissionCode,
    PermissionSubGroup, PermissionSubGroupTranslation,
    Permission, PermissionTranslation>(
    linkToSubGroup: (p, sg) => p.SubGroup = sg);

// 4. Protect endpoints
[Authorize(nameof(MyPermissionCode.UserView))]
public IActionResult GetUsers() { ... }
```

### Current User
```csharp
// Inject anywhere
public class MyService(ICurrentUser currentUser) { }
public class MyService(ICurrentUser<int> currentUser) { } // typed Id
```

## Stack
- .NET 10
- EF Core 9
