using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEase.Migrations
{
    /// <inheritdoc />
    public partial class SeedSouthAfricanTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[EventTypes] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[EventTypes] WHERE [EventTypeId] = 10) INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name]) VALUES (10, N'Sports');
IF NOT EXISTS (SELECT 1 FROM [dbo].[EventTypes] WHERE [EventTypeId] = 11) INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name]) VALUES (11, N'Festival');
IF NOT EXISTS (SELECT 1 FROM [dbo].[EventTypes] WHERE [EventTypeId] = 12) INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name]) VALUES (12, N'Theatre');
IF NOT EXISTS (SELECT 1 FROM [dbo].[EventTypes] WHERE [EventTypeId] = 13) INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name]) VALUES (13, N'Charity');
IF NOT EXISTS (SELECT 1 FROM [dbo].[EventTypes] WHERE [EventTypeId] = 14) INSERT INTO [dbo].[EventTypes] ([EventTypeId], [Name]) VALUES (14, N'Awards');
SET IDENTITY_INSERT [dbo].[EventTypes] OFF;

SET IDENTITY_INSERT [dbo].[Venues] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9101) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9101, N'Cape Town International Convention Centre', N'Convention Square, 1 Lower Long Street, Cape Town, Western Cape, South Africa', 5000, N'https://images.unsplash.com/photo-1492684223066-81342ee5ff30?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9102) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9102, N'Sandton Convention Centre', N'161 Maude Street, Sandown, Sandton, Gauteng, South Africa', 4500, N'https://images.unsplash.com/photo-1511578314322-379afb476865?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9103) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9103, N'Durban International Convention Centre', N'45 Bram Fischer Road, Durban Central, KwaZulu-Natal, South Africa', 6000, N'https://images.unsplash.com/photo-1505373877841-8d25f7d46678?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9104) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9104, N'Sun City Superbowl', N'Sun City Resort, Rustenburg, North West, South Africa', 6000, N'https://images.unsplash.com/photo-1540575467063-178a50c2df87?auto=format&fit=crop&w=1200&q=80', 0);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9105) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9105, N'Kirstenbosch Botanical Gardens', N'Rhodes Drive, Newlands, Cape Town, Western Cape, South Africa', 1200, N'https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9106) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9106, N'Nelson Mandela Bay Stadium', N'70 Prince Alfred Road, North End, Gqeberha, Eastern Cape, South Africa', 46000, N'https://images.unsplash.com/photo-1522778119026-d647f0596c20?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9107) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9107, N'Loftus Versfeld Stadium', N'Kirkness Street, Arcadia, Pretoria, Gauteng, South Africa', 51000, N'https://images.unsplash.com/photo-1431324155629-1a6deb1dec8d?auto=format&fit=crop&w=1200&q=80', 0);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9108) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9108, N'Constitution Hill Human Rights Precinct', N'11 Kotze Street, Braamfontein, Johannesburg, Gauteng, South Africa', 800, N'https://images.unsplash.com/photo-1517457373958-b7bdd4587205?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9109) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9109, N'Artscape Theatre Centre', N'DF Malan Street, Foreshore, Cape Town, Western Cape, South Africa', 1500, N'https://images.unsplash.com/photo-1503095396549-807759245b35?auto=format&fit=crop&w=1200&q=80', 1);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Venues] WHERE [VenueId] = 9110) INSERT INTO [dbo].[Venues] ([VenueId], [Name], [Location], [Capacity], [ImageUrl], [IsAvailable]) VALUES (9110, N'Gallagher Convention Centre', N'19 Richards Drive, Midrand, Gauteng, South Africa', 7000, N'https://images.unsplash.com/photo-1556761175-b413da4baf72?auto=format&fit=crop&w=1200&q=80', 1);
SET IDENTITY_INSERT [dbo].[Venues] OFF;

SET IDENTITY_INSERT [dbo].[Users] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [UserId] = 9401) INSERT INTO [dbo].[Users] ([UserId], [Email], [Password], [Role]) VALUES (9401, N'thando.mokoena@eventease.test', N'$2a$11$TestSeedOnlyPlaceholderHashForBookingSpecialist01', N'BookingSpecialist');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [UserId] = 9402) INSERT INTO [dbo].[Users] ([UserId], [Email], [Password], [Role]) VALUES (9402, N'aisha.naidoo@eventease.test', N'$2a$11$TestSeedOnlyPlaceholderHashForBookingSpecialist02', N'BookingSpecialist');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [UserId] = 9403) INSERT INTO [dbo].[Users] ([UserId], [Email], [Password], [Role]) VALUES (9403, N'lebo.dlamini@eventease.test', N'$2a$11$TestSeedOnlyPlaceholderHashForBookingSpecialist03', N'BookingSpecialist');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [UserId] = 9404) INSERT INTO [dbo].[Users] ([UserId], [Email], [Password], [Role]) VALUES (9404, N'admin.seed@eventease.test', N'$2a$11$TestSeedOnlyPlaceholderHashForAdminUser000000', N'Admin');
SET IDENTITY_INSERT [dbo].[Users] OFF;

