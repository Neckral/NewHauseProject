create proc GetImagesByAdId
@adid int
as
select * from Images where AdID = @adid
go

create proc RemoveFavoriteByUserIdAndAdId
@userId varchar,
@adId int
as
delete from Favorites where UserID = @userId and AdID = @adId
go