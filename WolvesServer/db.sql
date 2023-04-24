use master
go
create database WolvesTeam
go
use WolvesTeam
go
create table Account
(
    Id          int primary key identity (100000,1),
    PhoneNumber char(20)            not null,
    Email       varchar(100) unique not null,
    FirstName   nvarchar(100)       not null,
    LastName    nvarchar(100)       not null,
    DateOfBirth date                not null,
    Avatar      ntext,
    Wolves        int,
    Type        int,
    IsActive    bit default 1
)
go
create proc CountAccount
as
begin
    select count(Id) from Account
end
go
create proc GetAccountList @page int
as
begin
    select * from Account where (@page - 1) * 15 + 1 <= Id and Id <= 15 * @page
end
go
create proc SearchAccount @keyword nvarchar(50)
as
begin
    select *
    from Account
    where PhoneNumber like N'%' + @keyword + '%'
       or Email like N'%' + @keyword + '%'
       or FirstName like N'%' + @keyword + '%'
       or LastName like N'%' + @keyword + '%'
       or DateOfBirth like N'%' + @keyword + '%'
end
go
create proc Register @PhoneNumber char(20), @Email varchar(100),
                     @FirstName nvarchar(100), @LastName nvarchar(100), @DateOfBirth date, @Avatar ntext
as
begin
    insert into Account(PhoneNumber, Email, FirstName, LastName, DateOfBirth, Avatar, Wolves, [Type])
    values (@PhoneNumber, @Email, @FirstName, @LastName, @DateOfBirth, @Avatar, 0, 0)
end
go
create proc UpdateAccount @Id int, @PhoneNumber char(20), @Email varchar(100),
                          @FirstName varchar(100), @LastName varchar(100), @DateOfBirth date, @Avatar ntext
as
begin
    update Account
    set PhoneNumber= @PhoneNumber,
        Email = @Email,
        FirstName= @FirstName,
        LastName= @LastName,
        DateOfBirth = @DateOfBirth,
        Avatar= @Avatar
    where Id = @Id
end
go
create proc BlockAccount @Id int
as
begin
    update Account set IsActive = 0 where Id = @Id
end
go
create proc UnBlockAccount @Id int
as
begin
    update Account set IsActive = 1 where Id = @Id
end
go
create proc LoginAccount @Email varchar(100)
as
begin
    select * from Account where Email = @Email and IsActive = 1
end
-----------------------------------------------------------------------------------------------
go
create table VIP
(
    Id        int primary key identity (1,1),
    IdAccount int
        constraint fk_user_vip foreign key (IdAccount) references Account (Id),
    Start     date not null,
    [End]     date not null,
    Type      int
)

go
create proc CheckVIP @IdAccount int
as
begin
    select * from VIP where IdAccount = @IdAccount and [Start] <= getdate() and GETDATE() <= [End]
end
go
go
create proc RegisterVIP @Month int, @Type int, @Wol int, @IdAccount int
as
begin
    insert into VIP(IDACCOUNT, START, [END], TYPE) values (@IdAccount, GETDATE(), getdate() + (@Month * 30), @Type)
    update Account set Wolves = Wolves - @Wol where Account.Id = @IdAccount
end
--------------------------------------------------------------------------------------------
go
create table Invite
(
    Id        int primary key identity (1,1),
    Presenter int not null
        constraint fk_presenter_invite foreign key (Presenter) references Account (Id),
    Presentee int not null,
)

go
create proc GetInvited @Id int
as
begin
    select PhoneNumber, Email, FirstName, LastName, DateOfBirth, Avatar
    from Invite,
         Account
    where Invite.Presenter = @Id
      and Invite.Presentee = Account.Id
end
go
create proc InputInvite @Presenter int, @Preseentee int
as
begin
    insert into Invite(Presenter, Presentee) values (@Presenter, @Preseentee)
    insert into VIP(IdAccount,[Start],[End],Type) values(@Presenter,getDate(),getDate()+30,1)
end
go
--------------------------------------------------------------------------------
create table LoadWolves
(
    Id        int identity primary key,
    Wolves      int not null,
    IdAccount int not null
        constraint fk_id_load_Wolves foreign key (IdAccount) references Account (Id),
    [Status]  int default 0
)
go
create proc GetCountLoadWolvesWaiting
as
begin
    select count(Id) from LoadWolves where [Status] = 0
end
go
create proc GetCountLoadWolvesConfirmed
as
begin
    select count(Id) from LoadWolves where [Status] = 1
end
go
create proc GetCountLoadWolvesCancel
as
begin
    select count(Id) from LoadWolves where [Status] = 2
