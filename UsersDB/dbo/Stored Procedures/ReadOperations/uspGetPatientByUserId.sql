﻿CREATE PROCEDURE [dbo].[uspGetPatientByUserId]
@userId int
AS
	select * from UserProfile
	inner join Patients on UserProfile.ProfileId = Patients.ProfileId
	where UserProfile.UserId = @userId