SET IDENTITY_INSERT [dbo].[Customers] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9301) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9301, N'Nomsa Khumalo', N'nomsa.khumalo@example.co.za', N'+27821234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9302) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9302, N'Sipho Mthembu', N'sipho.mthembu@example.co.za', N'+27761234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9303) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9303, N'Anika van Wyk', N'anika.vanwyk@example.co.za', N'+27831234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9304) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9304, N'Kagiso Molefe', N'kagiso.molefe@example.co.za', N'+27711234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9305) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9305, N'Priya Govender', N'priya.govender@example.co.za', N'+27841234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9306) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9306, N'Lerato Nkosi', N'lerato.nkosi@example.co.za', N'+27611234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9307) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9307, N'David Petersen', N'david.petersen@example.co.za', N'+27851234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9308) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9308, N'Zanele Sithole', N'zanele.sithole@example.co.za', N'+27721234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9309) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9309, N'Ruan Jacobs', N'ruan.jacobs@example.co.za', N'+27861234567');
IF NOT EXISTS (SELECT 1 FROM [dbo].[Customers] WHERE [CustomerId] = 9310) INSERT INTO [dbo].[Customers] ([CustomerId], [Name], [Email], [Phone]) VALUES (9310, N'Boitumelo Maseko', N'boitumelo.maseko@example.co.za', N'+27621234567');
SET IDENTITY_INSERT [dbo].[Customers] OFF;

SET IDENTITY_INSERT [dbo].[Events] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9201) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9201, N'Cape Town Tech Summit 2026', '2026-08-14T09:00:00', '2026-08-14T17:00:00', N'Cloud, AI, and software engineering talks for South African technology teams.', N'https://images.unsplash.com/photo-1540575467063-178a50c2df87?auto=format&fit=crop&w=1200&q=80', 2, 9101);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9202) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9202, N'Mzansi Food and Music Festival', '2026-09-05T12:00:00', '2026-09-05T23:00:00', N'KwaZulu-Natal food stalls, local artists, and family entertainment.', N'https://images.unsplash.com/photo-1533174072545-7a4b6ad7a6c3?auto=format&fit=crop&w=1200&q=80', 11, 9103);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9203) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9203, N'Joburg Wedding and Lifestyle Expo', '2026-07-18T10:00:00', '2026-07-19T16:00:00', N'Wedding suppliers, designers, venues, and lifestyle exhibitors in Sandton.', N'https://images.unsplash.com/photo-1519741497674-611481863552?auto=format&fit=crop&w=1200&q=80', 7, 9102);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9204) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9204, N'Soweto Jazz Night', '2026-10-02T18:30:00', '2026-10-02T23:00:00', N'An evening concert celebrating South African jazz legends and new artists.', N'https://images.unsplash.com/photo-1501386761578-eac5c94b800a?auto=format&fit=crop&w=1200&q=80', 4, 9108);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9205) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9205, N'Table Mountain Charity Gala', '2026-11-12T18:00:00', '2026-11-12T22:30:00', N'Fundraising dinner supporting youth coding programmes in the Western Cape.', N'https://images.unsplash.com/photo-1464366400600-7168b8af9bc3?auto=format&fit=crop&w=1200&q=80', 13, 9105);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9206) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9206, N'SA Rugby Fan Day', '2026-06-20T09:00:00', '2026-06-20T18:00:00', N'Fan activations, junior clinics, and live rugby screenings in Pretoria.', N'https://images.unsplash.com/photo-1540747913346-19e32dc3e97e?auto=format&fit=crop&w=1200&q=80', 10, 9107);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9207) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9207, N'Durban Business Leadership Seminar', '2026-06-30T08:30:00', '2026-06-30T15:00:00', N'Leadership, finance, and operations seminar for KwaZulu-Natal SMEs.', N'https://images.unsplash.com/photo-1556761175-b413da4baf72?auto=format&fit=crop&w=1200&q=80', 6, 9103);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9208) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9208, N'Khayelitsha Makers Workshop', '2026-08-22T09:00:00', '2026-08-22T13:00:00', N'Hands-on design, crafts, and digital making workshop for community creators.', N'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1200&q=80', 5, 9109);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9209) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9209, N'Sun City Tourism Awards Evening', '2026-12-04T18:00:00', '2026-12-04T23:30:00', N'Awards ceremony recognising excellence in South African tourism and hospitality.', N'https://images.unsplash.com/photo-1511795409834-ef04bbd61622?auto=format&fit=crop&w=1200&q=80', 14, 9104);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9210) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9210, N'Gqeberha Coastal Marathon Expo', '2026-07-04T07:00:00', '2026-07-04T15:00:00', N'Race expo, sponsor stalls, and athlete registration for coastal runners.', N'https://images.unsplash.com/photo-1552674605-db6ffd4facb5?auto=format&fit=crop&w=1200&q=80', 10, 9106);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9211) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9211, N'Cape Town Theatre Showcase', '2026-09-18T19:00:00', '2026-09-18T21:30:00', N'Local theatre productions and emerging performers at Artscape.', N'https://images.unsplash.com/photo-1503095396549-807759245b35?auto=format&fit=crop&w=1200&q=80', 12, 9109);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9212) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9212, N'Midrand Corporate Expo', '2026-10-15T09:00:00', '2026-10-16T17:00:00', N'B2B exhibition for enterprise services, cloud solutions, and procurement teams.', N'https://images.unsplash.com/photo-1515169067865-5387ec356754?auto=format&fit=crop&w=1200&q=80', 8, 9110);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9213) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9213, N'Pretoria Youth Innovation Conference', '2026-08-07T09:00:00', '2026-08-07T16:00:00', N'Innovation pitches and mentorship sessions for young founders.', N'https://images.unsplash.com/photo-1517245386807-bb43f82c33c4?auto=format&fit=crop&w=1200&q=80', 2, 9110);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9214) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9214, N'Johannesburg Social Mixer', '2026-06-26T18:00:00', '2026-06-26T22:00:00', N'Networking and social evening for young professionals in Johannesburg.', N'https://images.unsplash.com/photo-1527529482837-4698179dc6ce?auto=format&fit=crop&w=1200&q=80', 9, 9108);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Events] WHERE [EventId] = 9215) INSERT INTO [dbo].[Events] ([EventId], [Name], [StartDateTime], [EndDateTime], [Description], [ImageUrl], [EventTypeId], [VenueId]) VALUES (9215, N'Cape Community Market Day', '2026-06-28T08:00:00', '2026-06-28T14:00:00', N'Outdoor community market featuring local food, crafts, and family activities.', N'https://images.unsplash.com/photo-1488459716781-31db52582fe9?auto=format&fit=crop&w=1200&q=80', 1, 9105);
SET IDENTITY_INSERT [dbo].[Events] OFF;

