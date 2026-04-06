namespace ShelfTrackPro.Domain.Enums;

/// <summary>
/// Roles for authorization. Each role has different permissions:
/// Admin   — full access (CRUD all resources, manage users, approve orders)
/// Manager — create/edit products and orders, cannot delete or manage users
/// Viewer  — read-only access to all resources
/// </summary>

public enum UserRole
{
    Viewer = 0,
    Manager = 1,
    Admin = 2
}