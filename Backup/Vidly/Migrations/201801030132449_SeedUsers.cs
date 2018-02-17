namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'96d07efb-c6a6-4e76-be19-39b4dd2ef8b4', N'rainerbuenop29@gmail.com', 0, N'APyLa1bmc+897pTGqrkvR2ova9Rfqh8s5I2dz9ieiL7b//pDTjzy/nnHodaPpJY2Gw==', N'0c91eb6f-f204-4bd0-bf98-6853e42f692b', NULL, 0, 0, NULL, 1, 0, N'rainerbuenop29@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9883caf9-88f6-4234-b1f5-e39b04a4a18c', N'Guest@vidly.com', 0, N'ADI7zn5bXDvqOz4onEJSil4zWz6q+SY6++DQPdgcmlzyFnExRSdNcU0U1KMu1IIb+w==', N'fdc3f461-f02d-4ac5-9d26-eb436b960f3e', NULL, 0, 0, NULL, 1, 0, N'Guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd25cac86-c3a4-4182-9b6e-fe0bd8100206', N'Admin@vidly.com', 0, N'AN+HM2iVUTbgfEEctFGTFoUX2O2n2Tv4MR8S3bc3jW3pHZhv7a7dAYG63CJIPa4D6w==', N'fa061873-572e-47c5-82b4-86504257fa7b', NULL, 0, 0, NULL, 1, 0, N'Admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a63fcab5-1959-45e3-b2d2-f6cfd1b1a176', N'canManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd25cac86-c3a4-4182-9b6e-fe0bd8100206', N'a63fcab5-1959-45e3-b2d2-f6cfd1b1a176')

");
        }
        
        public override void Down()
        {
        }
    }
}
