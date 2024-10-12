create table employee
(
	id uuid not null
		constraint employee_pk
			primary key,
	fname varchar(100) not null,
	lname varchar(100) not null,
	mname varchar(100) not null,
	bdate date not null,
	passport varchar(1000) not null,
	telephone varchar(50) not null,
	address varchar(1000) not null,
	position varchar(1000) not null,
	department varchar(1000) not null,
	salary integer not null,
	contract varchar(1000) not null,
	date_insert timestamp not null,
	date_update timestamp not null,
	date_hiring date not null
);


create table client
(
	id uuid not null
		constraint client_pk
			primary key,
	fname varchar(100) not null,
	lname varchar(100) not null,
	mname varchar(100) not null,
	bdate date not null,
	passport varchar(1000) not null,
	telephone varchar(50) not null,
	address varchar(1000) not null,
	date_insert timestamp not null,
	date_update timestamp not null
);

create table account
(
	id uuid not null
		constraint account_pk
			primary key,
	currency_name varchar(20) not null,
	amount integer not null,
	client_id uuid not null,
	date_insert timestamp not null,
	date_update timestamp not null
		constraint account_client_id_fk
			references client
				on update cascade on delete cascade
)


INSERT INTO employee (id, fname, lname, mname, bdate, passport, address, salary, contract, date_hiring, date_insert, date_update, position, department, telephone)
VALUES
  ('73640815-B01D-5C1F-6B5A-A49475102D78','Nathan','Larissa','Felix','1989-02-05','VEQ696036','242-429 Diam St.',8443,'Apr 7, 2025','2023-12-28','2024-09-23 02:27:41','2024-09-14 11:28:50', 'pos1', 'dep1','1-576-437-8746'),
  ('E5BC206A-6F2C-11AF-C4CA-8121C1A7C52B','Quamar','Hilary','Macey','2001-11-06','NDV262506','Ap #476-6381 Mauris, St.',4098,'Apr 9, 2024','2024-04-30','2023-12-25 09:00:17','2024-08-10 19:26:49', 'pos2', 'dep2','1-754-313-7228'),
  ('8C93B9AD-675B-ED25-1BB3-8766591C2CE1','Jane','Sheila','Kelsie','1986-10-14','WPF614355','Ap #619-7508 Commodo Rd.',6416,'Mar 31, 2024','2021-06-25','2024-01-14 17:44:48','2024-08-23 07:34:49', 'pos1', 'dep1','(885) 267-8284'),
  ('8CAE9CA3-A58A-7F73-4B1C-40D6D75685CC','Ebony','Kaseem','Matthew','1994-02-19','VTB268331','P.O. Box 339, 2279 Scelerisque Rd.',3951,'Jun 30, 2024','2024-09-04','2024-08-06 00:17:48','2024-03-11 15:36:00', 'pos3', 'dep3','(526) 169-3324'),
  ('E639EECC-FF61-E556-4165-19C15C621366','Charissa','Priscilla','Cameron','1988-03-17','DLL511658','153-2770 Phasellus Road',9662,'Apr 29, 2025','2024-04-15','2023-11-04 17:58:22','2024-12-31 17:18:26', 'pos1', 'dep1','(782) 672-1973'),
  ('C19317AE-D765-F9AB-7809-3529C4A951C3','Vladimir','Caryn','Astra','1991-12-29','PMS506153','Ap #354-9938 Turpis St.',2204,'Apr 29, 2024','2024-04-26','2024-09-21 23:27:55','2024-08-06 01:59:20', 'pos2', 'dep2','(711) 295-4718'),
  ('B8DD015D-68EB-B618-3617-33F814B28639','Halla','Joel','Castor','1998-08-10','XNY468647','Ap #540-4604 Aliquam Ave',1452,'Jun 27, 2025','2024-07-17','2024-05-06 09:11:13','2025-10-11 13:47:16', 'pos4', 'dep4','1-645-712-8112'),
  ('AD2EA15C-E884-C02C-FE58-28325CD54ABA','Bevis','Samuel','Keefe','1995-10-15','IGH694916','P.O. Box 414, 5816 Eleifend Street',2138,'May 10, 2024','2021-01-07','2025-05-28 10:37:43','2024-10-12 19:52:01', 'pos5', 'dep5','(715) 955-3474'),
  ('15F1CD5E-4448-E9AB-927C-DA74A5274549','Gisela','Nichole','Shelby','1997-08-04','ZBR444563','Ap #256-5267 Libero. St.',1462,'Feb 17, 2024','2021-05-27','2023-12-20 13:14:31','2024-02-17 00:11:15', 'pos6', 'dep6','1-560-636-6674'),
  ('BD9C8CA5-42F2-E5A7-6D41-05D51CEB28D2','Cassady','Danielle','David','1993-04-07','QYS520111','Ap #137-4516 Sem St.',2824,'Nov 28, 2023','2023-05-29','2024-05-02 08:34:47','2023-11-20 09:52:48', 'pos6', 'dep6','1-477-852-8182');


