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

--creating the Table called Terrain
create table facilities(
	ID integer NOT NULL primary key,
	Description varchar(MAX),
	Name varchar(255),
	TextureFilePath varchar(255)
	);


--creating the Table called lines
create table lines (
	portid integer NOT NULL primary key,
	areacode varchar(3),
	officecode varchar(3),
	stationcode varchar(4),
	state varchar(4) NOT NULL, 
	foreign key (portid)  references facilities(portid) on delete cascade
	);

--inserting data into table lines
insert into lines values (1, '613', '134', '0001', 'BUSY');
insert into lines values (2, '613', '136', '0002', 'BUSY');
insert into lines values (3, '613', '156', '0003', 'IDLE');
insert into lines values (4, '613', '220', '0004', 'BUSY');
insert into lines values (5, '613', '221', '0005', 'BUSY');
insert into lines values (6, '613', '222', '0006', 'BUSY');
insert into lines values (7, '613', '223', '0007', 'IDLE');
insert into lines values (8, '613', '226', '0008', 'BUSY');
insert into lines values (9, '613', '227', '0009', 'BUSY');
insert into lines values (10, '613', '229', '0010', 'IDLE');
insert into lines values (11, '613', '310', '0011', 'IDLE');
insert into lines values (12, '613', '322', '0012', 'IDLE');
insert into lines values (13, '613', '333', '0013', 'BUSY');
insert into lines values (14, '613', '334', '0014', 'IDLE');
insert into lines values (15, '613', '389', '0015', 'BUSY');
insert into lines values (16, '613', '457', '0016', 'IDLE');
insert into lines values (17, '613', '489', '0017', 'IDLE');
insert into lines values (18, '613', '523', '0018', 'BUSY');
insert into lines values (19, '613', '568', '0019', 'IDLE');
insert into lines values (20, '613', '578', '0020', 'IDLE');
insert into lines values (21, '613', '623', '0021', 'BUSY');
insert into lines values (22, '613', '645', '0022', 'BUSY');
insert into lines values (23, '613', '657', '0023', 'IDLE');
insert into lines values (24, '613', '712', '0024', 'BUSY');
insert into lines values (25, '613', '728', '0025', 'BUSY');
insert into lines values (26, '613', '798', '0026', 'IDLE');
insert into lines values (27, '416', '219', '0027', 'IDLE');
insert into lines values (28, '416', '331', '0028', 'IDLE');
insert into lines values (29, '416', '333', '0029', 'IDLE');
insert into lines values (30, '416', '334', '0030', 'IDLE');
insert into lines values (31, '416', '756', '0031', 'IDLE');
insert into lines values (32, '705', '221', '0032', 'IDLE');
insert into lines values (33, '905', '347', '0033', 'IDLE');
insert into lines values (34, '905', '657', '0034', 'IDLE');
insert into lines values (35, '905', '819', '0035', 'IDLE');
insert into lines values (36, '819', '223', '0036', 'IDLE');
insert into lines values (37, '819', '227', '0037', 'IDLE');
insert into lines values (38, '705', '221', '0038', 'IDLE');
insert into lines values (39, '819', '223', '0039', 'IDLE');
insert into lines values (40, '819', '227', '0040', 'IDLE');
insert into lines values (41, '905', '347', '0041', 'IDLE');
insert into lines values (42, '905', '657', '0042', 'IDLE');
insert into lines values (43, '905', '819', '0043', 'IDLE');
insert into lines values (44, '416', '219', '0044', 'IDLE');
insert into lines values (45, '416', '331', '0045', 'IDLE');
insert into lines values (46, '416', '333', '0046', 'IDLE');
insert into lines values (47, '416', '334', '0047', 'IDLE');
insert into lines values (48, '416', '756', '0048', 'IDLE');
insert into lines values (49, '705', '221', '0049', 'IDLE');
insert into lines values (50, '819', '227', '0050', 'IDLE');


