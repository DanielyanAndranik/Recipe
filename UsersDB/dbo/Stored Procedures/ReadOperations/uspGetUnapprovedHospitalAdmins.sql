﻿CREATE PROCEDURE [dbo].[uspGetUnapprovedHospitalAdmins]
AS
	select p.UserId, u.FullName, h.HospitalName, p.CreatedDate as 'ProfileCreatedOn', h.StartedWorking 
	from Users u
	join UserProfile p on u.Id = p.UserId
	join HospitalDirectors h on p.ProfileId = h.ProfileId
	where p.[Type] = 'hospital_director' and p.IsApproved = 0

RETURN 0