SET IDENTITY_INSERT [dbo].[Bookings] ON;
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9501) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9501, N'Confirmed', '2026-08-14T08:00:00', '2026-08-14T18:00:00', 9301, 9101, 9201, 9401);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9502) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9502, N'Pending', '2026-09-05T10:00:00', '2026-09-06T01:00:00', 9302, 9103, 9202, 9402);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9503) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9503, N'Confirmed', '2026-07-18T08:00:00', '2026-07-19T18:00:00', 9303, 9102, 9203, 9401);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9504) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9504, N'Completed', '2026-06-26T17:00:00', '2026-06-26T23:00:00', 9304, 9108, 9214, 9403);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9505) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9505, N'Confirmed', '2026-11-12T16:00:00', '2026-11-12T23:00:00', 9305, 9105, 9205, 9402);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9506) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9506, N'Pending', '2026-06-20T08:00:00', '2026-06-20T19:00:00', 9306, 9107, 9206, 9401);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9507) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9507, N'Cancelled', '2026-06-30T07:30:00', '2026-06-30T16:00:00', 9307, 9103, 9207, 9403);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9508) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9508, N'Confirmed', '2026-08-22T08:00:00', '2026-08-22T14:00:00', 9308, 9109, 9208, 9402);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9509) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9509, N'Pending', '2026-12-04T15:00:00', '2026-12-05T01:00:00', 9309, 9104, 9209, 9401);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9510) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9510, N'Confirmed', '2026-07-04T06:00:00', '2026-07-04T16:00:00', 9310, 9106, 9210, 9403);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9511) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9511, N'Confirmed', '2026-09-18T17:00:00', '2026-09-18T22:30:00', 9301, 9109, 9211, 9402);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9512) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9512, N'Confirmed', '2026-10-15T08:00:00', '2026-10-16T18:00:00', 9302, 9110, 9212, 9401);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9513) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9513, N'Pending', '2026-08-07T08:00:00', '2026-08-07T17:00:00', 9303, 9110, 9213, 9403);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9514) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9514, N'Completed', '2026-06-28T07:00:00', '2026-06-28T15:00:00', 9304, 9105, 9215, 9402);
IF NOT EXISTS (SELECT 1 FROM [dbo].[Bookings] WHERE [BookingId] = 9515) INSERT INTO [dbo].[Bookings] ([BookingId], [Status], [StartDateTime], [EndDateTime], [CustomerId], [VenueId], [EventId], [UserId]) VALUES (9515, N'Cancelled', '2026-08-14T19:00:00', '2026-08-14T22:00:00', 9305, 9101, 9201, 9401);
SET IDENTITY_INSERT [dbo].[Bookings] OFF;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Bookings] WHERE [BookingId] BETWEEN 9501 AND 9515;
DELETE FROM [dbo].[Events] WHERE [EventId] BETWEEN 9201 AND 9215;
DELETE FROM [dbo].[Customers] WHERE [CustomerId] BETWEEN 9301 AND 9310;
DELETE FROM [dbo].[Users] WHERE [UserId] BETWEEN 9401 AND 9404;
DELETE FROM [dbo].[Venues] WHERE [VenueId] BETWEEN 9101 AND 9110;
DELETE FROM [dbo].[EventTypes] WHERE [EventTypeId] BETWEEN 10 AND 14;
");
        }
    }
}