--creating the table called trunks
create table trunks (
	portid integer NOT NULL primary key,
	foreign_switch varchar(15),
	foreign key (portid)  references facilities(portid) on delete cascade
	);

--inserting data into table trunks
insert into trunks values ( 100, 'KANATA');
insert into trunks values ( 101, 'ORLEANS');
insert into trunks values ( 102, 'RIDEAU');
insert into trunks values ( 103, 'BASELINE');
insert into trunks values ( 104, 'SOMERSET');
insert into trunks values ( 105, 'NEPEAN');
insert into trunks values ( 106, 'BAY');
insert into trunks values ( 107, 'Innes');


--creating the table called treatments
create table treatments (
	tcode varchar(3) NOT NULL primary key,
	portid integer,treatment_name varchar(20),
	foreign key (portid) references facilities(portid) on delete cascade
	);

--inserting data for table treatment 
insert into treatments values ( 'BSY', 500, 'CALLED PARTY BUSY');
insert into treatments values ( 'CBY', 500, 'CIRCUITS BUSY');
insert into treatments values ( 'NSV', 510, 'NO SERVICE');
insert into treatments values ( 'INV', 520, 'INVALID NUMBER');


--creating the table called Service
create table services (
	scode varchar(3) NOT NULL primary key,
	service varchar(25)
	);

--inserting data for table services
insert into services values ( 'CWT', 'Call Waiting');
insert into services values ( 'CFB', 'Call Forward Busy');
insert into services values ( '3WC', 'Conference Call');
insert into services values ( 'MSG', 'Message Answer');
insert into services values ( 'DSP', 'Call Number Display');


--creating a table called service_subscribers
create table service_subscribers(
	line integer NOT NULL references facilities(portid),
	service varchar(3) NOT NULL references 	services(scode),
	primary key(line, service)
	);