INSERT INTO client (id, fname, lname, mname, bdate, passport, address, date_insert, date_update, telephone)
VALUES
  ('A3A46D33-4235-B9ED-0BD7-3AC47649C0C3','Hamilton','Roth','Shana','1993-07-22','RWS932886','227-2081 Dictum Ave','Apr 17, 2024','2025-05-07 14:05:26','2025-02-09 06:28:38','(287) 815-7629'),
  ('A9CEBCD8-67E7-2C26-D63D-72DFCB69E771','Tara','Miranda','Hadley','1988-01-21','GMV463873','814-3992 Cras Street','Aug 18, 2025','2024-09-16 10:15:31','2024-09-18 16:11:11','1-848-818-1460'),
  ('3ED442BA-A238-EF7B-E684-A225EC59F61B','Hedda','Slade','Camden','1999-12-02','MEW114594','625-6446 Egestas Av.','Jul 22, 2024','2024-05-25 13:32:14','2024-08-30 00:58:35','(473) 489-6494'),
  ('E0558FB9-F913-BA0C-6637-15CDF4678633','Kelsey','MacKensie','Desirae','2004-02-06','IYI172753','P.O. Box 809, 2633 Mi, St.','Jan 15, 2025','2025-09-24 06:12:09','2025-09-22 09:01:49','(321) 678-0790'),
  ('148DBA5A-9856-6A84-29B5-D85236694C84','Grant','Gary','Kyra','1987-06-14','KJP164073','Ap #608-1553 Dictum Avenue','Dec 23, 2023','2024-02-20 02:12:54','2024-04-19 10:20:44','(415) 849-3210'),
  ('23B47A4F-4AED-8226-A744-107BD15F6A1A','Flynn','Ashton','Alika','2002-08-02','VXA458148','P.O. Box 592, 3481 Parturient Rd.','Jul 1, 2025','2024-01-07 23:14:23','2024-03-12 22:53:02','1-681-674-2100'),
  ('DAA1B50A-A523-1866-6EC6-AF46544D525F','Craig','Zane','Ezekiel','1999-07-30','UVH949377','5114 Cursus, Rd.','Jun 30, 2024','2024-12-08 20:31:48','2025-10-06 02:20:33','(724) 757-6107'),
  ('1CC7C93B-DB96-3CD1-1226-790E5EA3AFA2','Hillary','Meredith','Francis','1998-04-06','SDM783467','583-4358 Eget, Av.','Apr 25, 2024','2024-11-27 09:51:02','2023-11-29 09:04:45','1-724-831-6285'),
  ('D72755A9-B671-B565-78EE-57E8B48DEEB2','Alec','Yoko','Daniel','1988-11-24','JDY573300','496-5881 Dolor, Avenue','Sep 26, 2025','2024-09-27 11:48:55','2024-08-19 06:34:03','1-291-210-2227'),
  ('F213E5EC-9856-1833-0191-35BE664CBCF6','Ezra','Laith','Jasmine','1988-01-30','GQC665512','424-6219 Non Av.','May 11, 2025','2024-05-03 14:21:53','2025-02-17 19:45:04','(852) 371-6988');


