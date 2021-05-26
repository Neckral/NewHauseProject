-- Execute those procedures befor first project launch to create them and have ability to call them when need

create proc GetImagesByAdId
@adid int
as
select * from Images where AdID = @adid
go

create proc RemoveFavoriteByUserIdAndAdId
@userId varchar(500),
@adId int
as
delete from Favorites where UserID = @userId and AdID = @adId
go