--inserting data for table service_suscribers
insert into service_subscribers values (1, 'MSG');
insert into service_subscribers values (3, 'MSG');
insert into service_subscribers values (5, 'MSG');
insert into service_subscribers values (7, 'MSG');
insert into service_subscribers values (9, 'MSG');
insert into service_subscribers values (11, 'MSG');
insert into service_subscribers values (13, 'MSG');
insert into service_subscribers values (15, 'MSG');
insert into service_subscribers values (17, 'MSG');
insert into service_subscribers values (19, 'MSG');
insert into service_subscribers values (21, 'MSG');
insert into service_subscribers values (23, 'MSG');
insert into service_subscribers values (25, 'MSG');
insert into service_subscribers values (27, 'MSG');
insert into service_subscribers values (29, 'MSG');
insert into service_subscribers values (31, 'MSG');
insert into service_subscribers values (33, 'MSG');
insert into service_subscribers values (35, 'MSG');
insert into service_subscribers values (37, 'MSG');
insert into service_subscribers values (39, 'MSG');
insert into service_subscribers values (41, 'MSG');
insert into service_subscribers values (43, 'MSG');
insert into service_subscribers values (45, 'MSG');
insert into service_subscribers values (47, 'MSG');
insert into service_subscribers values (49, 'MSG');
insert into service_subscribers values (2, 'CWT');
insert into service_subscribers values (4, 'CWT');
insert into service_subscribers values (6, 'CWT');
insert into service_subscribers values (8, 'CWT');
insert into service_subscribers values (10, 'CWT');
insert into service_subscribers values (12, 'CWT');
insert into service_subscribers values (14, 'CWT');
insert into service_subscribers values (16, 'CWT');
insert into service_subscribers values (18, 'CWT');
insert into service_subscribers values (20, 'CWT');
insert into service_subscribers values (32, 'CWT');
insert into service_subscribers values (34, 'CWT');
insert into service_subscribers values (36, 'CWT');
insert into service_subscribers values (38, 'CWT');
insert into service_subscribers values (40, 'CWT');
insert into service_subscribers values (42, 'CWT');
insert into service_subscribers values (44, 'CWT');
insert into service_subscribers values (46, 'CWT');
insert into service_subscribers values (48, 'CWT');
insert into service_subscribers values (50, 'CWT');
insert into service_subscribers values (1, 'CFB');
insert into service_subscribers values (4, 'CFB');
insert into service_subscribers values (7, 'CFB');
insert into service_subscribers values (10, 'CFB');
insert into service_subscribers values (13, 'CFB');
insert into service_subscribers values (16, 'CFB');
insert into service_subscribers values (19, 'CFB');
insert into service_subscribers values (22, 'CFB');
insert into service_subscribers values (25, 'CFB');
insert into service_subscribers values (28, 'CFB');
insert into service_subscribers values (31, 'CFB');
insert into service_subscribers values (34, 'CFB');
insert into service_subscribers values (37, 'CFB');
insert into service_subscribers values (40, 'CFB');
insert into service_subscribers values (43, 'CFB');
insert into service_subscribers values (46, 'CFB');
insert into service_subscribers values (49, 'CFB');
insert into service_subscribers values (3, 'DSP');
insert into service_subscribers values (6, 'DSP');
insert into service_subscribers values (9, 'DSP');
insert into service_subscribers values (12, 'DSP');
insert into service_subscribers values (15, 'DSP');
insert into service_subscribers values (18, 'DSP');
insert into service_subscribers values (21, 'DSP');
insert into service_subscribers values (24, 'DSP');
insert into service_subscribers values (27, 'DSP');
insert into service_subscribers values (30, 'DSP');
insert into service_subscribers values (33, 'DSP');
insert into service_subscribers values (36, 'DSP');
insert into service_subscribers values (39, 'DSP');
insert into service_subscribers values (42, 'DSP');
insert into service_subscribers values (45, 'DSP');
insert into service_subscribers values (48, 'DSP');
insert into service_subscribers values (2, '3WC');
insert into service_subscribers values (6, '3WC');
insert into service_subscribers values (10, '3WC');
insert into service_subscribers values (18, '3WC');
insert into service_subscribers values (21, '3WC');
insert into service_subscribers values (28, '3WC');
insert into service_subscribers values (29, '3WC');
insert into service_subscribers values (30, '3WC');
insert into service_subscribers values (33, '3WC');
insert into service_subscribers values (37, '3WC');
insert into service_subscribers values (43, '3WC');
insert into service_subscribers values (48, '3WC');


--creating table called subscribers
create table subscribers (
	portid integer NOT NULL,name varchar(30) NOT NULL,
	address varchar(50),
	primary key (portid, name),
	foreign key (portid)  references facilities(portid) on delete cascade
	);