INSERT INTO account (id,amount,date_update,date_insert,currency_name,client_id)
VALUES
  ('D3BB4D04-4233-6113-B208-9861A23BE991',24333,'2023-11-01 12:13:30','2017-10-09 22:19:17','USD','A3A46D33-4235-B9ED-0BD7-3AC47649C0C3'),
  ('9FC8C132-4BBB-E779-22CD-B938994C6A8B',83706,'2024-08-06 13:14:00','2017-08-16 00:40:03','USD','E0558FB9-F913-BA0C-6637-15CDF4678633'),
  ('C13B5B9C-65A3-7FDB-481C-42AE282476A7',36936,'2024-02-09 17:46:05','2017-06-29 06:40:14','USD','E0558FB9-F913-BA0C-6637-15CDF4678633'),
  ('CE159F16-E0B4-022B-D95D-EF672A767E82',10097,'2023-10-25 18:30:24','2017-01-06 21:37:38','USD','DAA1B50A-A523-1866-6EC6-AF46544D525F'),
  ('2A568CC8-7803-5F38-FDF6-F71E491B79B2',15602,'2024-06-16 20:53:25','2023-06-30 00:56:56','USD','A3A46D33-4235-B9ED-0BD7-3AC47649C0C3'),
  ('89F81C8B-D949-214B-E529-20B0407B2757',28003,'2024-01-31 19:13:58','2022-07-22 05:53:12','USD','D72755A9-B671-B565-78EE-57E8B48DEEB2'),
  ('5C8C724C-80E8-EA86-D0EC-A97898A7A0B4',22080,'2024-02-29 12:07:47','2021-01-16 07:23:08','USD','D72755A9-B671-B565-78EE-57E8B48DEEB2'),
  ('01A586C6-FC06-2084-CDEC-CBE8B89DD9D6',2721,'2024-08-01 06:51:14','2019-08-01 00:43:14','USD','F213E5EC-9856-1833-0191-35BE664CBCF6'),
  ('1D92A7EE-06DE-D3D3-3915-6CEB228404BB',46011,'2023-12-10 22:37:01','2016-04-16 13:27:23','USD','E0558FB9-F913-BA0C-6637-15CDF4678633'),
  ('35ED3D9A-7A44-DD5C-27A7-70D528E31710',18349,'2024-09-26 15:39:59','2022-05-27 22:20:24','USD','A3A46D33-4235-B9ED-0BD7-3AC47649C0C3'),
  ('9742E35E-1837-8C93-8246-20C38D3C7236',81508,'2023-10-30 08:16:28','2017-03-14 18:36:41','USD','F213E5EC-9856-1833-0191-35BE664CBCF6'),
  ('7215D94D-43E2-B443-6893-DA2843D5D8DA',60873,'2024-07-16 14:43:23','2019-03-27 23:58:11','USD','DAA1B50A-A523-1866-6EC6-AF46544D525F'),
  ('27FF1EAA-3E5E-8B6B-89EA-C289DD729DC4',47507,'2024-08-07 14:13:07','2016-09-29 18:13:52','USD','E0558FB9-F913-BA0C-6637-15CDF4678633'),
  ('4728CCFD-3C43-2848-1C87-6B6798EEA2DB',91160,'2023-12-31 05:56:50','2019-09-05 02:16:51','USD','D72755A9-B671-B565-78EE-57E8B48DEEB2'),
  ('3B6DB2C2-7997-C831-E23D-485720896D13',42819,'2023-10-22 00:33:24','2018-05-07 11:45:50','USD','DAA1B50A-A523-1866-6EC6-AF46544D525F'),
  ('AE85C386-C462-5BCB-2DD9-A853FA375E68',96032,'2023-11-29 07:50:59','2018-08-03 05:40:00','USD','E0558FB9-F913-BA0C-6637-15CDF4678633'),
  ('BAEE7F38-E25B-DB9D-42D7-5C71D49CA167',7636,'2023-11-05 20:02:39','2022-07-17 00:11:49','USD','A3A46D33-4235-B9ED-0BD7-3AC47649C0C3'),
  ('4B51D4DC-4F1F-63C4-64DC-5D835217888E',33070,'2024-04-10 17:58:15','2019-08-29 15:31:06','USD','D72755A9-B671-B565-78EE-57E8B48DEEB2'),
  ('0C57E0A4-559A-6F0F-4F6A-BE3B463928E3',86008,'2023-10-20 06:11:10','2022-09-13 09:02:32','USD','F213E5EC-9856-1833-0191-35BE664CBCF6'),
  ('7DEDD946-8BBE-4560-6BAC-2274C39E046E',75493,'2024-02-27 04:46:48','2019-07-05 13:10:21','USD','E0558FB9-F913-BA0C-6637-15CDF4678633');

select *
  from account a
where a.amount < 50000
order by a.amount

select * 
  from account a
where a.amount = (select MIN(amount) from account);
или
select * 
  from account a
order by a.account
limit 1;
по идее с индексом второй быстрее

select sum(a.amount) total_sum
    from account a

select *
  from account a
join public.client c on c.id = a.client_id

select *
  from client a
order by a.bdate desc

select date_part('year',age(a.bdate)) age, count(*) count_client
  from client a
group by age
having count(*) > 1

select date_part('year',age(a.bdate)) age, count(*) count_client
  from client a
group by age

select string_agg(concat(a.fname, ' ', a.lname, ' ', a.mname), ','), date_part('year',age(a.bdate)) age, count(*) count_client
  from client a
group by age

select *
  from client a
limit 5