end
go
create proc GetLoadWolvesListWaiting @page int
as
begin
    select * from LoadWolves where (@page - 1) * 15 + 1 <= Id and Id <= 15 * @page and [Status] = 0
end
go

create proc GetLoadWolvesListConfirmed @page int
as
begin
    select * from LoadWolves where (@page - 1) * 15 + 1 <= Id and Id <= 15 * @page and [Status] = 1
end
go

create proc GetLoadWolvesListCancel @page int
as
begin
    select * from LoadWolves where (@page - 1) * 15 + 1 <= Id and Id <= 15 * @page and [Status] = 2
end

go
create proc LoadingWolves @Wolves int, @IdAccount int
as
begin
    insert into LoadWolves(Wolves, IdAccount, Status) values (@Wolves, @IdAccount, 0)
end
go
create proc ConfirmWolves @Id int, @IdAccount int, @Wolves int
as
begin
    update LoadWolves set Status= 1 where Id = @Id
    update Account set Wolves = @Wolves where Id = @IdAccount
    --set for lv1

    update Account
    set Wolves = Account.Wolves + (@Wolves * 20 / 100)
    where Id in (select Presenter from Invite where Invite.Presentee = @IdAccount)
    --set for lv2

    update Account
    set Wolves = Account.Wolves + (@Wolves * 5 / 100)
    where Id in (select top 1 Presenter
                 from Invite
                 where Invite.Presentee = (select Presenter from Invite where Invite.Presentee = @IdAccount))
    --set for lv3
    update Account
    set Wolves = Account.Wolves + (@Wolves * 3 / 100)
    where Id in (select top 1 Presenter
                 from Invite
                 where Invite.Presentee = (select Presenter from Invite where Invite.Presentee =(
                 select top 1 Presenter from Invite where Invite.Presentee = (select Presenter from Invite where Invite.Presentee = @IdAccount))))

end
go


-------------------------------------------------------------------------------------------
go
create table
    Symbol
(
    Name varchar(20) primary key,
)
go
create proc InsertSymbol @Name varchar(20)
as
begin
    if not exists(select Name from Symbol where Name = @Name)
        begin
            insert into Symbol values (@Name)
        end
end
go
create proc GetSymbolList
as
begin
    select * from Symbol
end
go
create table Rate
(
    Id     int primary key identity (1,1),
    Name   varchar(20) not null
        constraint fk_name_rate foreign key (Name) references Symbol (Name),
    Buy    float,
    Sell   float,
    [Date] date,
    [Time] time
)
go
create proc InsertRate @Name varchar(20), @Buy float, @Rate float
as
begin
    insert into Rate([Name], Buy, Sell, [Date], [Time])
    values (@Name, @Buy, @Rate, cast(getdate() as date), cast(getdate() as time))
end
go
create proc GetLastRateByName @Name varchar(20)
as
begin
    select Top 1 *
    from Rate
    where Name = @Name
    order by Id desc
end
go
create proc GetRateByNameAndDate @Name varchar(20), @Date date
as
begin
    select * from Rate where Name = @Name and cast(@Date as date) = Rate.Date
end
-----------------------------------------------------------------------------------------------------
go
create table News
(
    Id        int identity (1,1) primary key,
    [Date]    date not null,
    [Time]    time not null,
    [Content] nvarchar(max),
    Type      bit
)
go
create proc AddNews @Date date, @Time time, @Content nvarchar(max), @Type bit
as
begin
    insert into News(Date, Time, Content, Type) values (@Date, @Time, @Content, @Type)
end
go
create proc GetNews
as
begin
    select * from News where Type = 0
end
go
create proc GetNewsVip
as
begin
    select * from News where Type = 1
end
go
create proc GetCurrentAndPrevRate
as
begin
    declare @numOfSymbol int
    set @numOfSymbol = (select COUNT(Symbol.Name) from Symbol)

    select Top (@numOfSymbol * 2) Symbol.Name, Sell
    from Symbol,
         Rate
    where Symbol.Name = Rate.Name
    Order by Id DESC
end
go
create proc GetMinMaxInDay @name nvarchar(50), @date date
as
begin
    select max(Rate.Sell) [max], min(Rate.Sell) [min] from Rate where Rate.Date = @date and Name = @name
end
go
create proc GetCurrentAndPrevByNameAndDay @name nvarchar(50), @date date
as
begin
    select top 2 *
    from Rate
    where Rate.Date = @date
      and Name = @name
    order by Id desc
end
go
create proc GetNewsByDate @date Date
as
begin
    select *
    from News
    where Type = 0
      and Date = @date
    order by id desc
end
go