--inserting data into table subscribers
insert into subscribers values( 1, 'Mats Sundin', '45 Elgin St.');
insert into subscribers values( 2, 'Jason Allison', '46 Elgin St.');
insert into subscribers values( 3, 'Eric Lindros', '48 Elgin St.');
insert into subscribers values( 4, 'Bryan MacCabe', '23 MacLeod St.');
insert into subscribers values( 5, 'Steve Nash', '1129 Otterson Dr.');
insert into subscribers values( 6, 'Michael Jordan', '1223 Carling Ave.');
insert into subscribers values( 7, 'Roger Clemens', '14 Hopewell Ave.');
insert into subscribers values( 8, 'Jack Morris', '23 Prince of Wales Dr.');
insert into subscribers values( 9, 'Roberto Alomar', '55 Moodie Dr.');
insert into subscribers values( 10, 'Joe Carter', '18 Summerset East.');
insert into subscribers values( 11, 'Wayne Grekzy', '45 Merviale.');
insert into subscribers values( 12, 'George Bell', '46 Colon By Dr.');
insert into subscribers values( 13, 'Eric Staal', '423 Riverside Drive.');
insert into subscribers values( 14, 'Roy Halladay', '23 Ogilvie Rd.');
insert into subscribers values( 15, 'Mario Lemieux', '1129 Bank Dr.');
insert into subscribers values( 16, 'Patrick Roy', '1223 Greenbank Ave.');
insert into subscribers values( 17, 'Martin Brodeur', '14 Baseline Ave.');
insert into subscribers values( 18, 'Homer Simpson', '123 Prince of Wales Dr.');
insert into subscribers values( 19, 'Bart Simpson', '155 Moodie Dr.');
insert into subscribers values( 20, 'Joe Johnson', '21 Sussex Dr.');
insert into subscribers values( 21, 'Vince Carter', '45 Hunt Club St.');
insert into subscribers values( 22, 'Ed Belfour', '46 Fisher St.');
insert into subscribers values( 23, 'Bobby Orr', '48 Prince Edward St.');
insert into subscribers values( 24, 'Bobby Hull', '23 Wellington St.');
insert into subscribers values( 25, 'Gordie Howe', '1129 Preston Dr.');
insert into subscribers values( 26, 'Larry Bird', '1223 Warden Ave.');
insert into subscribers values( 27, 'Mark Messier', '14 Finch Ave.');
insert into subscribers values( 28, 'Wade Redden', '23 Steeles Ave.');
insert into subscribers values( 29, 'Sidney Crosby', '55 Sheppard Ave.');
insert into subscribers values( 30, 'Peter Forsberg', '1 Kennedy Dr.');
insert into subscribers values( 31, 'Paul Kariya', '45 Midland.');
insert into subscribers values( 32, 'Rob Blake', '12 19th Ave.');
insert into subscribers values( 33, 'Chris Pronger', '48 16th Ave.');
insert into subscribers values( 34, 'Gary Roberts', '23 John St.');
insert into subscribers values( 35, 'Alex Mogily', '1129 14th Ave.');
insert into subscribers values( 36, 'Scott Gomez', '1223 Montreal Ave.');
insert into subscribers values( 37, 'Frank Thomas', '14 Hull Ave.');
insert into subscribers values( 38, 'Barry Bonds', '23 Hood Rd.');
insert into subscribers values( 39, 'Hank Aaron', '55 Denison Ave.');
insert into subscribers values( 40, 'Babe Ruth', '1 Old Kennedy.');
insert into subscribers values( 41, 'Ted Williams', '45 Birchmount Ave.');
insert into subscribers values( 42, 'Chris Bosh', '46 Queens Ave.');
insert into subscribers values( 43, 'Steve Sampras', '448 St Clare Ave.');
insert into subscribers values( 44, 'Kobe Bryan', '23 Bayview St.');
insert into subscribers values( 45, 'Kevin Garnett', '59 Mike Myers Dr.');
insert into subscribers values( 46, 'Larry Brown', '99 Blue Jays Ave.');
insert into subscribers values( 47, 'Brooke Shields', '69 College Ave.');
insert into subscribers values( 48, 'Matt Stajan', '50 LakeShore Ave.');
insert into subscribers values( 49, 'Tie Domi', '89 Spidina Rd.');
insert into subscribers values( 50, 'Tevor Kidd', '30 McCowan Ave.');


--creating a table called trunk_channels
create table trunk_channels (
	portid integer NOT NULL,
	channel integer NOT NULL, 
	state char(4) default 'IDLE',
	primary key(portid, channel),
	foreign key (portid)  references trunks(portid) on delete cascade
	);

