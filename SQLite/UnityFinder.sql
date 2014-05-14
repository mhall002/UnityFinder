begin transaction;

drop table if exists Terrain;
drop table if exists TerrainState;

drop table if exists Status;
drop table if exists Entity;
drop table if exists EntityAbility;

drop table if exists Ability;
drop table if exists Campaign;
drop table if exists CampaignCharacter;
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
values ('It''s a mat!', 'a', 'NormalTile', 'Normal', 1);

insert into Terrain (Description, EntityCode, Name, TextureFilePath, TypeID)
values ('It''s not a mat!', 'b', 'BlackTile', 'Inaccessible', 1);

insert into Terrain (Description, EntityCode, Name, TextureFilePath, TypeID)
values ('It''s not a mat!', 'c', 'RailVertical', 'Normal2', 1);

insert into Terrain (Description, EntityCode, Name, TextureFilePath, TypeID)
values ('It''s not a mat!', 'd', 'RailHorizontal', 'Normal3', 1);

create table Room(
	ID integer NOT NULL primary key,
	CampaignID integer NOT NULL,
	XPWorth integer,
	TerrainGridString varchar(255),
	Visible boolean,
	foreign key (CampaignID) references Campaign(ID)
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

create table CampaignCharacter (
	CampaignID integer NOT NULL,
	EntityID integer NOT NULL,
	PRIMARY KEY(CampaignID, EntityID),
	foreign key (CampaignID) references Campaign(ID),
	foreign key (EntityID) references Entity(ID)
);
commit;




