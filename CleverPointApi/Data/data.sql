USE [CleverPointApiTest]
GO
SET IDENTITY_INSERT [dbo].[Status] ON 
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [IsFirstStatusOfTicket]) VALUES (1, N'Open', NULL, 1)
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [IsFirstStatusOfTicket]) VALUES (2, N'In Progress', NULL, 0)
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [IsFirstStatusOfTicket]) VALUES (3, N'Under Review', NULL, 0)
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [IsFirstStatusOfTicket]) VALUES (4, N'In Testing', NULL, 0)
GO
INSERT [dbo].[Status] ([Id], [Name], [Description], [IsFirstStatusOfTicket]) VALUES (5, N'Done', NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Username], [Name], [Lastname]) VALUES (1, N'KNikos', N'Nikos', N'Koulouris')
GO
INSERT [dbo].[Users] ([Id], [Username], [Name], [Lastname]) VALUES (2, N'AStelios', N'Stelios', N'Agoropoulos')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 
GO
INSERT [dbo].[Tickets] ([Id], [Title], [Description], [ShipmentID], [ShipmentTrackingNumber], [EstimatedStoryPoints], [SpentStoryPoints], [DateCreated], [DateClosed], [StatusId], [CreatorUserId], [AssigneeUserId]) VALUES (1, N'Create a new Repo', N'Create a new repo for an Angular application in angular 14', N'000000', N'000000001', 2, 1, CAST(N'2024-07-13T14:53:27.4220000' AS DateTime2), CAST(N'2024-07-13T14:53:27.4220000' AS DateTime2), 1, 1, 2)
GO
INSERT [dbo].[Tickets] ([Id], [Title], [Description], [ShipmentID], [ShipmentTrackingNumber], [EstimatedStoryPoints], [SpentStoryPoints], [DateCreated], [DateClosed], [StatusId], [CreatorUserId], [AssigneeUserId]) VALUES (2, N'Create the Repo', N'Create the repo for the new Api solution', N'000000', N'000000001', 1, 1, CAST(N'2024-07-13T15:31:14.8870000' AS DateTime2), CAST(N'2024-07-13T16:31:14.8870000' AS DateTime2), 5, 2, 1)
GO
INSERT [dbo].[Tickets] ([Id], [Title], [Description], [ShipmentID], [ShipmentTrackingNumber], [EstimatedStoryPoints], [SpentStoryPoints], [DateCreated], [DateClosed], [StatusId], [CreatorUserId], [AssigneeUserId]) VALUES (3, N'Create a new API', N'Create the API for the CleverPoint case study', N'000000', N'000000001', 2, 2, CAST(N'2024-07-13T16:31:14.8870000' AS DateTime2), CAST(N'2024-07-14T17:31:14.8870000' AS DateTime2), 2, 2, 1)
GO
INSERT [dbo].[Tickets] ([Id], [Title], [Description], [ShipmentID], [ShipmentTrackingNumber], [EstimatedStoryPoints], [SpentStoryPoints], [DateCreated], [DateClosed], [StatusId], [CreatorUserId], [AssigneeUserId]) VALUES (4, N'Models and Database', N'Create the models and build the database', N'000000', N'000000001', 2, NULL, CAST(N'2024-07-13T16:31:14.8870000' AS DateTime2), NULL, 1, 2, 1)
GO
INSERT [dbo].[Tickets] ([Id], [Title], [Description], [ShipmentID], [ShipmentTrackingNumber], [EstimatedStoryPoints], [SpentStoryPoints], [DateCreated], [DateClosed], [StatusId], [CreatorUserId], [AssigneeUserId]) VALUES (5, N'Controller', N'Create the Users and Tickets controllers', N'000000', N'000000001', 3, NULL, CAST(N'2024-07-13T16:31:14.8870000' AS DateTime2), NULL, 1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240711092103_database-creation', N'8.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240711092134_database-creation-2', N'8.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240713144824_stuff', N'8.0.7')
GO