--inserting data into table trunk Channels
insert into trunk_channels values (100, 0, 'IDLE'); 
insert into trunk_channels values (100, 1, 'IDLE');
insert into trunk_channels values (100, 2, 'IDLE');
insert into trunk_channels values (100, 3, 'IDLE');
insert into trunk_channels values (100, 4, 'IDLE');
insert into trunk_channels values (100, 5, 'IDLE');
insert into trunk_channels values (100, 6, 'IDLE');
insert into trunk_channels values (100, 7, 'IDLE');
insert into trunk_channels values (100, 8, 'IDLE');
insert into trunk_channels values (100, 9, 'IDLE');
insert into trunk_channels values (100, 10, 'IDLE');
insert into trunk_channels values (100, 11, 'IDLE');
insert into trunk_channels values (100, 12, 'IDLE');
insert into trunk_channels values (100, 13, 'IDLE');
insert into trunk_channels values (100, 14, 'IDLE');
insert into trunk_channels values (100, 15, 'IDLE');
insert into trunk_channels values (100, 16, 'IDLE');
insert into trunk_channels values (100, 17, 'IDLE');
insert into trunk_channels values (100, 18, 'IDLE');
insert into trunk_channels values (100, 19, 'IDLE');
insert into trunk_channels values (100, 20, 'IDLE');
insert into trunk_channels values (100, 21, 'IDLE');
insert into trunk_channels values (100, 22, 'IDLE');
insert into trunk_channels values (100, 23, 'IDLE');
insert into trunk_channels values (101, 0, 'IDLE'); 
insert into trunk_channels values (101, 1, 'IDLE');
insert into trunk_channels values (101, 2, 'IDLE');
insert into trunk_channels values (101, 3, 'IDLE');
insert into trunk_channels values (101, 4, 'IDLE');
insert into trunk_channels values (101, 5, 'IDLE');
insert into trunk_channels values (101, 6, 'IDLE');
insert into trunk_channels values (101, 7, 'IDLE');
insert into trunk_channels values (101, 8, 'IDLE');
insert into trunk_channels values (101, 9, 'IDLE');
insert into trunk_channels values (101, 10, 'IDLE');
insert into trunk_channels values (101, 11, 'IDLE');
insert into trunk_channels values (101, 12, 'IDLE');
insert into trunk_channels values (101, 13, 'IDLE');
insert into trunk_channels values (101, 14, 'IDLE');
insert into trunk_channels values (101, 15, 'IDLE');
insert into trunk_channels values (101, 16, 'IDLE');
insert into trunk_channels values (101, 17, 'IDLE');
insert into trunk_channels values (101, 18, 'IDLE');
insert into trunk_channels values (101, 19, 'IDLE');
insert into trunk_channels values (101, 20, 'IDLE');
insert into trunk_channels values (101, 21, 'IDLE');
insert into trunk_channels values (101, 22, 'IDLE');
insert into trunk_channels values (101, 23, 'IDLE');
insert into trunk_channels values (102, 0, 'IDLE'); 
insert into trunk_channels values (102, 1, 'IDLE');
insert into trunk_channels values (102, 2, 'BUSY');
insert into trunk_channels values (102, 3, 'IDLE');
insert into trunk_channels values (102, 4, 'IDLE');
insert into trunk_channels values (102, 5, 'IDLE');
insert into trunk_channels values (102, 6, 'IDLE');
insert into trunk_channels values (102, 7, 'IDLE');
insert into trunk_channels values (102, 8, 'IDLE');
insert into trunk_channels values (102, 9, 'IDLE');
insert into trunk_channels values (102, 10, 'IDLE');
insert into trunk_channels values (102, 11, 'IDLE');
insert into trunk_channels values (102, 12, 'IDLE');
insert into trunk_channels values (102, 13, 'BUSY');
insert into trunk_channels values (102, 14, 'IDLE');
insert into trunk_channels values (102, 15, 'IDLE');
insert into trunk_channels values (102, 16, 'IDLE');
insert into trunk_channels values (102, 17, 'IDLE');
insert into trunk_channels values (102, 18, 'IDLE');
insert into trunk_channels values (102, 19, 'IDLE');
insert into trunk_channels values (102, 20, 'IDLE');
insert into trunk_channels values (102, 21, 'BUSY');
insert into trunk_channels values (102, 22, 'IDLE');
insert into trunk_channels values (102, 23, 'IDLE');
insert into trunk_channels values (103, 0, 'IDLE'); 
insert into trunk_channels values (103, 1, 'IDLE');
insert into trunk_channels values (103, 2, 'IDLE');
insert into trunk_channels values (103, 3, 'BUSY');
insert into trunk_channels values (103, 4, 'IDLE');
insert into trunk_channels values (103, 5, 'IDLE');
insert into trunk_channels values (103, 6, 'BUSY');
insert into trunk_channels values (103, 7, 'IDLE');
insert into trunk_channels values (103, 8, 'IDLE');
insert into trunk_channels values (103, 9, 'IDLE');
insert into trunk_channels values (103, 10, 'IDLE');
insert into trunk_channels values (103, 11, 'BUSY');
insert into trunk_channels values (103, 12, 'IDLE');
insert into trunk_channels values (103, 13, 'IDLE');
insert into trunk_channels values (103, 14, 'IDLE');
insert into trunk_channels values (103, 15, 'IDLE');
insert into trunk_channels values (103, 16, 'IDLE');
insert into trunk_channels values (103, 17, 'IDLE');
insert into trunk_channels values (103, 18, 'IDLE');
insert into trunk_channels values (103, 19, 'IDLE');
insert into trunk_channels values (103, 20, 'IDLE');
insert into trunk_channels values (103, 21, 'IDLE');
insert into trunk_channels values (103, 22, 'IDLE');
insert into trunk_channels values (103, 23, 'IDLE');
insert into trunk_channels values (104, 0, 'IDLE'); 
insert into trunk_channels values (104, 1, 'IDLE');
insert into trunk_channels values (104, 2, 'IDLE');
insert into trunk_channels values (104, 3, 'IDLE');
insert into trunk_channels values (104, 4, 'IDLE');
insert into trunk_channels values (104, 5, 'IDLE');
insert into trunk_channels values (104, 6, 'IDLE');
insert into trunk_channels values (104, 7, 'IDLE');
insert into trunk_channels values (104, 8, 'IDLE');
insert into trunk_channels values (104, 9, 'BUSY');
insert into trunk_channels values (104, 10, 'IDLE');
insert into trunk_channels values (104, 11, 'IDLE');
insert into trunk_channels values (104, 12, 'IDLE');
insert into trunk_channels values (104, 13, 'IDLE');
insert into trunk_channels values (104, 14, 'IDLE');
insert into trunk_channels values (104, 15, 'IDLE');
insert into trunk_channels values (104, 16, 'IDLE');
insert into trunk_channels values (104, 17, 'IDLE');
insert into trunk_channels values (104, 18, 'IDLE');
insert into trunk_channels values (104, 19, 'IDLE');
insert into trunk_channels values (104, 20, 'IDLE');
insert into trunk_channels values (104, 21, 'IDLE');
insert into trunk_channels values (104, 22, 'IDLE');
insert into trunk_channels values (104, 23, 'IDLE');
insert into trunk_channels values (105, 0, 'IDLE'); 
insert into trunk_channels values (105, 1, 'IDLE');
insert into trunk_channels values (105, 2, 'IDLE');
insert into trunk_channels values (105, 3, 'IDLE');
insert into trunk_channels values (105, 4, 'IDLE');
insert into trunk_channels values (105, 5, 'IDLE');
insert into trunk_channels values (105, 6, 'IDLE');
insert into trunk_channels values (105, 7, 'BUSY');
insert into trunk_channels values (105, 8, 'IDLE');
insert into trunk_channels values (105, 9, 'IDLE');
insert into trunk_channels values (105, 10, 'BUSY');
insert into trunk_channels values (105, 11, 'IDLE');
insert into trunk_channels values (105, 12, 'IDLE');
insert into trunk_channels values (105, 13, 'IDLE');
insert into trunk_channels values (105, 14, 'IDLE');
insert into trunk_channels values (105, 15, 'IDLE');
insert into trunk_channels values (105, 16, 'IDLE');
insert into trunk_channels values (105, 17, 'IDLE');
insert into trunk_channels values (105, 18, 'IDLE');
insert into trunk_channels values (105, 19, 'IDLE');
insert into trunk_channels values (105, 20, 'BUSY');
insert into trunk_channels values (105, 21, 'IDLE');
insert into trunk_channels values (105, 22, 'IDLE');
insert into trunk_channels values (105, 23, 'BUSY');
insert into trunk_channels values (106, 0, 'IDLE'); 
insert into trunk_channels values (106, 1, 'IDLE');
insert into trunk_channels values (106, 2, 'IDLE');
insert into trunk_channels values (106, 3, 'IDLE');
insert into trunk_channels values (106, 4, 'IDLE');
insert into trunk_channels values (106, 5, 'IDLE');
insert into trunk_channels values (106, 6, 'BUSY');
insert into trunk_channels values (106, 7, 'IDLE');
insert into trunk_channels values (106, 8, 'IDLE');
insert into trunk_channels values (106, 9, 'IDLE');
insert into trunk_channels values (106, 10, 'IDLE');
insert into trunk_channels values (106, 11, 'IDLE');
insert into trunk_channels values (106, 12, 'IDLE');
insert into trunk_channels values (106, 13, 'IDLE');
insert into trunk_channels values (106, 14, 'IDLE');
insert into trunk_channels values (106, 15, 'IDLE');
insert into trunk_channels values (106, 16, 'IDLE');
insert into trunk_channels values (106, 17, 'IDLE');
insert into trunk_channels values (106, 18, 'IDLE');
insert into trunk_channels values (106, 19, 'IDLE');
insert into trunk_channels values (106, 20, 'IDLE');
insert into trunk_channels values (106, 21, 'IDLE');
insert into trunk_channels values (106, 22, 'IDLE');
insert into trunk_channels values (106, 23, 'IDLE');
insert into trunk_channels values (107, 0, 'IDLE'); 
insert into trunk_channels values (107, 1, 'IDLE');
insert into trunk_channels values (107, 2, 'IDLE');
insert into trunk_channels values (107, 3, 'IDLE');
insert into trunk_channels values (107, 4, 'BUSY');
insert into trunk_channels values (107, 5, 'BUSY');
insert into trunk_channels values (107, 6, 'IDLE');
insert into trunk_channels values (107, 7, 'IDLE');
insert into trunk_channels values (107, 8, 'IDLE');
insert into trunk_channels values (107, 9, 'IDLE');
insert into trunk_channels values (107, 10, 'IDLE');
insert into trunk_channels values (107, 11, 'IDLE');
insert into trunk_channels values (107, 12, 'IDLE');
insert into trunk_channels values (107, 13, 'IDLE');
insert into trunk_channels values (107, 14, 'IDLE');
insert into trunk_channels values (107, 15, 'IDLE');
insert into trunk_channels values (107, 16, 'IDLE');
insert into trunk_channels values (107, 17, 'IDLE');
insert into trunk_channels values (107, 18, 'IDLE');
insert into trunk_channels values (107, 19, 'IDLE');
insert into trunk_channels values (107, 20, 'IDLE');
insert into trunk_channels values (107, 21, 'IDLE');
insert into trunk_channels values (107, 22, 'IDLE');
insert into trunk_channels values (107, 23, 'IDLE');


