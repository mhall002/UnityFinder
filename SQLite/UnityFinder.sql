begin transaction;

drop table if exists Terrain;
drop table if exists TerrainState;

drop table if exists Status;
drop table if exists Entity;
drop table if exists EntityAbility;

drop table if exists Ability;
drop table if exists Campaign;
drop table if exists CampaignEntity;
drop table if exists CampaignRoomLink;
drop table if exists Room;
drop table if exists RoomEntity;

create table Terrain(
	ID integer NOT NULL primary key,
	Description varchar(255),
	EntityCode varchar(1),
	Name varchar(255),
	TextureFilePath varchar(255),
	TypeID integer
);

insert into Terrain (Description, EntityCode, Name, TextureFilePath, TypeID)
values ('It''s a mat!', 'a', 'Tiles/NormalTile', 'Normal', 1);

insert into Terrain (Description, EntityCode, Name, TextureFilePath, TypeID)
values ('It''s not a mat!', 'b', 'Tiles/BlackTile', 'Inaccessible', 1);

create table Room(
	ID integer NOT NULL primary key,
	XPWorth integer,
	TerrainGridString varchar(255),
	Visible boolean
); 

create table Entity (
	ID integer NOT NULL primary key,
	Name varchar(255),
	AvatarFilePath varchar(255)
);

create table Campaign(
	ID integer NOT NULL primary key,
	Name varchar(255)
);

create table CampaignRoomLink (
	CampaignID integer NOT NULL,
	FirstRoomID integer NOT NULL,
	SecondRoomID integer NOT NULL,
	PRIMARY KEY (CampaignID, FirstRoomID, SecondRoomID),
	foreign key (CampaignID) references Campaign(ID),
	foreign key (FirstRoomID) references Room(ID),
	foreign key (SecondRoomID) references Room(ID)
);

create table RoomEntity (
	RoomID integer NOT NULL,
	EntityID integer NOT NULL,
	X integer,
	Y integer,
	PRIMARY KEY (RoomID, EntityID),
	foreign key (RoomID) references Room(ID),
	foreign key (EntityID) references Entity(ID)
);
commit;




