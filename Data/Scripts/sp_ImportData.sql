SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Jay French
-- Create date: 4/24/2011
-- Description: Import Data from old (MySql) database.
-- =============================================
ALTER PROCEDURE [dbo].[ImportData] AS
BEGIN
    DECLARE @Years Table (
        [Year] int
    )

    INSERT @Years
    SELECT DISTINCT TY_Year
    FROM OLD_Team_Year
        
    PRINT 'Remove existing data from NewsItems'
    DELETE FROM NewsItems

    PRINT 'Remove existing data from PageContents'
    DELETE FROM PageContents

    PRINT 'Remove existing data from StatLines'
    DELETE FROM StatLines

    PRINT 'Remove existing data from Players'
    DELETE FROM Players

    PRINT 'Remove existing data from GameParticipants'
    DELETE FROM GameParticipants

    PRINT 'Remove existing data from Games'
    DELETE FROM Games

    PRINT 'Remove existing data from TeamYears'
    DELETE FROM TeamYears

    PRINT 'Remove existing data from Teams'
    DELETE FROM Teams

    PRINT 'Remove existing data from ContactPhoneNumbers'
    DELETE FROM ContactPhoneNumbers

    PRINT 'Remove existing data from Coaches'
    DELETE FROM Coaches

    PRINT 'Remove existing data from Churches'
    DELETE FROM Churches

    PRINT 'Remove existing data from Addresses'
    DELETE FROM Addresses

    PRINT 'Remove existing data from Divisions'
    DELETE FROM Divisions

    PRINT 'Remove existing data from Conferences'
    DELETE FROM Conferences

    PRINT 'Remove existing data from GameStatus'
    DELETE FROM GameStatus

    PRINT 'Remove existing data from GameTypes'
    DELETE FROM GameTypes

    PRINT 'Remove existing data from AspNetUsers (that are not Email addresses)'
    DELETE FROM AspNetUsers
	WHERE UserName NOT LIKE '%@%'

	PRINT 'Updating _Update_ fields that are blank or dates equal to create dates to be null'
	UPDATE OLD_PageContent   SET PC_Update_User = NULL, PC_Update_Date = NULL WHERE LTRIM(PC_Update_User) = '' OR PC_Update_Date = PC_Create_Date
	UPDATE OLD_Player_Stats  SET PS_Update_User = NULL, PS_Update_Date = NULL WHERE LTRIM(PS_Update_User) = '' OR PS_Update_Date = PS_Create_Date
	UPDATE OLD_Player        SET  P_Update_User = NULL,  P_Update_Date = NULL WHERE LTRIM( P_Update_User) = '' OR  P_Update_Date =  P_Create_Date
	UPDATE OLD_News          SET  N_Update_User = NULL,  N_Update_Date = NULL WHERE LTRIM( N_Update_User) = '' OR  N_Update_Date =  N_Create_Date
	UPDATE OLD_Team          SET  T_Update_User = NULL,  T_Update_Date = NULL WHERE LTRIM( T_Update_User) = '' OR  T_Update_Date =  T_Create_Date
	UPDATE OLD_Schedule      SET  S_Update_User = NULL,  S_Update_Date = NULL WHERE LTRIM( S_Update_User) = '' OR  S_Update_Date =  S_Create_Date
	UPDATE OLD_Schedule_Team SET ST_Update_User = NULL, ST_Update_Date = NULL WHERE LTRIM(ST_Update_User) = '' OR ST_Update_Date = ST_Create_Date
	UPDATE OLD_Division      SET  D_Update_User = NULL,  D_Update_Date = NULL WHERE LTRIM( D_Update_User) = '' OR  D_Update_Date =  D_Create_Date
	UPDATE OLD_Conference    SET  C_Update_User = NULL,  C_Update_Date = NULL WHERE LTRIM( C_Update_User) = '' OR  C_Update_Date =  C_Create_Date
	UPDATE OLD_Team_Year     SET TY_Update_User = NULL, TY_Update_Date = NULL WHERE LTRIM(TY_Update_User) = '' OR TY_Update_Date = TY_Create_Date
	UPDATE OLD_Contact       SET CO_Update_User = NULL, CO_Update_Date = NULL WHERE LTRIM(CO_Update_User) = '' OR CO_Update_Date = CO_Create_Date
	UPDATE OLD_Church        SET CH_Update_User = NULL, CH_Update_Date = NULL WHERE LTRIM(CH_Update_User) = '' OR CH_Update_Date = CH_Create_Date

	PRINT 'Updating _Create_Date fields that are blank to be 1/1/2001'
	UPDATE OLD_PageContent   SET PC_Create_Date = '2001-01-01' WHERE PC_Create_Date IS NULL OR PC_Create_Date = ''
	UPDATE OLD_Player_Stats  SET PS_Create_Date = '2001-01-01' WHERE PS_Create_Date IS NULL OR PS_Create_Date = ''
	UPDATE OLD_Player        SET  P_Create_Date = '2001-01-01' WHERE  P_Create_Date IS NULL OR  P_Create_Date = ''
	UPDATE OLD_News          SET  N_Create_Date = '2001-01-01' WHERE  N_Create_Date IS NULL OR  N_Create_Date = ''
	UPDATE OLD_Team          SET  T_Create_Date = '2001-01-01' WHERE  T_Create_Date IS NULL OR  T_Create_Date = ''
	UPDATE OLD_Schedule      SET  S_Create_Date = '2001-01-01' WHERE  S_Create_Date IS NULL OR  S_Create_Date = ''
	UPDATE OLD_Schedule_Team SET ST_Create_Date = '2001-01-01' WHERE ST_Create_Date IS NULL OR ST_Create_Date = ''
	UPDATE OLD_Division      SET  D_Create_Date = '2001-01-01' WHERE  D_Create_Date IS NULL OR  D_Create_Date = ''
	UPDATE OLD_Conference    SET  C_Create_Date = '2001-01-01' WHERE  C_Create_Date IS NULL OR  C_Create_Date = ''
	UPDATE OLD_Team_Year     SET TY_Create_Date = '2001-01-01' WHERE TY_Create_Date IS NULL OR TY_Create_Date = ''
	UPDATE OLD_Contact       SET CO_Create_Date = '2001-01-01' WHERE CO_Create_Date IS NULL OR CO_Create_Date = ''
	UPDATE OLD_Church        SET CH_Create_Date = '2001-01-01' WHERE CH_Create_Date IS NULL OR CH_Create_Date = ''

	PRINT 'Updating _Create_User fields that are blank to be Unknown'
	UPDATE OLD_PageContent   SET PC_Create_User = 'Unknown' WHERE PC_Create_User IS NULL OR LTRIM(PC_Create_User) = ''
	UPDATE OLD_Player_Stats  SET PS_Create_User = 'Unknown' WHERE PS_Create_User IS NULL OR LTRIM(PS_Create_User) = ''
	UPDATE OLD_Player        SET  P_Create_User = 'Unknown' WHERE  P_Create_User IS NULL OR LTRIM( P_Create_User) = ''
	UPDATE OLD_News          SET  N_Create_User = 'Unknown' WHERE  N_Create_User IS NULL OR LTRIM( N_Create_User) = ''
	UPDATE OLD_Team          SET  T_Create_User = 'Unknown' WHERE  T_Create_User IS NULL OR LTRIM( T_Create_User) = ''
	UPDATE OLD_Schedule      SET  S_Create_User = 'Unknown' WHERE  S_Create_User IS NULL OR LTRIM( S_Create_User) = ''
	UPDATE OLD_Schedule_Team SET ST_Create_User = 'Unknown' WHERE ST_Create_User IS NULL OR LTRIM(ST_Create_User) = ''
	UPDATE OLD_Division      SET  D_Create_User = 'Unknown' WHERE  D_Create_User IS NULL OR LTRIM( D_Create_User) = ''
	UPDATE OLD_Conference    SET  C_Create_User = 'Unknown' WHERE  C_Create_User IS NULL OR LTRIM( C_Create_User) = ''
	UPDATE OLD_Team_Year     SET TY_Create_User = 'Unknown' WHERE TY_Create_User IS NULL OR LTRIM(TY_Create_User) = ''
	UPDATE OLD_Contact       SET CO_Create_User = 'Unknown' WHERE CO_Create_User IS NULL OR LTRIM(CO_Create_User) = ''
	UPDATE OLD_Church        SET CH_Create_User = 'Unknown' WHERE CH_Create_User IS NULL OR LTRIM(CH_Create_User) = ''

	PRINT 'Finding unique user names'
	DECLARE @UniqueUsers TABLE (Username varchar(100), UserId varchar(128))
	INSERT @UniqueUsers (Username)
	SELECT DISTINCT PC_Create_User FROM OLD_PageContent UNION
	SELECT DISTINCT PS_Create_User FROM OLD_Player_Stats UNION
	SELECT DISTINCT P_Create_User FROM OLD_Player UNION
	SELECT DISTINCT N_Create_User FROM OLD_News UNION
	SELECT DISTINCT T_Create_User FROM OLD_Team UNION
	SELECT DISTINCT S_Create_User FROM OLD_Schedule UNION
	SELECT DISTINCT ST_Create_User FROM OLD_Schedule_Team UNION
	SELECT DISTINCT D_Create_User FROM OLD_Division UNION
	SELECT DISTINCT C_Create_User FROM OLD_Conference UNION
	SELECT DISTINCT TY_Create_User FROM OLD_Team_Year UNION
	SELECT DISTINCT CO_Create_User FROM OLD_Contact UNION
	SELECT DISTINCT CH_Create_User FROM OLD_Church UNION
	SELECT DISTINCT PC_Update_User FROM OLD_PageContent UNION
	SELECT DISTINCT PS_Update_User FROM OLD_Player_Stats UNION
	SELECT DISTINCT P_Update_User FROM OLD_Player UNION
	SELECT DISTINCT N_Update_User FROM OLD_News UNION
	SELECT DISTINCT T_Update_User FROM OLD_Team UNION
	SELECT DISTINCT S_Update_User FROM OLD_Schedule UNION
	SELECT DISTINCT ST_Update_User FROM OLD_Schedule_Team UNION
	SELECT DISTINCT D_Update_User FROM OLD_Division UNION
	SELECT DISTINCT C_Update_User FROM OLD_Conference UNION
	SELECT DISTINCT TY_Update_User FROM OLD_Team_Year UNION
	SELECT DISTINCT CO_Update_User FROM OLD_Contact UNION
	SELECT DISTINCT CH_Update_User FROM OLD_Church

	PRINT 'Creating User records that don''t already exist'
	INSERT AspNetUsers (Id, UserName, FirstName, LastName, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
	SELECT NEWID(), uu.Username, uu.Username, 'Pre-2016', 0, 0, 0, 0, 0
	FROM @UniqueUsers uu
		LEFT JOIN AspNetUsers u ON u.UserName = uu.Username
	WHERE LEN(uu.Username) > 0
	  AND u.Id IS NULL

	PRINT 'Getting created user''s ids'
	UPDATE uu SET
		UserId = u.Id
	FROM @UniqueUsers uu
		JOIN AspNetUsers u ON u.UserName = uu.Username

    PRINT 'Inserting into GameStatus'
    SET IDENTITY_INSERT GameStatus ON
    INSERT GameStatus (GameStatusId, Description, DisplayOutcome, IsComplete, AllowStatistics)
        SELECT
            GS_Id,
            GS_Description,
            GS_Display_Scores,
            GS_Completed,
			CASE WHEN GS_Id = 5 THEN 1 ELSE 0 END
        FROM OLD_GameStatus
        ORDER BY
            GS_Id
    SET IDENTITY_INSERT GameStatus OFF

    PRINT 'Inserting into GameTypes'
    SET IDENTITY_INSERT GameTypes ON
    INSERT GameTypes (GameTypeId, [Description], RecordGame)
        SELECT
            GT_Id,
            GT_Description,
            GT_Record
        FROM OLD_GameType
        ORDER BY
            GT_Id
    SET IDENTITY_INSERT GameTypes OFF

    PRINT 'Inserting into Conferences'
    SET IDENTITY_INSERT Conferences ON
    INSERT Conferences (ConferenceId, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            C_Id,
            C_Create_Date,
            uC.UserId,
            C_Update_Date,
            uU.UserId
        FROM OLD_Conference
			JOIN @UniqueUsers uC ON uC.Username = C_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = C_Update_User
        ORDER BY
            C_Id
    SET IDENTITY_INSERT Conferences OFF

    PRINT 'Inserting into ConferenceYears'
    DBCC CHECKIDENT ([ConferenceYears], RESEED, 0)
    INSERT ConferenceYears (ConferenceId, [Year], Name, IsInLeague, Sort, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            C_Id,
            [Year],
            C_Name,
            C_In_League,
            C_Sort,
            C_Create_Date,
			uC.UserId,
            C_Update_Date,
            uU.UserId
        FROM OLD_Conference
			JOIN @UniqueUsers uC ON uC.Username = C_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = C_Update_User
			, @Years
        ORDER BY
            [Year], 
            C_Id

    PRINT 'Inserting into Divisions'
    SET IDENTITY_INSERT Divisions ON
    INSERT Divisions (DivisionId, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            D_Id,
            D_Create_Date,
			uC.UserId,
            D_Update_Date,
            uU.UserId
        FROM OLD_Division
			JOIN @UniqueUsers uC ON uC.Username = D_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = D_Update_User
        ORDER BY
            D_Id
    SET IDENTITY_INSERT Divisions OFF

    PRINT 'Inserting into DivisionYears'
    DBCC CHECKIDENT ([DivisionYears], RESEED, 0)
    INSERT DivisionYears (DivisionId, ConferenceYearId, [Year], Name, IsInLeague, Sort, Created, CreatedBy, Modified, ModifiedBy)
        SELECT DISTINCT
            D_Id,
            ConferenceYearId,
            [Year],
            D_Name,
            D_In_League,
            D_Sort,
            D_Create_Date,
			uC.UserId,
            D_Update_Date,
            uU.UserId
        FROM           OLD_Division
            INNER JOIN OLD_Team_Year   ON TY_D_Id      = D_Id 
            INNER JOIN ConferenceYears ON ConferenceId = D_C_Id
                                      AND [Year]       = TY_Year
			JOIN @UniqueUsers uC ON uC.Username = D_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = D_Update_User
        ORDER BY
            [Year],
            D_Id

    PRINT 'Inserting into Addresses for Churches'
    SET IDENTITY_INSERT Addresses ON
    INSERT Addresses (AddressId, Street1, City, StateId, Zip, Created, CreatedBy)
        SELECT
            CH_Id,
            CASE WHEN LTRIM(ISNULL(CH_Address_Street, '')) = '' THEN NULL ELSE LTRIM(RTRIM(CH_Address_Street)) END,
            CASE WHEN LTRIM(ISNULL(CH_Address_City, '')) = '' THEN NULL ELSE LTRIM(RTRIM(CH_Address_City)) END,
            CASE WHEN LTRIM(ISNULL(CH_Address_State, '')) = 'MN' THEN 23 ELSE NULL END,
            CASE WHEN LTRIM(ISNULL(CH_Address_Zip, '')) = '' THEN NULL ELSE LTRIM(RTRIM(CH_Address_Zip)) END,
            GETDATE(),
            uC.UserId
        FROM OLD_Church
			JOIN @UniqueUsers uC ON uC.Username = CH_Create_User
        ORDER BY
            CH_Id
    SET IDENTITY_INSERT Addresses OFF

    PRINT 'Inserting into Churches'
    SET IDENTITY_INSERT Churches ON
    INSERT Churches (ChurchId, AddressId, FullName, DisplayName, Information, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            CH_Id,
            CH_Id + 0,
            CH_Name,
            CH_Name_Short,
            REPLACE(REPLACE(LTRIM(RTRIM(CH_Info)), '\r\n', CHAR(13) + CHAR(10)), '\n', CHAR(13) + CHAR(10)),
            CH_Create_Date,
			uC.UserId,
            CH_Update_Date,
            uU.UserId
        FROM OLD_Church
			JOIN @UniqueUsers uC ON uC.Username = CH_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = CH_Update_User
        ORDER BY
            CH_Id
    SET IDENTITY_INSERT Churches OFF

    PRINT 'Inserting into ContactPhoneNumbers for Churches'
    INSERT ContactPhoneNumbers (ChurchId, PhoneNumberTypeId, PhoneNumber, Created, CreatedBy)
        SELECT
            CH_Id,
            1,
            LTRIM(RTRIM(CH_Phone)),
            GETDATE(),
            uC.UserId
        FROM OLD_Church
			JOIN @UniqueUsers uC ON uC.Username = CH_Create_User
		WHERE LTRIM(ISNULL(CH_Phone, '')) <> ''
        ORDER BY
            CH_Id

    PRINT 'Inserting into Coaches'
    SET IDENTITY_INSERT Coaches ON
    INSERT Coaches (CoachId, LastName, FirstName, Comments, EmailAddress, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            CO_Id,
            CASE WHEN LTRIM(CO_Name_Last) = '' THEN NULL ELSE LTRIM(RTRIM(CO_Name_Last)) END,
            CASE WHEN LTRIM(CO_Name_First) = '' THEN NULL ELSE LTRIM(RTRIM(CO_Name_First)) END,
            CASE WHEN LTRIM(ISNULL(CO_Comments, '')) = '' THEN NULL ELSE REPLACE(REPLACE(LTRIM(RTRIM(CO_Comments)), '\r\n', CHAR(13) + CHAR(10)), '\n', CHAR(13) + CHAR(10)) END,
            CASE WHEN LTRIM(ISNULL(CO_Email, '')) = '' THEN NULL ELSE LTRIM(RTRIM(CO_Email)) END,
            CO_Create_Date,
			uC.UserId,
            CO_Update_Date,
            uU.UserId
        FROM OLD_Contact
			JOIN @UniqueUsers uC ON uC.Username = CO_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = CO_Update_User
        ORDER BY
            CO_Id
    SET IDENTITY_INSERT Coaches OFF

    PRINT 'Inserting into ContactPhoneNumbers for Coaches'
    INSERT ContactPhoneNumbers (CoachId, PhoneNumberTypeId, PhoneNumber, Created, CreatedBy)
        SELECT
            CO_Id,
            1,
            LTRIM(RTRIM(CO_Phone)),
            GETDATE(),
            uC.UserId
        FROM OLD_Contact
			JOIN @UniqueUsers uC ON uC.Username = CO_Create_User
		WHERE LTRIM(ISNULL(CO_Phone, '')) <> ''
        ORDER BY
            CO_Id

    PRINT 'Inserting into Teams'
    SET IDENTITY_INSERT Teams ON
    INSERT Teams (TeamId, FieldInformation, Comments, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            T_Id,
            CASE WHEN LTRIM(ISNULL(T_Field_Directions, '')) = '' THEN NULL ELSE LTRIM(RTRIM(T_Field_Directions)) END,
            CASE WHEN LTRIM(ISNULL(T_Comments, '')) = '' THEN NULL ELSE REPLACE(REPLACE(LTRIM(RTRIM(T_Comments)), '\r\n', CHAR(13) + CHAR(10)), '\n', CHAR(13) + CHAR(10)) END,
            T_Create_Date,
			uC.UserId,
            T_Update_Date,
            uU.UserId
        FROM OLD_Team
			JOIN @UniqueUsers uC ON uC.Username = T_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = T_Update_User
        ORDER BY
            T_Id
    SET IDENTITY_INSERT Teams OFF

    PRINT 'Inserting into TeamYears'
    SET IDENTITY_INSERT TeamYears ON
    INSERT TeamYears (TeamYearId, TeamId, [Year], TeamName, FullName, DivisionYearId, ChurchId, HeadCoachId, KeepsStats, HasPaid, Clinch, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            TY_Id,
            TY_T_Id,
            TY_Year,
            CASE WHEN LTRIM(ISNULL(TY_Name, '')) = '' THEN NULL ELSE LTRIM(RTRIM(TY_Name)) END,
			RTRIM(LTRIM(ISNULL(DisplayName, '') + ' ' + ISNULL(TY_Name, ''))),
            DivisionYearId,
            TY_CH_Id,
            TY_CO_Id,
            TY_Keeps_Stats,
            T_Paid,
            CASE WHEN ISNULL(TY_ClinchChar, '') = '' THEN NULL ELSE TY_ClinchChar END,
            TY_Create_Date,
			uC.UserId,
            TY_Update_Date,
            uU.UserId
        FROM           OLD_Team_Year
            INNER JOIN OLD_Team       ON T_Id       = TY_T_Id
            INNER JOIN Churches       ON ChurchId   = TY_CH_Id
            INNER JOIN DivisionYears  ON DivisionId = TY_D_Id
                                     AND [Year]     = TY_Year
			JOIN @UniqueUsers uC ON uC.Username = TY_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = TY_Update_User
        ORDER BY
            TY_Id
    SET IDENTITY_INSERT TeamYears OFF

    PRINT 'Inserting into Games'
    SET IDENTITY_INSERT Games ON
    INSERT Games (GameId, GameDate, GameTypeId, GameStatusId, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            S_Id,
            S_Date,
            S_GT_Id,
            S_GS_Id,
            S_Create_Date,
			uC.UserId,
            S_Update_Date,
            uU.UserId
        FROM OLD_Schedule
			JOIN @UniqueUsers uC ON uC.Username = S_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = S_Update_User
        ORDER BY
            S_Id
    SET IDENTITY_INSERT Games OFF

    PRINT 'Inserting into GameParticipants'
    DBCC CHECKIDENT ([GameParticipants], RESEED, 0)
    INSERT GameParticipants (GameId, TeamYearId, IsHost, RunsScored, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            ST_S_Id,
            TY_Id,
            ST_Host,
            ST_Runs_Scored,
            ST_Create_Date,
			uC.UserId,
            ST_Update_Date,
            uU.UserId
        FROM           OLD_Schedule_Team
            INNER JOIN OLD_Schedule      ON S_Id    = ST_S_Id
            INNER JOIN OLD_Team_Year     ON TY_T_Id = ST_T_Id
                                        AND TY_Year = YEAR(S_Date)
			JOIN @UniqueUsers uC ON uC.Username = ST_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = ST_Update_User
        ORDER BY
            ST_S_Id,
            ST_Host

    PRINT 'Inserting into Players'
    SET IDENTITY_INSERT Players ON
    INSERT Players (PlayerId, LastName, FirstName, IsActive, CurrentTeamId, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            P_Id,
            CASE WHEN LTRIM(ISNULL(P_Name_Last, '')) = '' THEN
                    CASE WHEN LTRIM(ISNULL(P_Name_First, '')) = '' THEN CONVERT(varchar, P_Id) ELSE 'Something' END
                 ELSE LTRIM(RTRIM(P_Name_Last)) END,
            CASE WHEN LTRIM(ISNULL(P_Name_First, '')) = '' THEN 'Mr.'
                 ELSE LTRIM(RTRIM(P_Name_First)) END,
            P_Active,
            P_T_Id,
            P_Create_Date,
			uC.UserId,
            P_Update_Date,
            uU.UserId
        FROM OLD_Player
			JOIN @UniqueUsers uC ON uC.Username = P_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = P_Update_User
        ORDER BY
            P_Id
    SET IDENTITY_INSERT Players OFF

    PRINT 'Inserting into StatLines'
    DBCC CHECKIDENT ([StatLines], RESEED, 0)
    INSERT StatLines (PlayerId, GameParticipantId, BattingOrderPosition, StatSingles, StatDoubles, StatTriples, StatHomeRuns, StatWalks, StatSacrificeFlies, StatOuts, StatFieldersChoices, StatReachedByErrors, StatStrikeouts, StatRuns, StatRunsBattedIn, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            PS_P_Id,
            GameParticipantId,
            PS_Order,
            PS_1B,
            PS_2B,
            PS_3B,
            PS_HR,
            PS_BB,
            PS_Sac,
            PS_Outs,
            PS_FC,
            PS_E,
            PS_SO,
            PS_Runs,
            PS_RBI,
            PS_Create_Date,
			uC.UserId,
            PS_Update_Date,
            uU.UserId
        FROM           OLD_Player_Stats
            INNER JOIN Games            g   ON g.GameId       = PS_S_Id
            INNER JOIN TeamYears        ty  ON ty.TeamId      = PS_T_Id
                                           AND ty.[Year]      = YEAR(g.GameDate)
            INNER JOIN GameParticipants gty ON gty.GameId     = g.GameId
                                           AND gty.TeamYearId = ty.TeamYearId
			JOIN @UniqueUsers uC ON uC.Username = PS_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = PS_Update_User
        ORDER BY
            PS_S_Id,
            PS_T_Id,
            PS_Order

    PRINT 'Remove Players who don''t have stats'
    DELETE
    FROM Players
    WHERE PlayerId IN (
        SELECT DISTINCT
            p.PlayerId
        FROM          Players   p
            LEFT JOIN StatLines sl ON sl.PlayerId = p.PlayerId
        WHERE sl.PlayerId IS NULL
    )

    PRINT 'Inserting into NewsItems'
    SET IDENTITY_INSERT NewsItems ON
    INSERT NewsItems (NewsItemId, StartDate, EndDate, IsActive, TeamId, Subject, Content, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            N_Id,
            CASE WHEN ISNULL(N_Date_Start, '0000-00-00 00:00:00') = '0000-00-00 00:00:00' THEN N_Create_Date ELSE N_Date_Start END,
            CASE WHEN ISNULL(N_Date_End, '0000-00-00 00:00:00') = '0000-00-00 00:00:00' THEN '9999-12-31 23:59:59' ELSE N_Date_End END,
            N_Active,
            CASE WHEN ISNULL(N_T_Id, 0) = 0 THEN NULL ELSE N_T_Id END,
            CASE WHEN LTRIM(ISNULL(N_Subject, '')) = '' THEN NULL ELSE LTRIM(RTRIM(N_Subject)) END,
            CASE WHEN LTRIM(ISNULL(N_Content, '')) = '' THEN '' ELSE REPLACE(REPLACE(N_Content, '\r\n', CHAR(13) + CHAR(10)), '\n', CHAR(13) + CHAR(10)) END,
            N_Create_Date,
			uC.UserId,
            N_Update_Date,
            uU.UserId
        FROM OLD_News
			JOIN @UniqueUsers uC ON uC.Username = N_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = N_Update_User
        ORDER BY
            N_Id
    SET IDENTITY_INSERT NewsItems OFF

    PRINT 'Inserting into PageContents'
    SET IDENTITY_INSERT PageContents ON
    INSERT PageContents (PageContentId, PageTag, Title, Content, Created, CreatedBy, Modified, ModifiedBy)
        SELECT
            PC_Id,
			REPLACE(PC_Page, ' ', ''),
            PC_Page,
            REPLACE(REPLACE(PC_Content, '\r\n', CHAR(13) + CHAR(10)), '\n', CHAR(13) + CHAR(10)),
            PC_Create_Date,
			uC.UserId,
            PC_Update_Date,
            uU.UserId
        FROM OLD_PageContent
			JOIN @UniqueUsers uC ON uC.Username = PC_Create_User
			LEFT JOIN @UniqueUsers uU ON uU.Username = PC_Update_User
        ORDER BY
            PC_Id
    SET IDENTITY_INSERT PageContents OFF

    PRINT 'Updating Phone Numbers with formatting'
	UPDATE ContactPhoneNumbers SET
		PhoneNumber = '(' + SUBSTRING(PhoneNumber, 1, 3) + ') ' + SUBSTRING(PhoneNumber, 4, 3) + '-' + SUBSTRING(PhoneNumber, 7, 4)
	WHERE LEN(PhoneNumber) = 10
END