--creating a table called Calls
create table calls (
	call_id integer NOT NULL primary key,
	orig integer,
	term integer,
	och integer,
	tch integer,
	area varchar(3),
	offi varchar(3),
	stn varchar(4),
	foreign key (orig)  references facilities(portid) on delete cascade,
	foreign key (term)  references facilities(portid) on delete cascade
	);

--inserting data into table calls
insert into calls values (001, 1, 5, 0, 0, '613', '221', '0005');
insert into calls values (002, 2, 25, 0, 0, '613', '728', '0025');
insert into calls values (003, 18, 15, 0, 0, '613', '389', '0015');
insert into calls values (004, 6, 9, 0, 0, '613', '227', '0009');
insert into calls values (005, 22, 24, 0, 0, '613', '712', '0024');
insert into calls values (006, 8,102 , 0, 2, '416', '333', '0029');
insert into calls values (008, 21, 105, 0, 7, '705', '221', '0032');
insert into calls values (009, 4, 105, 0, 20, '905', '657', '0034');
insert into calls values (011, 13, 103, 0, 11, '819', '227', '0037');
insert into calls values (012, 105,102 , 10, 13, '416', '334', '0030');
insert into calls values (013, 106, 104, 6, 9, '613', '457', '0016');
insert into calls values (014, 102, 105, 21, 23, '705', '221', '0038');
insert into calls values (015, 103, 107, 3, 5, '905', '819', '0035');
insert into calls values (016, 107, 103, 4, 6, '819', '223', '0039');