create proc GetNewsVipByDate @date date
as
begin
    select * from News where Type = 1 and Date = @date
end
go
create proc GetIDByEmail @Email nvarchar(50)
as
begin
    select Id from Account where Email = @Email
end
go
--------------------------------------------------------
create table BanLenh
(
    Id        int identity primary key,
    [Date]    date,
    [Content] nvarchar(100),
    TP        float,
    SL        float,
    Image     nvarchar(max)
)
go
create proc AddBanLenh @Date date, @Content nvarchar(100), @TP float, @SL float, @Image nvarchar(max)
as
begin
    insert into BanLenh(Date, Content, TP, SL, Image) values (@Date, @Content, @TP, @SL, @Image)
end
go
go
create proc GetBanLenh @Date date
as
begin
    select * from BanLenh where Date = @Date
end
go
------------------------------------------------------------
create table Contact
(
    Id int identity primary key,
    Address     nvarchar(max),
    Gmail       nvarchar(100),
    PhoneNumber varchar(20),
    STK         varchar(100),
    NameBank    nvarchar(100),
    Bank        nvarchar(100),
    Website     nvarchar(100),
    Telegram nvarchar(max)
)
go
create table PackVIP(
    Id int identity primary key ,
    Month int,
    Wol int
)
go
insert into PackVIP(Month,Wol) values(1,59),(3,150),(6,250),(12,450)
go
create table NewWolves(
    Id int identity primary key,
    Titile nvarchar(max),
    Content nvarchar(max),
    Image nvarchar(max),
    [Date] date default cast(getdate() as date)
)
go
insert into Contact(Address,
    Gmail     ,
    PhoneNumber ,
    STK        ,
    NameBank    ,
    Bank       ,
    Website     ,
    Telegram ) values(N'Toà Nhà Gold Coast Số 1 Trần Hưng Đạo, TP. Nha Trang, Khánh Hoà','WolvesVNTeam@gmail.com','0969.239.222','9969239222',N'LE GIA HUY','Vietcombank',N'http://wolvesvn.com.vn','https://t.me/wolvesvn_channel')
go
create table DoiTien(
    Id int identity primary key,
    Quantity int,
    IdAccount int,
    STK nvarchar(100),
    Bank nvarchar(100),
    NameBank nvarchar(100),
    Status bit default 0
)
go
create proc GetDoiTien
as
begin
select * from DoiTien,Account where  DoiTien.Status = 0
end
go
create proc InsertDoiTien @IdAccount int, @Quantity int,@STK nvarchar(100),@Bank nvarchar(100),@NameBank nvarchar(100)
as
begin
insert into DoiTien(IdAccount,Quantity,STK,Bank,NameBank) values(@IdAccount,@Quantity,@STK,@Bank,@NameBank)
update Account set Wolves = Wolves-@Quantity where Id = @IdAccount
end
go
create proc ConfirmDoiTien @Id int
as
begin
    update DoiTien set Status = 1 where Id = @Id
end
go
create table DoiTac(
    Id int identity primary key,
    TenDoiTac nvarchar(100),
    TrangWeb nvarchar(100),
    ThongTinKhac nvarchar(max)
)
go

go
create table ThongKe(
    Id int identity primary key,
    [Money] nvarchar(50),
    [Date] datetime default getdate(),
    PipCu int,
    PipMoi int,
    SL float,
    TP float
)
go
create proc GetThongKe @Date date
as 
begin
select * from ThongKe where Date = @Date
end
go
create table TongQuat(
    Id int identity primary key,
    TongPip int,
    Trades int,
    WinRate float
)
go
create proc GetTongQuat
as
select top(1) * from TongQuat order by Id desc
go
create table SanGiaoDich(
    Id int identity primary key,
    Titile nvarchar(100),
    Content nvarchar(max)
)
go
alter proc GetTinHieuPost @Date date
as
begin
    select * from TinHieuPost where Date = @Date
end
go
create proc GetLastTinHieuPost
as
begin
    select top 1 * from TinHieuPost where Date = GetDate()
    order by Id desc
end
go
create table Gold(
    Id int identity primary key,
    Symbol nvarchar(100),
    SoldOut float,
    BuyInto float,
    Date date default getdate()
)
go
create table TinHieuPost(
    Id        int identity primary key,
    [Date]    date default getdate(),
    [Content] nvarchar(100),
    TP        float,
    SL        float,
    Image     nvarchar(max)
)
go
alter table Gold add  Content nvarchar(max)
go

create table Video(
    Id int identity primary key,
    Content nvarchar(max),
    LinkVideo nvarchar(max),
    LinkYoutube nvarchar(max)
)