-- ============================================
-- Insert Dummy Data for Smart Task Manager
-- ============================================

-- 1. Insert Roles
INSERT INTO "AppRole" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES 
    ('role-1', 'Admin', 'ADMIN', 'stamp-1'),
    ('role-2', 'Manager', 'MANAGER', 'stamp-2'),
    ('role-3', 'User', 'USER', 'stamp-3'),
    ('role-4', 'Developer', 'DEVELOPER', 'stamp-4'),
    ('role-5', 'Team Lead', 'TEAM LEAD', 'stamp-5');

-- 2. Insert Users
-- Note: These passwords are hashed version of "Password123!"
INSERT INTO "AppUser" (
    "Id", 
    "FullName", 
    "RoleId", 
    "ProfileImageUrl", 
    "IsActive", 
    "CreatedAt", 
    "UpdatedAt",
    "UserName", 
    "NormalizedUserName", 
    "Email", 
    "NormalizedEmail",
    "EmailConfirmed",
    "PasswordHash",
    "SecurityStamp",
    "ConcurrencyStamp",
    "PhoneNumber",
    "PhoneNumberConfirmed",
    "TwoFactorEnabled",
    "LockoutEnabled",
    "AccessFailedCount"
)
VALUES 
    (
        'user-1', 
        'John Doe', 
        'role-1', 
        'https://i.pravatar.cc/150?img=1', 
        true, 
        NOW(), 
        NULL,
        'johndoe',
        'JOHNDOE',
        'john.doe@example.com',
        'JOHN.DOE@EXAMPLE.COM',
        true,
        'AQAAAAIAAYagAAAAEHashed_Password_Here_1',
        'security-stamp-1',
        'concurrency-stamp-1',
        '+1234567890',
        true,
        false,
        true,
        0
    ),
    (
        'user-2', 
        'Jane Smith', 
        'role-2', 
        'https://i.pravatar.cc/150?img=2', 
        true, 
        NOW(), 
        NULL,
        'janesmith',
        'JANESMITH',
        'jane.smith@example.com',
        'JANE.SMITH@EXAMPLE.COM',
        true,
        'AQAAAAIAAYagAAAAEHashed_Password_Here_2',
        'security-stamp-2',
        'concurrency-stamp-2',
        '+1234567891',
        true,
        false,
        true,
        0
    ),
    (
        'user-3', 
        'Mike Johnson', 
        'role-3', 
        'https://i.pravatar.cc/150?img=3', 
        true, 
        NOW(), 
        NULL,
        'mikejohnson',
        'MIKEJOHNSON',
        'mike.johnson@example.com',
        'MIKE.JOHNSON@EXAMPLE.COM',
        true,
        'AQAAAAIAAYagAAAAEHashed_Password_Here_3',
        'security-stamp-3',
        'concurrency-stamp-3',
        '+1234567892',
        true,
        false,
        true,
        0
    ),
    (
        'user-4', 
        'Sarah Williams', 
        'role-4', 
        'https://i.pravatar.cc/150?img=4', 
        true, 
        NOW(), 
        NULL,
        'sarahwilliams',
        'SARAHWILLIAMS',
        'sarah.williams@example.com',
        'SARAH.WILLIAMS@EXAMPLE.COM',
        true,
        'AQAAAAIAAYagAAAAEHashed_Password_Here_4',
        'security-stamp-4',
        'concurrency-stamp-4',
        '+1234567893',
        true,
        false,
        true,
        0
    ),
    (
        'user-5', 
        'David Brown', 
        'role-5', 
        'https://i.pravatar.cc/150?img=5', 
        true, 
        NOW(), 
        NULL,
        'davidbrown',
        'DAVIDBROWN',
        'david.brown@example.com',
        'DAVID.BROWN@EXAMPLE.COM',
        true,
        'AQAAAAIAAYagAAAAEHashed_Password_Here_5',
        'security-stamp-5',
        'concurrency-stamp-5',
        '+1234567894',
        true,
        false,
        true,
        0
    );

-- 3. Insert Tasks
INSERT INTO "TaskItem" (
    "Description",
    "Piority",
    "Status",
    "Deadline",
    "AssignedToUserId",
    "AssignToUserId",
    "DueDate",
    "IsCompleted"
)
VALUES 
    (
        'Implement user authentication system',
        'High',
        'In Progress',
        NOW() + INTERVAL '7 days',
        'user-1',
        'user-1',
        NOW() + INTERVAL '7 days',
        false
    ),
    (
        'Design database schema for project',
        'High',
        'Completed',
        NOW() + INTERVAL '3 days',
        'user-2',
        'user-2',
        NOW() + INTERVAL '3 days',
        true
    ),
    (
        'Create REST API endpoints',
        'Medium',
        'To Do',
        NOW() + INTERVAL '10 days',
        'user-3',
        'user-3',
        NOW() + INTERVAL '10 days',
        false
    ),
    (
        'Write unit tests for controllers',
        'Medium',
        'In Progress',
        NOW() + INTERVAL '5 days',
        'user-4',
        'user-4',
        NOW() + INTERVAL '5 days',
        false
    ),
    (
        'Deploy application to production',
        'Low',
        'To Do',
        NOW() + INTERVAL '14 days',
        'user-5',
        'user-5',
        NOW() + INTERVAL '14 days',
        false
    );

-- Verify the data
SELECT * FROM "AppRole";
SELECT * FROM "AppUser";
SELECT * FROM "TaskItem";