--create table called trunk_routes
create table trunk_routes (
	portid integer NOT NULL,area varchar(3) NOT NULL,
	office varchar(3) NOT NULL,
	primary key (portid, area, office),
	foreign key (portid)  references trunks(portid) on delete cascade
	);

-- For each trunk facility it will only support dialed digits list in the table 
-- A trunk can support several distinct sets of numbers and any set of numbers can be supported by several trunks.
-- Table matches trunk portid'w with area & office code 
-- 1.If dialed digits of a call (area & office) matches those in a table row, that trunk can be used 
-- 2.If area code matches & office code = 000, trunk can be used but less preferred than 1
-- 3.A trunk with AREA = 000 & OFFICE = 000 can be used for any call but is least preferred 

--inserting data into table trunk routes
insert into trunk_routes values (100, '613', '220');
insert into trunk_routes values (100, '613', '221');
insert into trunk_routes values (100, '613', '222');
insert into trunk_routes values (100, '613', '223');
insert into trunk_routes values (100, '613', '225');
insert into trunk_routes values (100, '613', '227');
insert into trunk_routes values (101, '613', '310');
insert into trunk_routes values (101, '613', '322');
insert into trunk_routes values (101, '613', '333');
insert into trunk_routes values (101, '613', '334');
insert into trunk_routes values (101, '613', '389');
insert into trunk_routes values (102, '613', '000');
insert into trunk_routes values (102, '416', '000');
insert into trunk_routes values (102, '416', '333');
insert into trunk_routes values (102, '416', '334');
insert into trunk_routes values (102, '416', '331');
insert into trunk_routes values (102, '613', '229');
insert into trunk_routes values (103, '613', '134');
insert into trunk_routes values (103, '613', '136');
insert into trunk_routes values (103, '613', '156');
insert into trunk_routes values (103, '819', '227');
insert into trunk_routes values (103, '819', '228');
insert into trunk_routes values (103, '819', '223');
insert into trunk_routes values (104, '613', '457');
insert into trunk_routes values (104, '613', '489');
insert into trunk_routes values (104, '905', '000');
insert into trunk_routes values (104, '514', '000');
insert into trunk_routes values (104, '514', '654');
insert into trunk_routes values (104, '514', '123');
insert into trunk_routes values (105, '613', '568');
insert into trunk_routes values (105, '613', '578');
insert into trunk_routes values (105, '613', '523');
insert into trunk_routes values (105, '705', '000');
insert into trunk_routes values (105, '705', '221');
insert into trunk_routes values (105, '905', '000');
insert into trunk_routes values (105, '613', '657');
insert into trunk_routes values (105, '613', '623');
insert into trunk_routes values (105, '613', '645');
insert into trunk_routes values (106, '416', '219');
insert into trunk_routes values (106, '416', '756');
insert into trunk_routes values (106, '416', '000');
insert into trunk_routes values (105, '905', '657');
insert into trunk_routes values (107, '905', '347');
insert into trunk_routes values (107, '905', '819');
insert into trunk_routes values (107, '000', '000');
insert into trunk_routes values (107, '613', '000');
insert into trunk_routes values (107, '613', '712');
insert into trunk_routes values (107, '613', '728');
insert into trunk_routes values (107, '613', '798');

commit;




